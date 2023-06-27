using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public delegate void SliderChanged(float val);
      public class IHorizontalSlider : IForm
    {

        public float Max
        {
            get;
            set;
        }

        public float Min
        {
            get;
            set;
        }

        public float Value
        {
            get;
            set;
        }

        public event SliderChanged OnSliderChanged;

        private bool Drag = false;

        public IHorizontalSlider()
        {

            Min = 0.0f;
            Max = 1.0f;
            Value = 0.0f;

        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            Drag = true;

        }

        public override void OnMouseUp(MouseID button)
        {
            base.OnMouseUp(button);
            Drag = false;
        }

        bool Over = false;
        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);

            if (Drag)
            {
                mp = position - RenderPosition;
                md = delta;
                Value = ((float)mp.x / (float)Size.w);
                if (Value < 0) Value = 0;
                if (Value > 1) Value = 1;
                OnSliderChanged?.Invoke(Value);
            }
        }
        Position mp;
        Delta md;

        public override void OnRender()
        {
            //base.OnRender();

            Draw(UI.Theme.Pure, RenderPosition.x, RenderPosition.y+4, Size.w, 4, new Maths.Color(0.9f, 0.9f, 0.9f, 1.0f));
            int sx = (int)(Value * (float)Size.w);
            //sx = 20;

            Draw(UI.Theme.Pure, RenderPosition.x + sx - 3, RenderPosition.y, 6, 12, new Maths.Color(2, 2, 2, 1));
//            UI.DrawString(Value.ToString(), RenderPosition.x + Size.w + 8, RenderPosition.y-2, new Maths.Color(1, 1, 1, 1));


        }

    }
}
