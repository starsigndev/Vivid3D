using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public class IHorizontalScroller : IForm
    {

        public ValueChanged OnValueChanged
        {
            get;
            set;
        }

        public int MaxValue
        {
            get;
            set;
        }

        public int CurrentValue
        {
            get
            {
                return _CV;
            }
            set
            {

                _CV = value;
                if (av2 > 1.0)
                {
                    av2 = 1.0f;
                }
                if (_CV > Size.w - dh)
                {
                    _CV = Size.w - dh;
                }
                if (_CV < 0)
                {
                    _CV = 0;
                }

                //OnMove?.Invoke(this, (int)((float)MaxValue * av2),0);
                InvokeMove(this,(int)((float)MaxValue*av2),0);
            }

        }
        private int _CV = 0;
        private float av2 = 0;
        private int dh = 0;
        public float Value
        {
            get
            {
                float yi, hd, av, ov;
                float nm = 0;
                float ay = 0;
                float max_V = 0;
                yi = hd = av = ov = av2 = 0.0f;
                dh = 0;

                ov = (float)(Size.w - MaxValue) / (float)(MaxValue);

                if (ov > 0.95)
                {

                    ov = 0.95f;
                }
                if (ov < 0.35)
                {
                    ov = 0.35f;
                }





                dh = (int)(Size.w * ov);






                if (CurrentValue + dh > Size.w)
                {
                    if (dh != float.PositiveInfinity)
                    {
                        CurrentValue = Size.w - (int)dh;
                        if (CurrentValue < 0) CurrentValue = 0;
                    }
                }



                max_V = Size.w - (dh);


                av2 = CurrentValue / max_V;

                return av2;

                //return 0;

            }
        }
        public IButton LeftButton
        {
            get;
            set;
        }

        public IButton RightButton
        {
            get;
            set;
        }
        public IHorizontalScroller()
        {
            CurrentValue = 0;
            MaxValue = 350;
            OnValueChanged = null;
            LeftButton = new IButton();
            RightButton = new IButton();
            LeftButton.Icon = UI.Theme.ArrowLeft;
            RightButton.Icon = UI.Theme.ArrowRight;

            LeftButton.OnClick += LeftButton_OnClick;
            RightButton.OnClick += RightButton_OnClick;
            LeftButton.MouseDown += LeftButton_MouseDown;
            RightButton.MouseDown += RightButton_MouseDown;

            /*
            LeftButton.OnClick = (form, data) =>
            {
                CurrentValue = CurrentValue - 10;
            };
            RightButton.OnClick = (form, data) =>
            {
                CurrentValue = CurrentValue + 10;
            };
            LeftButton.MouseDown = (form, data) =>
            {
                CurrentValue = CurrentValue - 10;
            };
            RightButton.MouseDown = (form, data) =>
            {
                CurrentValue = CurrentValue + 10;
            };
            */
            AddForms(LeftButton, RightButton);

        }

        private void RightButton_MouseDown(IForm form, object data = null)
        {
            CurrentValue = CurrentValue + 10;
        }

        private void LeftButton_MouseDown(IForm form, object data = null)
        {
            CurrentValue = CurrentValue - 10;
        }

        private void RightButton_OnClick(IForm form, object data = null)
        {
            CurrentValue = CurrentValue + 10;
        }

        private void LeftButton_OnClick(IForm form, object data = null)
        {
            //throw new NotImplementedException();
            CurrentValue = CurrentValue - 10;
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (over_drag)
            {
                Dragging = true;
            }

        }
        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            Dragging = false;
            over_drag = false;
        }
        private bool Dragging = false;
        private bool over_drag = false;

        public override void AfterSet()
        {
            //base.AfterSet();
            LeftButton.Set(-12,0, 12, Size.h);
            RightButton.Set(Size.w,0, 12,Size.h);
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            if (Dragging)
            {
                int cy = CurrentValue;
                CurrentValue = CurrentValue + (int)delta.x;
                if (CurrentValue < 0)
                {
                    CurrentValue = 0;
                }
                //OnMove?.Invoke(this, (int)((float)MaxValue * av2),0);
                InvokeMove(this, (int)((float)MaxValue * av2), 0);

            }
            else
            {
                //base.OnMouseMove(position, delta);
                if (position.x >= (RenderPosition.x+CurrentValue) && position.x <= (RenderPosition.x + CurrentValue +dh))
                {
                    //Environment.Exit(1);
                    if (position.y >= (RenderPosition.y) && position.y <= (RenderPosition.y + Size.h))
                    {
                        over_drag = true;
                    }
                    else
                    {
                        over_drag = false;
                    }
                }
                else
                {
                    over_drag = false;
                }
            }
        }

        public override void OnRender()
        {
            //base.OnRender();

            float v = Value;
            Draw(UI.Theme.Pure, -1, -1, -1, -1, new Maths.Color(0.3f, 0.3f, 0.3f, 1.0f));
            Draw(UI.Theme.FramePureH, RenderPosition.x+CurrentValue, RenderPosition.y , dh, Size.h);

        }

    }
}
