using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public class ILabelButton : IForm
    {

        public float LabelScale
        {
            get;
            set;
        }

        private bool OverText
        {
            get;
            set;
        }

        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
               // OverText = false;
                float prev_scale = UI.SystemFont.Scale;

                UI.SystemFont.Scale = LabelScale;
                Size.w = UI.SystemFont.StringWidth(Text);
                Size.h = UI.SystemFont.StringHeight();
                UI.SystemFont.Scale = prev_scale;

            }
        }
        private string _text = "";
        public ILabelButton()
        {
            LabelScale = 1.0f;
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            InvokeClick(this, null);
        }

        public override void OnLeave()
        {
            //base.OnLeave();
            OverText = false;
        }
        public override void OnEnter()
                
        
        {
            //base.OnMouseMove(position, delta);
            OverText = false;
            //float prev_scale = UI.SystemFont.Scale;
            
            //UI.SystemFont.Scale = LabelScale;
           // Size.w = UI.SystemFont.StringWidth(Text);
           // Size.h = UI.SystemFont.StringHeight();
           // if (position.x>=RenderPosition.x && position.x <= RenderPosition.x + UI.SystemFont.StringWidth(Text))
           // {
              //  if(position.y>=RenderPosition.y && position.y<=RenderPosition.y + UI.SystemFont.StringHeight())
                //{
                    OverText = true;
            //    }
          //  }



        }

        public override void OnRender()
        {
            //base.OnRender();

            float col_scale = 0.8f;

            if (OverText)
            {
                col_scale = 1.0f;
            }

            float prev_scale = UI.SystemFont.Scale;
            UI.SystemFont.Scale = LabelScale;
            UI.DrawString(Text, RenderPosition.x, RenderPosition.y, UI.Theme.TextColor*col_scale);
            UI.SystemFont.Scale = prev_scale;
        }


    }
}
