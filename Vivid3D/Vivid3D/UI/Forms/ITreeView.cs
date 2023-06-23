using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vivid.Maths;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public delegate void TreeAction(TreeItem item);

    public class TreeItem
    {
        public string Text
        {
            get;
            set;
        }

        public Texture2D Icon
        {
            get;
            set;
        }

        public List<TreeItem> Items
        {
            get;
            set;
        }

        public bool Open
        {
            get;
            set;
        }

        public TreeAction Click
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public TreeItem()
        {
            Items = new List<TreeItem>();
        }

        public TreeItem AddItem(string text)
        {
            TreeItem item = new TreeItem();
            item.Text = text;
            Items.Add(item);
            return item;
        }

    }
    public class ITreeView : IForm
    {
        public IVerticalScroller VerticalScroller { get; set; }
        public IHorizontalScroller HorizontalScroller { get; set; }

        public int ScrollSize
        {
            get;
            set;
        }

        public TreeItem Root
        {
            get;
            set;
        }

        private int ScrollX
        {
            get;
            set;
        }
        private int ScrollY
        {
            get;
            set;
        }

        public TreeItem OverItem
        {
            get;
            set;
        }

        public TreeItem SelectedItem
        {
            get;
            set;
        }

        public TreeAction Click
        {
            get;
            set;
        }

        public ITreeView()
        {
            ScrollSize = 14;
            VerticalScroller = new IVerticalScroller();
            HorizontalScroller = new IHorizontalScroller();
            AddForms(VerticalScroller, HorizontalScroller);
            Root = new TreeItem();
            ScissorSelf = true;
            VerticalScroller.OnMove += (item, x, y) =>
            {
                //ScrollValue = new Maths.Position(0, y);
               
                ScrollY = y;
            };
            HorizontalScroller.OnMove += (item, x, y) =>
            {
                ScrollX = x;
            };
            DrawOutline = true;
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            VerticalScroller.Set(Size.w - ScrollSize, 12, ScrollSize, Size.h-26, "");
            HorizontalScroller.Set(12, Size.h - ScrollSize, Size.w-24-ScrollSize, ScrollSize, "");
        }
        private int bigx = 0;
        public override void OnMouseWheelMove(Vector2 delta)
        {
            //base.OnMouseWheelMove(delta);
            //ScrollY -= (int)delta.Y * 15;
            VerticalScroller.CurrentValue = VerticalScroller.CurrentValue - (int)delta.Y * 15;

            if (ScrollY < 0)
            {
                ScrollY = 0;
            }
            if (ScrollY > VerticalScroller.MaxValue)
            {
                ScrollY = VerticalScroller.MaxValue;

            }
        }
        public int RenderItem(TreeItem item,ref int x,int y)
        {
            

            foreach(var sub in item.Items)
            {

                if(sub == OverItem)
                {
                    Draw(UI.Theme.FramePure, RenderPosition.x+1, y-ScrollY, Size.w - ScrollSize-2, UI.SystemFont.StringHeight() + 4, new Maths.Color(0.7f, 0.7f, 0.7f, 1.0f));
                       
                }else if(sub == SelectedItem)
                {
                    Draw(UI.Theme.FramePure, RenderPosition.x + 1, y - ScrollY, Size.w - ScrollSize - 2, UI.SystemFont.StringHeight() + 4, new Maths.Color(0.7f, 0.95f, 0.95f, 1.0f));
                }
                
                if (sub.Items.Count > 0)
                {
                    Draw(UI.Theme.Pure, x - ScrollX, (y + 6) - ScrollY, 8, 8, new Maths.Color(1.8f, 1.8f,1.8f, 1.0f));
                    if (sub.Open)
                    {
                        Draw(UI.Theme.Pure, x + 2 - ScrollX, (y + 8) - ScrollY, 4, 4, new Maths.Color(3.4f,3.4f, 3.4f, 1.0f));
                    }
                }
                UI.DrawString(sub.Text, x+16-ScrollX, y+3-ScrollY,UI.Theme.TextColor);
                
                int bx = x + 16 + UI.SystemFont.StringWidth(sub.Text);

                if (bx > bigx) bigx = bx;
                y = y + UI.SystemFont.StringHeight() + 4;

                if (sub.Open)
                {
                    x = x + 32;
                    y = RenderItem(sub,ref x, y);
                    x = x - 32;
                }
            }

            return y;

        }

        public int CheckItem(TreeItem item,int y,int mx,int my)
        {
            foreach(var sub in item.Items)
            {
                if (mx >= RenderPosition.x && mx <= RenderPosition.x + Size.w - ScrollSize)
                {
                    if (my >= (y - ScrollY) && my <= (y - ScrollY) + UI.SystemFont.StringHeight() + 4)
                    {
                        OverItem = sub;
                    }
                }
                y = y + UI.SystemFont.StringHeight() + 4;

                if (sub.Open)
                {
                    y = CheckItem(sub, y,mx,my);
                }
            }
            return y;
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);(
            if (OverItem != null)
            {
                OverItem.Open = OverItem.Open ? false : true;
          
                OverItem.Click?.Invoke(OverItem);

                Click?.Invoke(OverItem);

                SelectedItem = OverItem; 

            }

            DragObject drag = new DragObject();
            drag.Image = null;
            drag.Text = OverItem.Text;
            drag.Path = "";
            drag.Object = OverItem.Data;
            UI.This.BeginDrag(drag);
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            OverItem = null;
            CheckItem(Root,RenderPosition.y + 8,position.x,position.y);
        }

        public override void OnRender()
        {
         //   Draw(UI.Theme.Frame, RenderPosition.x, RenderPosition.y, Size.w, Size.h, new Maths.Color(2, 2, 2, 2));
            Draw(UI.Theme.Frame, RenderPosition.x, RenderPosition.y, Size.w, Size.h, new Maths.Color(0.35f, 0.35f, 0.35f, 1.0f));
            bigx = 0;
            int dx = RenderPosition.x + 8;
            //ScrollX = 0;

            int y = RenderItem(Root,ref dx,RenderPosition.y+8);
            //  y = y - RenderPosition.y;
            VerticalScroller.MaxValue = y - 108;
            if(VerticalScroller.MaxValue<0) VerticalScroller.MaxValue = 0;
            dx = dx - RenderPosition.x;
            
            int mv = bigx-(Size.w-ScrollSize);
            if (mv < 5) mv = 5;
            HorizontalScroller.MaxValue = mv;


            




        }

    }
}
