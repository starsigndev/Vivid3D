﻿using System;
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
            DrawOutline = true;
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
        public void CalculateHeight()
        {

            int mh = 0;
            mh = (Items.Count) * (UI.SystemFont.StringHeight()+8);
            Size = new Maths.Size(Size.w, mh);

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
            ix = RenderPosition.x + 5;
            iy = RenderPosition.y + 5;
            OverItem = null;
            foreach(var item in Items)
            {
                if (position.y > iy-2 && position.y < (iy-2) + UI.SystemFont.StringHeight()+8 )
                {
                    if (position.x > RenderPosition.x && position.x < RenderPosition.x + Size.w)
                    {
                        OverItem = item;
                    }
                }
                iy = iy + UI.SystemFont.StringHeight() + 8;
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
            Draw(UI.Theme.Pure, -1, -1, -1, -1, new Maths.Color(0.5f, 0.5f, 0.5f, 0.85f));
           // Draw(UI.Theme.Frame, 10, 10, Size.w - 20, Size.h - 20, new Maths.Color(3, 3, 3, 1));

            int ix, iy;

            ix = RenderPosition.x + 5;
            iy = RenderPosition.y + 5;
          //  return;
            foreach(var item in Items)
            {
                if(item == OverItem)
                {
                    Draw(UI.Theme.Frame, ix - 5, iy-2, Size.w, UI.SystemFont.StringHeight() + 6, new Maths.Color(0.7f, 0.7f, 0.7f, 0.8f));
                }
                UI.DrawString(item.Name, ix, iy,item.Color);
                iy = iy + UI.SystemFont.StringHeight() + 8;

            }

        }



    }
}
