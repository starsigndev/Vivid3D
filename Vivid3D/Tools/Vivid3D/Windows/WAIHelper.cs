using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI;
using Vivid.AI;
using Vivid.UI.Forms;

namespace Vivid3D.Windows
{
    public class WAIHelper : IWindow
    {
        public ITextBox Ask;
        public ITextArea Response;
        public IButton AskBut;

        AIMind Mind = null;
        public WAIHelper() : base("AI Helper")
        {
            Set(200, 200, 800, 420, "AI Helper");
            Mind = new AIMind("general", "User:");

            Ask = new ITextBox().Set(5, 10, 720, 25, "") as ITextBox;
            Response = new ITextArea().Set(10, 50, 770, 335, "") as ITextArea;

            AskBut = new IButton().Set(730, 10, 60, 25, "Ask") as IButton;


            Content.AddForms(Ask,Response,AskBut);

            AskBut.OnClick += Ask_OnClick;

            //string res = Mind.AskQuestion("Who are you?");
            //Console.WriteLine("Response:" + res);
            //res = Mind.AskQuestion("How can I load a scene?");
            //Console.WriteLine("Response 2:" + res);

        }

        private void Ask_OnClick(IForm form, object data = null)
        {
            string quest = Ask.Text;
            string answer = Mind.AskQuestion(quest);


            Response.AddLine(answer);

            //throw new NotImplementedException();

        }
    }
}
