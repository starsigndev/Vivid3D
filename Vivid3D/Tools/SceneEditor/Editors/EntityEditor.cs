using Vivid.Scene;
using Vivid.Meshes;
using Vivid.Physx;
using Vivid.Texture;
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
    public partial class EntityEditor : Form
    {
        public Entity CurrentEntity
        {
            get
            {
                return _CurrentEntity;
            }
            set
            {
                FromEntity(value);
                _CurrentEntity = value;
            }
        }

        public Mesh CurrentMesh = null;

        private void FromEntity(Entity value)
        {
            entityName.Text = value.Name;
            entType.Text = value.NodeType;
            pxType.SelectedIndex = (int)value.BodyKind;
            cbDynamicType.SelectedIndex = (int)value.EntityType;
            cbMesh.Items.Clear();

            for (int i = 0; i < value.Meshes.Count; i++)
            {

                var mesh = value.Meshes[i];

                cbMesh.Items.Add("Mesh:" + i);

            }

            SetMesh(value.Meshes[0]);
            cbMesh.SelectedIndex = 0;

        }

        private Entity _CurrentEntity;

        public void SetMesh(Mesh mesh)
        {
            CurrentMesh = mesh;
            Bitmap cmap = new Bitmap(mesh.Material.ColorMap.Path);
            cmap = new Bitmap(cmap, panColor.Width, panColor.Height);
            panColor.BackgroundImage = cmap;

            Bitmap nmap = new Bitmap(mesh.Material.NormalMap.Path);
            nmap = new Bitmap(nmap, panColor.Width, panColor.Height);
            panNormal.BackgroundImage = nmap;

            Bitmap smap = new Bitmap(mesh.Material.SpecularMap.Path);
            smap = new Bitmap(smap, panColor.Width, panColor.Height);
            panSpec.BackgroundImage = smap;

            Edit = false;
            //nDiffR.Value = (decimal)mesh.Material.Diffuse.x;
            // nDiffG.Value = (decimal)mesh.Material.Diffuse.y;
            //nDiffB.Value = (decimal)mesh.Material.Diffuse.z;



            //nSpecR.Value = (decimal)mesh.Material.Specular.x;
            // nSpecG.Value = (decimal)mesh.Material.Specular.y;
            // nSpecB.Value = (decimal)mesh.Material.Specular.z;



            Edit = true;



        }

        public EntityEditor()
        {
            InitializeComponent();
            pxType.Items.Add("Box");
            pxType.Items.Add("Sphere");
            pxType.Items.Add("Static Mesh");
            pxType.Items.Add("Convex");
            pxType.Items.Add("None");
            pxType.Items.Add("FPS");



        }

        private void EntityEditor_Load(object sender, EventArgs e)
        {

        }

        private void pxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentEntity != null)
            {
                CurrentEntity.BodyKind = (BodyType)pxType.SelectedIndex;

            }
        }

        private void entityName_TextChanged(object sender, EventArgs e)
        {
            if (CurrentEntity != null)
            {
                CurrentEntity.Name = entityName.Text;
                global::SceneEditor.SceneEditor.Rebuild();
            }
        }

        private void cbMesh_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "png";

            openFileDialog1.ShowDialog();

            var file = openFileDialog1.FileName;

            if (File.Exists(file))
            {

                Texture2D tex = new Texture2D(file);
                CurrentMesh.Material.ColorMap = tex;
                SetMesh(CurrentMesh);


            }
            else
            {
                MessageBox.Show("File does not exist.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "png";

            openFileDialog1.ShowDialog();

            var file = openFileDialog1.FileName;

            if (File.Exists(file))
            {

                Texture2D tex = new Texture2D(file);
                CurrentMesh.Material.NormalMap = tex;
                SetMesh(CurrentMesh);


            }
            else
            {
                MessageBox.Show("File does not exist.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "png";

            openFileDialog1.ShowDialog();

            var file = openFileDialog1.FileName;

            if (File.Exists(file))
            {

                Texture2D tex = new Texture2D(file);
                CurrentMesh.Material.SpecularMap = tex;
                SetMesh(CurrentMesh);


            }
            else
            {
                MessageBox.Show("File does not exist.");
            }
        }

        bool Edit = false;
        private void nDiffR_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Diffuse.x = (float)nDiffR.Value;


        }

        private void nDiffG_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Diffuse.y = (float)nDiffG.Value;
        }

        private void nDiffB_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Diffuse.z = (float)nDiffB.Value;
        }

        private void nSpecR_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Specular.x = (float)nSpecR.Value;
        }

        private void nSpecG_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Specular.y = (float)nSpecG.Value;
        }

        private void nSpecB_ValueChanged(object sender, EventArgs e)
        {
            if (!Edit) return;
            //CurrentMesh.Material.Specular.z = (float)nSpecB.Value;
        }

        private void entType_TextChanged(object sender, EventArgs e)
        {
            if (CurrentEntity == null) return;
            CurrentEntity.NodeType = entType.Text;
        }

        private void Change_DynamicType(object sender, EventArgs e)
        {
            if (CurrentEntity == null) return;
            CurrentEntity.EntityType = (EntityType)cbDynamicType.SelectedIndex;

        }
    }
}
