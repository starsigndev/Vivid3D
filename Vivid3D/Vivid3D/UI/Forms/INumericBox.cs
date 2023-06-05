using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class INumericBox : IForm
    {
        public IButton Up
        {
            get;
            set;
        }

        public IButton Down
        {
            get;
            set;
        }

        public ITextBox Number
        {
            get;
            set;
        }

        public float Increment
        {
            get;set;
        }

        public INumericBox()
        {
            Increment = 1f;
            Down = new IButton();
            Up = new IButton();
            Number = new ITextBox();
            Number.Numeric = true;
            AddForms(Up, Down, Number);
            Down.OnClick = (form,data) =>
            {
                float num = Number.Value - Increment;
                Number.Text = num.ToString();
            };
            Up.OnClick = (form, data) =>
            {
                float num = Number.Value + Increment;
                Number.Text = num.ToString();
            };
        }

        public override void AfterSet()
        {
            Down.Set(0, 0, 32,Size.h, "\\/");
            Up.Set(Size.w - 32, 0,32, Size.h, "/\\");
            Number.Set(32, 0, Size.w - 64, Size.h, "0");
            //base.AfterSet();


        }

    }
}
