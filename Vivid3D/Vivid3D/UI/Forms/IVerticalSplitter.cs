using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public class IVerticalSplitter : IForm
    {

        public IForm LeftForm
        {
            get;
            set;
        }

        public IForm RightForm
        {
            get;
            set;
        }

        public int SplitX
        {
            get;
            set;
        }

        public IVerticalSplitter()
        {

            LeftForm = null;
            RightForm = null;
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            SplitX = Size.w / 2;
        }

        public void SetSplit(int x)
        {
            SplitX = x;
            UpdateForms();
        }

        public void SetLeft(IForm form)
        {
            LeftForm = form;
            AddForm(LeftForm);
            UpdateForms();
        }

        public void SetRight(IForm form)
        {
            RightForm = form;
            AddForm(RightForm);
            UpdateForms();
        }
        public void UpdateForms()
        {
            if (LeftForm != null)
            {
                LeftForm.Set(0, 0, SplitX - 5, Size.h, LeftForm.Text);
                LeftForm.Static = true;
            }
            if (RightForm != null)
            {
                RightForm.Set(SplitX + 5, 0, Size.w - (SplitX), Size.h, RightForm.Text);
                RightForm.Static = true;
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
                if (position.x >= RenderPosition.x + SplitX - 4 && position.x <= RenderPosition.x + SplitX + 4)
                {
                    if (position.y >= RenderPosition.y && position.y <= RenderPosition.y + Size.h)
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
                SetSplit(SplitX + (int)delta.x);
            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.FramePure, RenderPosition.x + SplitX - 4, RenderPosition.y, 8, Size.h, new Maths.Color(2, 2, 2, 1));
        }
    }
}
