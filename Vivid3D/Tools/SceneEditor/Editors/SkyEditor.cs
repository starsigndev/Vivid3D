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
namespace SceneEditor.Editors
{
    public partial class SkyEditor : Form
    {
        public SkyEditor()
        {
            InitializeComponent();
        }

        private void cbProceduralSky_CheckedChanged(object sender, EventArgs e)
        {
            if (cbProceduralSky.Checked)
            {

                EditScene.AddProcSky();

            }
            else
            {
                EditScene.RemoveProcSky();
            }

        }
    }
}
