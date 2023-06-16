using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IScrollArea : IForm
    {
        private IVerticalScroller VerticalScroller
        {
            get;
            set;
        }

        private IHorizontalScroller HorizontalScroller
        {
            get;
            set;
        }
        
        public int ScrollerSize
        {
            get;
            set;
        }

        public IScrollArea()
        {
            ScrollerSize = 12;
            VerticalScroller = new IVerticalScroller();
            HorizontalScroller = new IHorizontalScroller();
            AddForms(VerticalScroller, HorizontalScroller);
        }

        public void ClearForms()
        {
            foreach(var form in Forms.ToList())
            {
                if(form!=VerticalScroller && form!=HorizontalScroller)
                {
                    Forms.Remove(form);
                }
            }
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            VerticalScroller.Set(Size.w - ScrollerSize, 12, ScrollerSize, Size.h-24);
            HorizontalScroller.Set(12, Size.h - ScrollerSize, Size.w-ScrollerSize-24, ScrollerSize);
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            InvokeClick(this, button);
        }

        public override void OnUpdate()
        {
            //base.OnUpdate();
            int mh = ContentSize.h;
            int mw = ContentSize.w;

            VerticalScroller.MaxValue = mh;
            HorizontalScroller.MaxValue = mw;

            float cy = VerticalScroller.Value * mh;
            float cx = HorizontalScroller.Value * mw;
            Console.WriteLine("CV:" + cy);

            ScrollValue = new Maths.Position((int)cx, (int)cy);

            int a = 5;


        }

    }
}
