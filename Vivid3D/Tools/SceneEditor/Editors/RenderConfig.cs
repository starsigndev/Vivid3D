using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Editor.SceneEditor;
namespace Editor.Editors
{
    public partial class RenderConfig : Form
    {
        public RenderConfig()
        {
            InitializeComponent();
        }

        private void CB_Bloom(object sender, EventArgs e)
        {

            FinalRender.BloomOn = cbBloom.Checked;

        }

        private void cbLightShafts_CheckedChanged(object sender, EventArgs e)
        {
                FinalRender.LightShaftsOn = cbLightShafts.Checked;
        }
    }
}
