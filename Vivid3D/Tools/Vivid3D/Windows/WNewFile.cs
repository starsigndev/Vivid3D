using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI;
using Vivid.UI.Forms;


namespace Vivid3D.Windows
{
    public delegate void FileNamePicked(string filename);

    public class WNewFile : IWindow
    {
        public ITextBox Name
        {
            get;
            set;

        }

        public event FileNamePicked OnFileNamePicked;

        public WNewFile() : base("New File")
        {

            UI.This.Top = this;
            Set(0, 0, 300, 90, "New File");
            var lab = new ILabel().Set(10, 10, 20, 20, "File Name");
            Name = new ITextBox().Set(90, 5, 200, 25, "") as ITextBox;
            Content.AddForms(lab,Name);
            var OK = new IButton().Set(10, 40, 80, 25, "OK") as IButton;
            var cancel = new IButton().Set(195,40,80,25,"Cancel") as IButton;
            Content.AddForms(OK, cancel);

            OK.OnClick += (form, but) =>
            {
                OnFileNamePicked?.Invoke(Name.Text);
                UI.This.Top = null;
            };

            cancel.OnClick += (form, but) =>
            {
                UI.This.Top = null;
            };
            

        }
    }
}
