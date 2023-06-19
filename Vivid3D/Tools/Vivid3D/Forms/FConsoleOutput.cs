using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FConsoleOutput : IWindow
    {
        public static FConsoleOutput Console;

        public ITextArea Output;

        public FConsoleOutput() : base("Output Console")
        {
            Output = new ITextArea();
            Output.Text = " ";
            Content.AddForm(Output);
            Console = this;
            LogMessage("Welcome to Vivid3D");

        }

        public override void AfterSet()
        {
            base.AfterSet();
            Output.Set(0, 0, Content.Size.w-12, Content.Size.h-12, Output.Text);
        }

        public static void LogMessage(string text)
        {
            //Console.Output.Text = Console.Output.Text + text + "\n";
            Console.Output.AddLine(text);
        }

    }
}
