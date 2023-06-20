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
            Display.Scissor = true;
            //Display.ScissorSelf = true;


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

            var enabled = AddCheckBox("Enabled?");

            enabled.Checked = CurrentNode.Enabled;

            enabled.OnChecked += (form, check) =>
            {
                CurrentNode.Enabled = check;
            };

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


            var rot = AddVector3(node.EulerRotation,"Rotation");

            rot[0].Number.OnChange += (box, val) =>
            {
                node.EulerRotation = new Vector3(box.Value, node.EulerRotation.Y, node.EulerRotation.Z);
            };

            rot[1].Number.OnChange += (box, val) =>
            {
                node.EulerRotation = new Vector3(node.EulerRotation.X, box.Value, node.EulerRotation.Z);
            };

            rot[2].Number.OnChange += (box, val) =>
            {
                node.EulerRotation = new Vector3(node.EulerRotation.X, node.EulerRotation.Y, box.Value);
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

            if(node is Light)
            {

                var light = node as Light;

                var shadows = AddCheckBox("Cast Shadows?");

                shadows.Checked = light.CastShadows;

                shadows.OnChecked += (form, val) =>
                {

                    light.CastShadows = val;

                };

                var light_type = AddEnum(typeof(LightType));

                light_type.CurrentSelection = (int)light.Type;

                light_type.OnSelected += (sel) =>
                {

                    light.Type = (LightType)light_type.CurrentSelection;

                };

                var light_diff = AddVector3(light.Diffuse, "Diffuse", 0.05f);

                light_diff[0].Number.OnChange += (box, val) =>
                {
                    light.Diffuse = new Vector3(box.Value,light.Diffuse.Y,light.Diffuse.Z);
                };
                light_diff[1].Number.OnChange += (box, val) =>
                {
                    light.Diffuse = new Vector3(light.Diffuse.X,box.Value,  light.Diffuse.Z);
                };

                light_diff[2].Number.OnChange += (box, val) =>
                {
                    light.Diffuse = new Vector3(light.Diffuse.X,light.Diffuse.Y,box.Value);
                };


                var light_spec = AddVector3(light.Specular, "Specular",0.05f);


                light_spec[0].Number.OnChange += (box, val) =>
                {
                    light.Specular = new Vector3(box.Value, light.Specular.Y, light.Specular.Z);
                };

                light_spec[1].Number.OnChange += (box, val) =>
                {
                    light.Specular = new Vector3(light.Specular.X, box.Value, light.Specular.Z);
                };

                light_spec[2].Number.OnChange += (box, val) =>
                {
                    light.Specular = new Vector3(light.Specular.X, light.Specular.Y, box.Value);
                };

                var light_range = AddFloat(light.Range, "Range",5.0f);

                light_range.Number.OnChange += (box, val) =>
                {
                    if (box.Value > 1.0f)
                    {
                        light.Range = box.Value;
                    }
                };


            }

            //int a = 5;

        }

        public INumericBox AddFloat(float def, string name,float interval=1.0f)
        {
            var nlab = new ILabel().Set(10, CurrentY + 4, 5, 5, name) as ILabel;


            //var lx = new ILabel().Set(87, CurrentY + 4, 20, 20, "def");
            var lx_num = new INumericBox().Set(100, CurrentY, 80, 25) as INumericBox;

            lx_num.Increment = interval;
            lx_num.Number.Increment = interval;

            Display.AddForms(nlab, lx_num);

            lx_num.Number.Value = def;

            CurrentY += 45;
            return lx_num;
        }

        public IEnumSelector AddEnum(Type type)
        {

            var form = new IEnumSelector(type);
            form.Set(15, CurrentY, 120, 25, "");
           
            Display.AddForm(form);

            CurrentY += 45;
            return form;

        }

        public ICheckBox AddCheckBox(string name)
        {

            var cb = new ICheckBox(name);

            cb.Set(15, CurrentY, 14, 14, cb.Text);

            CurrentY += 35;

            Display.AddForm(cb);

            return cb;

        }

        public IForm AddTextBox(string text)
        {

            var lab = new ILabel().Set(15, CurrentY, 40, 25, text) as ILabel;
            var edit = new ITextBox().Set(70, CurrentY-4, 280, 25) as ITextBox;

            CurrentY += 45;

            Display.AddForms(lab, edit);

            return edit;

        }

        public INumericBox[] AddVector3(Vector3 vec,string name,float interval=1.0f)
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
            lx_num.Increment = ly_num.Increment = lz_num.Increment = interval;
            lx_num.Number.Increment = interval;
            ly_num.Number.Increment = interval;
            lz_num.Number.Increment = interval;

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
