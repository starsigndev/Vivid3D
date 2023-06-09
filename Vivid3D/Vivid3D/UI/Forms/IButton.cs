﻿using Vivid.Maths;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public class IButton : IFrame
    {

        public Texture2D Icon
        {
            get;
            set;
        }

        private Maths.Color Target;
        private Maths.Color SelectedCol = new Maths.Color(0, 0, 0, 0);
        bool Over = false;
        bool Down = false;
        private Texture2D SelectedImage;
        public bool Drag = false;

        public IButton()
        {
            Target = new Maths.Color(0.7f, 0.7f, 0.7f, 0.8f);

            Image = UI.Theme.Button;
            SelectedImage = UI.Theme.ButtonSelected;
            Color = new Maths.Color(0.85f, 0.85f, 0.68f, 0.5f);
        }

        public override void OnEnter()
        {
            //base.OnEnter();
            Target = (new Maths.Color(1.2f, 1.2f, 1.2f, 0.85f));
            Over = true;
            //   Console.WriteLine("Button!!!!!!!!");
            // Environment.Exit(0);
        }

        public override void OnLeave()
        {
            //base.OnLeave();
            Over = false;
            Target = new Maths.Color(0.85f, 0.85f, 0.85f, 0.8f);
        }

        public override void OnUpdate()
        {

            if (Down)
            {
                if (Environment.TickCount > next_down)
                {
                    next_down = next_down + 100;
                    InvokeMouseDown(this, null);
                    
                }
            }
            Color = Color + (Target - Color) * new Maths.Color(0.06f, 0.06f, 0.06f, 0.75f);
            //base.OnUpdate();

            if(Down)
            {
                SelectedCol += (new Maths.Color(1.3f, 1.3f, 1.3f, 0.95f) - SelectedCol) * 0.1f;
            }else
            if (Over)
            {
                SelectedCol += (new Maths.Color(1, 1, 1, 0.85f) - SelectedCol) * 0.05f;
            }
            else
            {
                SelectedCol += (new Maths.Color(1.05f, 1.05f,1.05f, 1.0f) - SelectedCol) * 0.08f;
            }
        }
        int next_down = 0;
        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            Target = new Maths.Color(1.4f, 1.8f, 1.8f, 1.0f);
            //OnClick?.Invoke(this, null);
            InvokeClick(this, null);
            Down = true;
            Drag = true;
            next_down = Environment.TickCount + 500;
        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            Target = new Maths.Color(0.5f, 0.5f, 0.5f, 0.8f);
            OnEnter();
           
            InvokeClicked(this,null);

            Down = false;
            Drag = false;
            next_down = 0;
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            if (Drag)
            {
              
                InvokeMove(this, (int)delta.x, (int)delta.y);
             //   Environment.Exit(1);
            }
        }

        public override void OnRender()
        {
            Draw(Image);
            if (Highlight)
            {
                Draw(UI.Theme.Highlight, -1, -1, -1, -1, new Maths.Color(2, 2, 2, 1));
            }
            // Draw(SelectedImage, -1, -1, -1, -1, SelectedCol);
            if (Icon == null)
            {
                UI.DrawString(Text, RenderPosition.x + Size.w / 2 - UI.SystemFont.StringWidth(Text) / 2, RenderPosition.y + Size.h / 2 - UI.SystemFont.StringHeight() / 2, UI.Theme.TextColor);
            }
            else
            {
                Draw(Icon, RenderPosition.x + 4, RenderPosition.y + 4, Size.w - 8, Size.h -8, new Maths.Color(1, 1, 1, 0.85f));
            }
        }
    }
}