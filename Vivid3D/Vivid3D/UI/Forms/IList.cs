using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public delegate void ItemAction(ListItem item,int index,object data);

    public class ListItem
    {
        public int Index = 0;
        public ItemAction Action
        {
            get;
            set;
        }
        public string Name
        {
            get;set;
        }

        public object Data
        {
            get;
            set;
        }

        public Maths.Color Color
        {
            get;
            set;
        }

        public ListItem()
        {
            Index = 0;
            Name = "";
            Action = null;
            Color = new Maths.Color(0.7f, 0.7f, 0.7f, 0.97f);
        }
    }
    public class IList : IForm
    {

        public IFrame ListFrame
        {
            get;
            set;
        }

        public IFrame ListContentsFrame
        {
            get;
            set;
        }

        public List<ListItem> Items
        {
            get;
            set;
        }

        public ListItem OverItem
        {
            get;
            set;
        }
           
        public IList()
        {

            Items = new List<ListItem>();
            OverItem = null;
            /*
            ListFrame = new IFrame();
            ListContentsFrame = new IFrame();
            AddForms(ListFrame);
            ListFrame.AddForm(ListContentsFrame);
            ListContentsFrame.Color = new Maths.Color(0, 1, 1, 1);
            */

        }

        public override void AfterSet()
        {
            //base.AfterSet();
            //ListFrame.Set(new Maths.Position(0, 0), new Maths.Size(Size.w, Size.h), "");
          //  ListContentsFrame.Set(new Maths.Position(10, 10), new Maths.Size(Size.w - 20, Size.h - 20),"");
        }

        public ListItem AddItem(string name)
        {
            ListItem item = new ListItem();
            item.Name = name;
            item.Index = Items.Count;
            Items.Add(item);

            return item;
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            int ix, iy;
            ix = RenderPosition.x + 30;
            iy = RenderPosition.y + 40;
            OverItem = null;
            foreach(var item in Items)
            {
                if (position.y > iy && position.y < iy + 30)
                {
                    if (position.x > RenderPosition.x && position.x < RenderPosition.x + Size.w)
                    {
                        OverItem = item;
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            
            base.OnUpdate();
            foreach(var item in Items)
            {
                if (item == OverItem)
                {
                    item.Color += (new Maths.Color(1f, 1.5f, 1.5f, 0.97f) - item.Color) * 0.1f;
                }
                else
                {
                    item.Color += (new Maths.Color(1.7f, 0.7f, 0.7f, 0.97f) - item.Color) * 0.1f;
                }
            }
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (OverItem != null)
            {
                OverItem.Action?.Invoke(OverItem, 0, OverItem.Data);
            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame);
            Draw(UI.Theme.Frame, 10, 10, Size.w - 20, Size.h - 20, new Maths.Color(3, 3, 3, 1));

            int ix, iy;

            ix = RenderPosition.x + 30;
            iy = RenderPosition.y + 40;

            foreach(var item in Items)
            {

                UI.DrawString(item.Name, ix, iy,item.Color);
                iy = iy + 40;

            }

        }



    }
}
