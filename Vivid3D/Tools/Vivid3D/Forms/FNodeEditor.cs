using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI;
using Vivid.UI.Forms;
using OpenTK.Mathematics;
using System.Security.Cryptography;
using System.Windows.Markup;

namespace Vivid3D.Forms
{
    public class FNodeEditor : IWindow
    {

        public static FNodeEditor Editor
        {
            get;
            set;
        }

        private IScrollArea Display
        {
            get;
            set;
        }

        private int CurrentY = 0;

        public Node CurrentNode = null;

        public FNodeEditor() : base("Node Editor")
        {

            Display = new IScrollArea();
            Content.AddForm(Display);
            Editor = this;

        }

        public override void AfterSet()
        {
            base.AfterSet();
            Display.Set(0, 0, Content.Size.w, Content.Size.h, "");
        }

        public void Reset()
        {

        }

        public void UpdateIf(Node node)
        {
            if(node == CurrentNode)
            {
                SetNode(node);
            }
        }

        public void SetNode(Node node)
        {

            CurrentNode = node;

            Display.ClearForms();
            CurrentY = 20;

            var name = AddTextBox("Name") as ITextBox;

            name.Text = node.Name;

            name.OnChange += (box, val) =>
            {
                node.Name = val;
                Vivid3D.Editor.UpdateSceneGraph();
            };


            var pos = AddVector3(node.Position,"Position");

            pos[0].Number.OnChange += (box, val) =>
            {
                node.Position = new Vector3(box.Value, node.Position.Y, node.Position.Z);
            };

            pos[1].Number.OnChange += (box, val) =>
            {
                node.Position = new Vector3(node.Position.X, box.Value, node.Position.Z);
            };

            pos[2].Number.OnChange += (box, val) =>
            {
                node.Position = new Vector3(node.Position.X,node.Position.Y,box.Value);
            };


            var scale = AddVector3(node.Scale, "Scale");

            scale[0].Number.OnChange += (box, val) =>
            {

                node.Scale = new Vector3(box.Value, node.Scale.Y, node.Scale.Z);

            };

            scale[1].Number.OnChange += (box, val) =>
            {

                node.Scale = new Vector3(node.Scale.X, box.Value, node.Scale.Z);

            };

            scale[2].Number.OnChange += (box, val) =>
            {

                node.Scale = new Vector3(node.Scale.X, node.Scale.Y,box.Value);

            };

            //int a = 5;

        }

        public IForm AddTextBox(string text)
        {

            var lab = new ILabel().Set(15, CurrentY, 40, 25, text) as ILabel;
            var edit = new ITextBox().Set(70, CurrentY-4, 280, 25) as ITextBox;

            CurrentY += 55;

            Display.AddForms(lab, edit);

            return edit;

        }

        public INumericBox[] AddVector3(Vector3 vec,string name)
        {


            var nlab = new ILabel().Set(10, CurrentY+4, 5, 5, name) as ILabel;

            var lx = new ILabel().Set(87, CurrentY+4, 20, 20, "X");
            var lx_num = new INumericBox().Set(100,CurrentY,80,25) as INumericBox;
            
            var ly = new ILabel().Set(185, CurrentY + 4, 20, 20, "Y");
            var ly_num = new INumericBox().Set(198, CurrentY, 80, 25) as INumericBox;

            var lz = new ILabel().Set(185 + 98, CurrentY + 4, 20, 20, "Z");
            var lz_num = new INumericBox().Set(185 + 98 + 13, CurrentY, 80, 25) as INumericBox;


            lx_num.Number.Value = vec.X;
            ly_num.Number.Value = vec.Y;
            lz_num.Number.Value = vec.Z;

            lx_num.Number.OnChange += (tb, val) =>
            {
                vec.X = lx_num.Number.Value;
            };

            Display.AddForms(nlab,lx, lx_num, ly, ly_num, lz, lz_num);

            CurrentY += 35;

            INumericBox[] forms = new INumericBox[3];

            forms[0] = lx_num;
            forms[1] = ly_num;
            forms[2] = lz_num;

            return forms;

        }

    }
}
