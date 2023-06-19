using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI.Forms;
using Vivid3D.Forms;

namespace Vivid3D.Windows
{
    public class WCsg : IWindow
    {

        public ITextBox Receiver;
        public ITextBox Input;
        public IEnumSelector OperatorType;
        public IButton SelectRecv;
        public IButton SelectInput;
        public Node RecvNode;
        public Node InputNode;
        public IButton CommitOp;

        public WCsg() : base("CSG Tools")
        {
            Set(200, 200, 340, 300,Text);
            Receiver = new ITextBox().Set(10, 20, 170, 25) as ITextBox;
            Input = new ITextBox().Set(10, 55, 170, 25) as ITextBox;
            OperatorType = new IEnumSelector(typeof(Vivid.CSG.CSGGen.CSGOpType)) as IEnumSelector;
            OperatorType.Set(14, 90, 165, 20, "");

            SelectRecv = new IButton().Set(190, 23, 80, 21,"Select") as IButton;
            SelectInput = new IButton().Set(190, 58, 80, 21, "Select") as IButton;

            SelectRecv.OnClick += SelectRecv_OnClick;
            SelectInput.OnClick += SelectInput_OnClick;

            CommitOp = new IButton().Set(8, 253, 180, 25, "Commit Operation") as IButton;
            IButton Cancel = new IButton().Set(200, 253, 120, 25, "Canncel") as IButton;

            CommitOp.OnClick += CommitOp_OnClick;

            Cancel.OnClick += (but, data) =>
            {
                Vivid3DApp.MainUI.Windows.Remove(this);
            };

            Content.AddForms(Receiver, Input, OperatorType,SelectRecv,SelectInput,CommitOp,Cancel);


        }

        private void CommitOp_OnClick(Vivid.UI.IForm form, object data = null)
        {
            //throw new NotImplementedException();
            if (InputNode == null)
            {
                FConsoleOutput.LogMessage("CSG Error 1: No Input Node.");
                return;
            }
            if (RecvNode == null)
            {
                FConsoleOutput.LogMessage("CSG Error 2: No Receiver Node.");
            }

            FConsoleOutput.LogMessage("CSG operation complete. Type:" + OperatorType.ToString());

        }

        private void SelectInput_OnClick(Vivid.UI.IForm form, object data = null)
        {
            if (Editor.SelectedNode == null) return;
            Input.Text = Editor.SelectedNode.Name;
            InputNode = Editor.SelectedNode;
            //throw new NotImplementedException();
        }

        private void SelectRecv_OnClick(Vivid.UI.IForm form, object data = null)
        {
            if (Editor.SelectedNode == null) return;
            Receiver.Text = Editor.SelectedNode.Name;
            RecvNode = Editor.SelectedNode;
            //throw new NotImplementedException();
        }
    }
}
