using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.PostProcesses;
using Vivid.UI;
using Vivid.UI.Forms;
using OpenTK.Mathematics;

namespace Vivid3D.Windows
{
    public class WPostProcessing : IWindow
    {

        public static List<PostProcess> PostProcesses = new List<PostProcess>();
        public IList PPList;

        public WPostProcessing() : base("Post Processing")
        {
            Set(200, 200, 500, 500, "Post Processing");
            PPList = new IList().Set(4, 6, 160, 500-28) as IList;
            Content.AddForm(PPList);

            foreach(var pp in PostProcesses)
            {

                string name = pp.ToString();
                name = name.Substring(name.LastIndexOf(".") + 1);

                var item = PPList.AddItem(name);
                item.Data = pp;
                item.Action += (act,idx,data) =>
                {
                    SetPP(pp);
                };

            }


            
            
        }
        public int CurrentY = 10;

        public void SetPP(PostProcess pp)
        {

            Content.Forms.Clear();
            Content.AddForm(PPList);
            CurrentY = 10;

            //int bb = 6;
            var lab = new ILabel().Set(175, CurrentY, 20, 20, "PostProcess:" +pp.GetType().Name);
            Content.AddForm(lab);
            var mod = pp;

            CurrentY += 35;

            Vivid.Reflection.ObjectState tmp_state = new Vivid.Reflection.ObjectState(pp);

            foreach (var prop in tmp_state._propertyValues.Keys)
            {

                Console.WriteLine("Prop:" + prop.Name);
                Console.WriteLine("Type:" + prop.PropertyType.ToString());

                if (prop.PropertyType.ToString().Contains("Mathematics.Vector3"))
                {

                    var v = AddVector3((Vector3)prop.GetValue(mod), prop.Name);

                    v[0].Number.OnChange += (tb, val) =>
                    {
                        Vector3 orig = (Vector3)prop.GetValue(mod);
                        prop.SetValue(mod, new Vector3(tb.Value, orig.Y, orig.Z));

                    };

                    //var v = AddVector3((Vector3)prop.GetValue(mod), prop.Name);

                    v[1].Number.OnChange += (tb, val) =>
                    {
                        Vector3 orig = (Vector3)prop.GetValue(mod);
                        prop.SetValue(mod, new Vector3(orig.X, tb.Value, orig.Z));

                    };


                    v[2].Number.OnChange += (tb, val) =>
                    {
                        Vector3 orig = (Vector3)prop.GetValue(mod);
                        prop.SetValue(mod, new Vector3(orig.X, orig.Y, tb.Value));

                    };

                }

                if (prop.PropertyType.ToString().Contains("System.Single"))
                {
                    var f = AddFloat((float)prop.GetValue(mod), prop.Name,0.02f);

                    f.Number.OnChange += (tb, val) =>
                    {

                        prop.SetValue(mod, f.Number.Value);

                    };


                }

                if (prop.PropertyType.ToString().Contains("System.String"))
                {
                    var tb = AddTextBox(prop.Name);
                    tb.Text = (string)prop.GetValue(mod);



                    tb.OnChange += (tb, val) =>
                    {

                        prop.SetValue(mod, val);

                    };


                }

                if (prop.PropertyType.ToString().Contains("System.Boolean"))
                {
                    var cb = AddCheckBox(prop.Name);

                    cb.Checked = (bool)prop.GetValue(mod);

                    cb.OnChecked += (b, val) =>
                    {
                        prop.SetValue(mod, val);
                    };

                }

                if (prop.PropertyType.ToString().Contains("Int32") || prop.PropertyType.ToString().Contains("Int64"))
                {

                    var nb = AddFloat(float.Parse(prop.GetValue(mod).ToString()), prop.Name);

                    nb.Number.OnChange += (b, val) =>
                    {
                        if (val.ToString() == "" || val.ToString() == "-")
                        {
                            prop.SetValue(mod, 0);
                        }
                        else
                        {
                            prop.SetValue(mod, System.Int32.Parse(val.ToString()));
                        }
                    };

                }


            }
        }

        public INumericBox AddFloat(float def, string name, float interval = 1.0f)
        {
            var nlab = new ILabel().Set(175, CurrentY + 4, 5, 5, name) as ILabel;


            //var lx = new ILabel().Set(87, CurrentY + 4, 20, 20, "def");
            var lx_num = new INumericBox().Set(295, CurrentY, 80, 25) as INumericBox;

            lx_num.Increment = interval;
            lx_num.Number.Increment = interval;

            Content.AddForms(nlab, lx_num);

            lx_num.Number.Value = def;

            CurrentY += 40;
            return lx_num;
        }

        public IEnumSelector AddEnum(Type type)
        {

            var form = new IEnumSelector(type);
            form.Set(15, CurrentY, 120, 20, "");

            Content.AddForm(form);

            CurrentY += 45;
            return form;

        }

        public ICheckBox AddCheckBox(string name)
        {

            var cb = new ICheckBox(name);

            cb.Set(15, CurrentY, 14, 14, cb.Text);

            CurrentY += 35;

            Content.AddForm(cb);

            return cb;

        }

        public ITextBox AddTextBox(string text)
        {

            var lab = new ILabel().Set(15, CurrentY, 40, 25, text) as ILabel;
            var edit = new ITextBox().Set(100, CurrentY - 4, 240, 25) as ITextBox;

            CurrentY += 45;

            Content.AddForms(lab, edit);

            return edit;

        }

        public INumericBox[] AddVector3(Vector3 vec, string name, float interval = 1.0f)
        {


            var nlab = new ILabel().Set(10, CurrentY + 4, 5, 5, name) as ILabel;

            var lx = new ILabel().Set(87, CurrentY + 4, 20, 20, "X");
            var lx_num = new INumericBox().Set(100, CurrentY, 80, 25) as INumericBox;

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

            Content.AddForms(nlab, lx, lx_num, ly, ly_num, lz, lz_num);

            CurrentY += 35;

            INumericBox[] forms = new INumericBox[3];

            forms[0] = lx_num;
            forms[1] = ly_num;
            forms[2] = lz_num;

            return forms;

        }
    }
}
