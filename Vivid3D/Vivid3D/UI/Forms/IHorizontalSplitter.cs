using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public class IHorizontalSplitter : IForm
    {
        public IForm TopForm
        {
            get;
            set;
        }

        public IForm BottomForm
        {
            get;
            set;
        }

        public int SplitY
        {
            get;
            set;
        }

        public IHorizontalSplitter()
        {

            TopForm = null;
            BottomForm = null;

        }

        public override void AfterSet()
        {
            //base.AfterSet();
            SplitY = Size.h / 2;
        }

        public void SetSplit(int y)
        {
            SplitY = y;
            if (SplitY > Size.h - 40)
            {
                SplitY = Size.h - 40;
            }
            UpdateForms();
        }

        public void SetTop(IForm form)
        {
            TopForm = form;
            AddForm(TopForm);
            UpdateForms();
        }

        public void SetBottom(IForm form)
        {
            BottomForm = form;
            AddForm(BottomForm);
            UpdateForms();
        }
        public void UpdateForms()
        {
            if (TopForm != null)
            {
                TopForm.Set(0,0,Size.w,SplitY-5, TopForm.Text);
                TopForm.Static = true;
            }
            if (BottomForm != null)
            {
                BottomForm.Set(0, SplitY + 5, Size.w, Size.h - (SplitY+35), BottomForm.Text);
                BottomForm.Static = true;
            }
        }

        private bool OverDrag = false;
        private bool Drag = false;
        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (OverDrag)
            {
                Drag = true;
            }
        }
        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            Drag = false;
        }
        public override void OnMouseMove(Position position, Delta delta)
        {
            if (!Drag)
            {
                //base.OnMouseMove(position, delta);
                if (position.x >= RenderPosition.x  && position.x <= RenderPosition.x + Size.w)
                {
                    if (position.y >= RenderPosition.y + (SplitY - 4) && position.y <= RenderPosition.y + (SplitY+4))
                    {
                        OverDrag = true;
                    }
                    else
                    {
                        OverDrag = false;
                    }
                }
                else
                {
                    OverDrag = false;
                }
            }
            else
            {
                SetSplit(SplitY + (int)delta.y);
            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.FramePure, RenderPosition.x, RenderPosition.y+SplitY-4, Size.w, 8, new Maths.Color(2, 2, 2, 1));
        }

    }
}
