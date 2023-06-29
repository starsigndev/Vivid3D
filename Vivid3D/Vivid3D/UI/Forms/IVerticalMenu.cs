using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public class IVerticalMenu : IForm
    {
        public static Texture2D Arrow
        {
            get;
            set;
        }
        public List<MenuItem> Items
        {
            get;set;
        }
        public MenuItem OverItem
        {
            get;
            set;
        }
        public IVerticalMenu OpenMenu
        {
            get;
            set;
        }

        public MenuItem OpenItem
        {
            get;
            set;
        }

        public IVerticalMenu()
        {
            Items = new List<MenuItem>();
            OverItem = null;
            OpenMenu = null;
            OpenItem = null;
            if (Arrow == null)
            {
                Arrow = new Texture2D("ui/arrow.png");
            }
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (OverItem != null)
            {
                OverItem.InvokeClick();
                if (OpenMenu != null)
                {
                    Forms.Remove(OpenMenu);
                }

                if (OpenItem == OverItem)
                {

                    OpenMenu = null;
                    OpenItem = null;
                    return;
                }
                OpenMenu = new IVerticalMenu();
                foreach (var item in OverItem.SubItems)
                {
                    OpenMenu.AddItem(item);
                }
                if (OverItem.SubItems.Count == 0)
                {
                    var imenu = Root as IMenu;
                    if (imenu!=null)
                    {
                        imenu.OpenMenu = null;
                        imenu.OpenItem = null;
                    }
                    Root.Forms.Remove(this);

                }
                Forms.Add(OpenMenu);
                OpenItem = OverItem;
                OpenMenu.Position = new Position(RenderPosition.x+Size.w+2,OverItem.DY-5);

            }

        }

        public override void OnActivate()
        {
            if (OpenMenu != null)
            {
                Forms.Remove(OpenMenu);
                OpenMenu = null;
                 OpenItem = null;
            }
        }

        public MenuItem AddItem(string text)
        {

            MenuItem item = new MenuItem();
            item.Text = text;
            AddItem(item);
            return item;

        }

        public MenuItem AddItem(MenuItem item)
        {
          
            Items.Add(item);

            int mw, mh;

            mw = 0;
            mh = 0;

            foreach(var it in Items)
            {
                if (it.Seperator) continue;
                int w = UI.SystemFont.StringWidth(it.Text) + 75;
                if (w > mw) mw = w;
            }

            mh = Items.Count * (UI.SystemFont.StringHeight() + 15);

            Size = new Maths.Size(mw, mh-5);


            return item;
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            int dx = RenderPosition.x + 35;
            int dy = RenderPosition.y + 5;
            OverItem = null;
            foreach (var item in Items)
            {
                if (item.Seperator)
                {
                 //   Draw(UI.Theme.Pure, RenderPosition.x + 15, dy + 2, Size.w - 30, 4, new Maths.Color(1, 1, 1, 1));
                    dy = dy + 13;
                    continue;
                }
                if (position.x >= RenderPosition.x && position.x <= RenderPosition.x + Size.w)
                {
                    if (position.y >= dy && position.y <= (dy + UI.SystemFont.StringHeight() + 8))
                    {
                        OverItem = item;
                    }
                }
             //   UI.DrawString(item.Text, dx, dy + 5, new Maths.Color(1, 1, 1, 1));
                dy = dy + UI.SystemFont.StringHeight() + 8;

            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            BlurBG(0.35f);
            Draw(UI.Theme.Pure, -1, -1, -1, -1, new Maths.Color(0.6f, 0.6f, 0.6f, 0.95f));

            int dx = RenderPosition.x + 35;
            int dy = RenderPosition.y + 5;

            foreach(var item in Items)
            {
                if (item.Seperator)
                {
                    Draw(UI.Theme.Pure, RenderPosition.x + 25, dy+1, Size.w - 50, 4, new Maths.Color(1, 1, 1, 1));
                    dy = dy + 13;
                    continue;
                }
                if (OverItem == item)

                {
                    Draw(UI.Theme.FramePure,RenderPosition.x, dy - 4,Size.w, 27, new Maths.Color(1.7f, 1.7f, 1.7f, 0.8f));
                    Draw(UI.Theme.FramePure,RenderPosition.x+1,dy-3,Size.w-2, 25, new Maths.Color(0.8f, 0.8f, 0.8f, 0.8f));
                }
                if (item.Icon != null)
                {
                    Draw(item.Icon, RenderPosition.x + 5, dy+2, 16, 16, new Maths.Color(1, 1, 1, 1));
                }
                UI.DrawString(item.Text, dx+5, dy+1,UI.Theme.TextColor);
                item.DY = dy;
                if (item.SubItems.Count > 0)
                {
                    Draw(Arrow, RenderPosition.x + Size.w - 22, dy+2, 16, 16, new Maths.Color(1, 1, 1, 1));
                }
                dy = dy + UI.SystemFont.StringHeight() + 11;
                item.DX = dx;



            }

        }

    }
}
