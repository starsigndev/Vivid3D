using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IToolBar : IForm
    {

        public IToolBar() { }

        private int CurrentX = 10;
        public void AddTool(IForm form)
        {
            AddForm(form);
          
            form.Position = new Maths.Position(CurrentX,6);
            if (form is IButton)
            {
                var but = form as IButton;
                if (but.Icon == null)
                {
                    form.Size = new Maths.Size(UI.SystemFont.StringWidth(form.Text) + 15, 20);
                }
                else
                {
                    form.Size = new Maths.Size(28, 20);
                }
            }
                
            CurrentX = CurrentX + form.Size.w + 10;

        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame, -1, -1, -1, -1, new Maths.Color(0.5f, 0.5f, 0.5f, 1.0f));

        }

    }
}
