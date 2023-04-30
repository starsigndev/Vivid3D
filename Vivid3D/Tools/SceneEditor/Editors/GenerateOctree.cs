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
    public partial class GenerateOctree : Form
    {
        public Vivid.Scene.Scene Scene
        {
            get
            {
                return _Scene;
            }
            set
            {
                _Scene = value;
                cNode.Text = "Nodes:" + _Scene.NodeCount;
                cVerts.Text = "Vertices:" + _Scene.VertexCount;
                cTris.Text = "Tris:" + _Scene.TriCount;

            }
        }

       

        private Vivid.Scene.Scene _Scene;
        public static Vivid.Acceleration.Octree.ASOctree OcScene;
        public GenerateOctree()
        {
            InitializeComponent();
            if (SceneEditor.EditSceneOT != null)
            {
                OcScene = SceneEditor.EditSceneOT;
                //ocNodes.Text = "Nodes:" + OcScene

                ocLeafs.Text = "Leafs:" + OcScene.LeafCount();

            }

        }

        private void Click_GenerateOctree(object sender, EventArgs e)
        {
            ocStatus.Text = "Building.";
            Invalidate();
            //OcNode.VertexLimit = (int)(leafVertices.Value);
            Vivid.Acceleration.Octree.ASOctree.VertexLeafLimit = (int)leafVertices.Value;
            OcScene = new Vivid.Acceleration.Octree.ASOctree(_Scene);
            OcScene.InitializeVisibility();
            ocStatus.Text = "Built.";
            SceneEditor.EditSceneOT = OcScene;

            Invalidate();

            //ocNodes.Text = "Nodes:" + OcScene.NodeCount;
            ocLeafs.Text = "Leafs:" + OcScene.LeafCount();


        }
        public void Invalid()
        {
            ocStatus.Text = "Needs Building.";
        }

        private void LeafVerts_Changed(object sender, EventArgs e)
        {
            OcNode.VertexLimit = (int)(leafVertices.Value);
            Invalid();

        }
    }
}
