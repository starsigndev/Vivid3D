using Vivid.Scene;
using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneEditor.Editors
{
    public partial class PropertyEditor : Form
    {
        public PropertyEditor()
        {
            InitializeComponent();
            propType.Items.Add("String");
            propType.Items.Add("Float");
            propType.Items.Add("Int");
            propType.Items.Add("Vector3");
            propType.Items.Add("Vector4");
            propType.Items.Add("Matrix");
            propType.Items.Add("File Path");
            propType.Items.Add("Texture2D");
            propType.Items.Add("Sound");
            propType.Items.Add("Scene");
            propType.Items.Add("Scene");
        }

        public static Scene EditScene = null;

        private void button1_Click(object sender, EventArgs e)
        {
            var new_prop = new Vivid.Scene.PropertyItem();
            EditScene.Properties.Items.Add(new_prop);
            RebuildUI();
        }
        public void RebuildUI()
        {
            propList.Items.Clear();
            foreach (var item in EditScene.Properties.Items)
            {
                propList.Items.Add(item);
            }
        }

        Vivid.Scene.PropertyItem SelectedItem = null;

        private void propList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   var sel = propList.SelectedIndex;
            var sel = propList.SelectedItem as Vivid.Scene.PropertyItem;

            SelectedItem = sel as Vivid.Scene.PropertyItem;
            if (SelectedItem == null)
            {
                propName.Text = "None Selected";
                propType.SelectedIndex = 0;
                return;
            }
            propName.Text = sel.Name;
            propType.SelectedIndex = (int)SelectedItem.Type;
            RebuildValueEdit();

        }

        private void propName_TextChanged(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {


                SelectedItem.Name = propName.Text;
                RebuildUI();
            }
            else
            {
                propName.Text = "";
            }
        }

        private void propType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;
            SelectedItem.Type = (PropertyType)propType.SelectedIndex;
            RebuildUI();
            RebuildValueEdit();
        }
        Control edit_con = null;
        NumericUpDown edit_x, edit_y, edit_z;
        private void RebuildValueEdit()
        {

            valuePanel.Controls.Clear();

            if (SelectedItem == null)
            {

                propName.Text = "";
                propType.SelectedIndex = 0;

                return;
            }

            edit_con = null;

            switch (SelectedItem.Type)
            {
                case PropertyType.String:

                    edit_con = new TextBox();
                    edit_con.Size = new Size(320, 30);
                    edit_con.Text = SelectedItem.StringValue;
                    edit_con.TextChanged += Edit_con_TextChanged;

                    break;
                case PropertyType.Int:

                    edit_con = new NumericUpDown();
                    edit_con.Size = new Size(80, 30);
                    var nud = edit_con as NumericUpDown;
                    nud.Value = SelectedItem.IntValue;
                    nud.ValueChanged += Nud_ValueChanged;

                    break;
                case PropertyType.Float:

                    edit_con = new NumericUpDown();
                    edit_con.Size = new Size(80, 30);
                    var fnud = edit_con as NumericUpDown;
                    fnud.DecimalPlaces = 4;
                    fnud.Value = (Decimal)SelectedItem.FloatValue;
                    fnud.ValueChanged += Fnud_ValueChanged;

                    break;
                case PropertyType.Vec3:

                    edit_con = new Panel();
                    edit_con.Size = new Size(440, 30);

                    var xl = new Label();
                    var xn = new NumericUpDown();
                    var yl = new Label();
                    var yn = new NumericUpDown();
                    var zl = new Label();
                    var zn = new NumericUpDown();

                    xl.Text = "X";
                    yl.Text = "Y";
                    zl.Text = "Z";
                    xl.Location = new Point(0, 3);
                    yl.Location = new Point(100, 3);
                    zl.Location = new Point(200, 3);
                    xn.Size = new Size(70, 30);
                    yn.Size = new Size(70, 30);
                    zn.Size = new Size(70, 30);


                    xn.Location = new Point(20, 0);
                    yn.Location = new Point(120, 0);
                    zn.Location = new Point(220, 0);
                    xl.Size = new Size(15, 20);
                    yl.Size = new Size(15, 20);
                    zl.Size = new Size(15, 20);
                    edit_con.Controls.AddRange(new Control[] {xl,yl,zl,  xn, yn, zn });

                    xn.DecimalPlaces = 2;
                    yn.DecimalPlaces = 2;
                    zn.DecimalPlaces = 2;
                    
                    xn.Value = (decimal)SelectedItem.Vec3Value.X;
                    yn.Value = (decimal)SelectedItem.Vec3Value.Y;
                    zn.Value = (decimal)SelectedItem.Vec3Value.Z;

                    edit_x = xn;
                    edit_y = yn;
                    edit_z = zn;

                    xn.ValueChanged += Xn_ValueChanged;
                    yn.ValueChanged += Yn_ValueChanged;
                    zn.ValueChanged += Zn_ValueChanged;




                    break;
            }

            if (edit_con == null) return;
            Label vt = new Label();
            vt.Text = SelectedItem.Type.ToString();
            valuePanel.Controls.Add(edit_con);
            valuePanel.Controls.Add(vt);
            edit_con.Location = new Point(55, 17);
            vt.Location = new Point(5, 20);


        }

        private void Zn_ValueChanged(object? sender, EventArgs e)
        {
            SelectedItem.Vec3Value.Z= (float)edit_z.Value;
        }

        private void Yn_ValueChanged(object? sender, EventArgs e)
        {
            SelectedItem.Vec3Value.Y = (float)edit_y.Value;

        }

        private void Xn_ValueChanged(object? sender, EventArgs e)
        {
            // throw new NotImplementedException();
            SelectedItem.Vec3Value.X = (float)edit_x.Value;

        }

        private void Fnud_ValueChanged(object? sender, EventArgs e)
        {
            var float_Edit = edit_con as NumericUpDown;
            SelectedItem.FloatValue = (float)float_Edit.Value;
        }

        private void Nud_ValueChanged(object? sender, EventArgs e)
        {
            var int_Edit = edit_con as NumericUpDown;
            SelectedItem.IntValue = (int)int_Edit.Value;
        }

        private void Edit_con_TextChanged(object? sender, EventArgs e)
        {
            var text_edit = edit_con as TextBox;
            SelectedItem.StringValue = text_edit.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            EditScene.Properties.Items.Remove(SelectedItem);
            propList.Items.Remove(SelectedItem);
            SelectedItem = null;
            RebuildUI();
            RebuildValueEdit();
            

        }
    }
}
