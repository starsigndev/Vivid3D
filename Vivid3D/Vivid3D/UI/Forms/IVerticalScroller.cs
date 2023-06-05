using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public delegate void ValueChanged(float value);
    public class IVerticalScroller : IForm
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
            get;
            set;
            
        }
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

                ov = (float)(Size.h - MaxValue) / (float)(MaxValue);

                if (ov > 0.95)
                {

                    ov = 0.95f;
                }
                if (ov < 0.35)
                {
                    ov = 0.35f;
                }





                dh = (int)(Size.h * ov);






                if (CurrentValue + dh > Size.h)
                {
                    if (dh != float.PositiveInfinity)
                    {
                        CurrentValue = Size.h - (int)dh;
                        if (CurrentValue < 0) CurrentValue = 0;
                    }
                }



                max_V = Size.h - (dh);


                av2 = CurrentValue / max_V;

                return av2;

                //return 0;

            }
        }

        public IVerticalScroller()
        {
            CurrentValue = 0;
            MaxValue = 350;
            OnValueChanged = null;
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
        public override void OnMouseMove(Position position, Delta delta)
        {
            if (Dragging)
            {
                int cy = CurrentValue;
                CurrentValue = CurrentValue + (int)delta.y;
                if (CurrentValue < 0)
                {
                    CurrentValue = 0;
                }
                OnMove?.Invoke(this, 0,(int)((float)MaxValue*av2));
            }
            else
            {
                //base.OnMouseMove(position, delta);
                if (position.x >= RenderPosition.x && position.x <= RenderPosition.x + Size.w)
                {
                    //Environment.Exit(1);
                    if (position.y >= (RenderPosition.y + CurrentValue) && position.y <= (RenderPosition.y + CurrentValue + dh))
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
            Draw(UI.Theme.FramePure, RenderPosition.x, RenderPosition.y+CurrentValue, Size.w, dh);

        }


    }
}
