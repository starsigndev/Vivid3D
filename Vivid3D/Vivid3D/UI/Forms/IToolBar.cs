using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public class IToolBar : IForm
    {

        public IToolBar() { }

        private int CurrentX = 10;

        public IForm AddTool(string text)
        {
            IButton button = new IButton().SetText(text) as IButton;
            return AddTool(button);

        }

        public IForm AddTool(Texture2D icon)
        {
            IButton button = new IButton();
            button.Icon = icon;
            return AddTool(button);

        }
        public void AddSpace(int space)
        {
            CurrentX += space;
        }
        public IForm AddTool(IForm form)
        {
            AddForm(form);
          
            form.Position = new Maths.Position(CurrentX,6);
            if (form is IButton)
            {
                var but = form as IButton;
                if (but.Icon == null)
                {
                    form.Size = new Maths.Size(UI.SystemFont.StringWidth(form.Text) + 25, 30);
                }
                else
                {
                    form.Size = new Maths.Size(38, 30);
                }
            }
                
            CurrentX = CurrentX + form.Size.w + 10;
            return form;

        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame, -1, -1, -1, -1, new Maths.Color(0.5f, 0.5f, 0.5f, 1.0f));

        }

    }
}
