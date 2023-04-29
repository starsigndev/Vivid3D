using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Mathematics;

namespace SceneEditor.Editors
{
    public partial class LightEditor : Form
    {
        public Light CurrentLight
        {
            get
            {
                return _CurrentLight;
            }
            set
            {
                FromLight(value);

                _CurrentLight = value;

            }
        }

        private void FromLight(Light value)
        {

            //diffRed.Value = (decimal)value.Diffuse.x;
            //diffGreen.Value = (decimal)value.Diffuse.y;
            //diffBlue.Value = (decimal)value.Diffuse.z;

            //specRed.Value = (decimal)value.Specular.x;
            //specGreen.Value = (decimal)value.Specular.y;
            //specBlue.Value = (decimal)value.Specular.z;

            lightRange.Value = (decimal)value.Range;

            shadowsEnabled.Checked = value.CastShadows;

            lightType.SelectedIndex = (int)value.Type;

            lightName.Text = value.Name;
            updateChanges = true;
        }


        Light _CurrentLight = null;
        public LightEditor()
        {
            InitializeComponent();
            //lightType
            lightType.Items.Add("Point");
            lightType.Items.Add("Spot");
            lightType.Items.Add("Directional");


        }

        private void LightEditor_Load(object sender, EventArgs e)
        {

        }

        public void ToLight()
        {
            if (updateChanges == false) return;
            CurrentLight.Diffuse = new Vector3((float)diffRed.Value, (float)diffGreen.Value, (float)diffBlue.Value);
            CurrentLight.Specular = new Vector3((float)specRed.Value, (float)specGreen.Value, (float)specBlue.Value);
            CurrentLight.Range = (float)lightRange.Value;
            CurrentLight.Name = (string)lightName.Text;
            CurrentLight.Type = (LightType)lightType.SelectedIndex;
            CurrentLight.CastShadows = shadowsEnabled.Checked;
            global::SceneEditor.SceneEditor.Rebuild();

        }
        bool updateChanges = false;
        private void diffRed_ValueChanged(object sender, EventArgs e)
        {

            ToLight();
        }

        private void diffGreen_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void diffBlue_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void specRed_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void specGreen_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void specBlue_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void lightRange_ValueChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void lightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void shadowsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ToLight();
        }

        private void lightName_TextChanged(object sender, EventArgs e)
        {
            ToLight();
        }
    }
}
