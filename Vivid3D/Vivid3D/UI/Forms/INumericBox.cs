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
            Down.Icon = UI.Theme.ArrowDown;
            Up.Icon = UI.Theme.ArrowUp;
            Number = new ITextBox();
            Number.Numeric = true;
            AddForms(Up, Down, Number);
            Down.OnClick += (form,data) =>
            {
                float num = Number.Value - Increment;
                Number.Text = num.ToString();
            };
            Up.OnClick += (form, data) =>
            {
                float num = Number.Value + Increment;
                Number.Text = num.ToString();
            };
        }

        public override void AfterSet()
        {
            Down.Set(0, 12, 18,11, "\\/");
            Up.Set(0, 0,18,11, "/\\");
            Number.Set(18, 0, Size.w - 20, Size.h-2, "0");
            //base.AfterSet();


        }

    }
}
