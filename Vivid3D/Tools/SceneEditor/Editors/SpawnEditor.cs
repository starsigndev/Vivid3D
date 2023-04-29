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

namespace SceneEditor.Editors
{
    public partial class SpawnEditor : Form
    {
        public SpawnEditor()
        {
            InitializeComponent();
        }

        public SpawnPoint CurrentSpawn
        {
            get;
            set;
        }

        public void FromSpawn()
        {
            Edit = false;
            spawnName.Text = CurrentSpawn.Name;
            spawnIndex.Value = (decimal)CurrentSpawn.Index;
            spawnType.Text = CurrentSpawn.Type;
            Edit = true;
        }
        bool Edit = false;
        private void spawnName_TextChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            CurrentSpawn.Name = spawnName.Text;
        }

        private void spawnIndex_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            CurrentSpawn.Index = (int)spawnIndex.Value;
        }

        private void spawnType_TextChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            CurrentSpawn.Type = spawnType.Text;
        }
    }
}
