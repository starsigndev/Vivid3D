using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IMessageBox : IWindow
    {
        ITextArea Msg;
        IButton OK;
        public IMessageBox(string title,string message) : base(title)
        {

            Set(0, 0, 300, 230,title);

            OK = new IButton().Set(5, 180, 290, 25, "OK") as IButton;

            Msg = new ITextArea(false).Set(5, 10, 290, 160) as ITextArea;

            OK.OnClick += (form, data) =>
            {
                UI.This.Top = null;
            };


            Msg.SetText(message);

            Content.AddForms(OK,Msg);

            if(UI.This.Top != null)
            {
                UI.This.NextTop = UI.This.Top;
                UI.This.Top = this;
            }

        }

    }
}
