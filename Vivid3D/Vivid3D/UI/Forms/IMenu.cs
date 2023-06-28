using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public delegate void MenuAction(MenuItem item);
    public class MenuItem
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
        public event MenuAction Click;

        public int DX, DY;
        public MenuItem()
        {
            DX = DY = 0;
            Click = null;
        }

        public void InvokeClick()
        {
            Click?.Invoke(this);
        }

        public MenuItem AddItem(string text)
        {
            MenuItem item = new MenuItem();
            item.Text = text;
            SubItems.Add(item);
            return item;
        }

        public List<MenuItem> SubItems = new List<MenuItem>();



    }
    public class IMenu : IForm
    {

        public List<MenuItem> Items
        {
            get;
            set;
        }

        public MenuItem OverItem
        {
            get;
            set;
        }

        public MenuItem OpenItem
        {
            get;
            set;
        }

        public IVerticalMenu OpenMenu
        {
            get;
            set;
        }
        
        public IMenu()
        {
            OverItem = null;
            Items = new List<MenuItem>();
            OpenMenu = null;
        }

        public MenuItem AddItem(string text)
        {
            MenuItem item = new MenuItem();
            item.Text = text;
            Items.Add(item);
            return item;
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (OverItem != null)
            {
                
                if (OpenMenu != null)
                {
                    Forms.Remove(OpenMenu);
                }
                
                if(OpenItem == OverItem)
                {
                    
                    OpenMenu = null;
                    OpenItem = null;
                    return;
                }
                OpenMenu = new IVerticalMenu();
                foreach(var item in OverItem.SubItems)
                {
                    OpenMenu.AddItem(item);

                }
                Forms.Add(OpenMenu);
                OpenItem = OverItem;
                OpenMenu.Position = new Position(OverItem.DX, Size.h + 1);

            }
            else
            {
                if (OpenMenu != null)
                {
                    Forms.Remove(OpenMenu);
                    OpenMenu = null;
                    OpenItem = null;
                }

            }

        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            int dx = 5;
            int dy = 10;
            OverItem = null;
            foreach (var item in Items)
            {
                if(position.x>=dx && position.x<=(dx+UI.SystemFont.StringWidth(item.Text)+20))
                {
                    if(position.y>=RenderPosition.y && position.y <= RenderPosition.y + Size.h){
                        OverItem = item;
                    }
                }
                dx = dx + UI.SystemFont.StringWidth(item.Text) + 20;
              //  UI.DrawString(item.Text, dx + 10, dy, new Maths.Color(1, 1, 1, 1));
            //    dx += UI.SystemFont.StringWidth(item.Text) + 20;
            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame, 0, 0, Size.w, Size.h, new Maths.Color(0.5f, 0.5f, 0.5f, 1.0f));

            int dx = 5;
            int dy = 7;

            foreach(var item in Items)
            {
                if (OverItem == item)
                
                {
                    Draw(UI.Theme.FramePure, dx-1, dy - 6, UI.SystemFont.StringWidth(item.Text) + 22, 29, new Maths.Color(1.4f, 1.4f, 1.4f, 0.8f));
                    Draw(UI.Theme.FramePure, dx, dy-4, UI.SystemFont.StringWidth(item.Text) + 20, 25, new Maths.Color(0.5f, 0.5f, 0.5f, 0.8f));
                }
                UI.DrawString(item.Text, dx+10,dy,UI.Theme.TextColor);
                item.DX = dx;
                item.DY = dy;
                dx += UI.SystemFont.StringWidth(item.Text) + 20;
            }

        }

    }
}
