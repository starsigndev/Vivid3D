using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI;
using Vivid.AI;
using Vivid.UI.Forms;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Vivid3D.Windows
{
    public class WAIHelper : IWindow
    {
        public ITextBox Ask;
        public ITextArea Response;
        public IButton AskBut;
        bool Answering = false;

        AIMind Mind = null;

    
        public WAIHelper() : base("AI Helper")
        {
            Set(200, 200, 1200, 590, "AI Helper");
            Mind = new AIMind("general", "User:");

            Ask = new ITextBox().Set(5, 10, 1120, 25, "") as ITextBox;

            Ask.OnEnterPressed += (form, txt) =>
            {
                Ask_OnClick(this, txt);
            };

            Response = new ITextArea().Set(10, 50, 1160, 505, "") as ITextArea;

            AskBut = new IButton().Set(1130, 10, 60, 25, "Ask") as IButton;


            Content.AddForms(Ask,Response,AskBut);

            AskBut.OnClick += Ask_OnClick;

            //string res = Mind.AskQuestion("Who are you?");
            //Console.WriteLine("Response:" + res);
            //res = Mind.AskQuestion("How can I load a scene?");
            //Console.WriteLine("Response 2:" + res);

        }

        private void Ask_OnClick(IForm form, object data = null)
        {
            if (Mind.Answering)
            {
                return;
            }
            string quest = Ask.Text;
            string answer = Mind.AskQuestion(quest);

            Answering = true;

            Response.AddLine(quest);
            Response.AddLine(answer);



            Ask.Text = "";

            //throw new NotImplementedException();

        }
        string cur_line = "";
        public override void OnUpdate()
        {
            //base.OnUpdate();
            if (Answering)
            {

                if (Mind.Answered)
                {
                    Answering = false;
                    return;
                }
                var np = Mind.GetResponses();
                foreach(var r in np)
                {
                    //cur_line = cur_line;

                    Response.AddText(r);
                    if(r.Contains("\n"))
                    {
                        Response.AddLine("");
                    }
                    if (UI.SystemFont.StringWidth(Response.Lines[Response.Lines.Length - 1]) > Response.Size.w-80)
                    {
                        Response.AddLine("");
                    }
                    //if (Response.Lines[Response.Lines-1].Length>())

                }

            }

        }
    }
}
