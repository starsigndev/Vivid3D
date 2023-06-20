using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public delegate void Checked(IForm form, bool val);
    public class ICheckBox : IForm
    {
        public bool Checked
        {
            get;
            set;
        }

        public event Checked OnChecked;



        public ICheckBox(string name)
        {
            Text = name;
            Checked = false;
            Set(0, 0, 16, 16, Text);
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);

            if(button == MouseID.Left)
            {
                Checked = Checked ? false : true;
                OnChecked?.Invoke(this, Checked);
            }

        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Pure, RenderPosition.x, RenderPosition.y, 14, 14, new Maths.Color(2f, 2f, 2f, 1));
            if (Checked)
            {
                Draw(UI.Theme.Pure, RenderPosition.x + 3, RenderPosition.y + 3, 8, 8, new Maths.Color(5f, 5f, 5f, 1));
            }

            UI.DrawString(Text, RenderPosition.x + 25, RenderPosition.y+1, UI.Theme.TextColor);

        }

    }
}
