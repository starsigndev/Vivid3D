using Vivid.Scene;
using Vivid.Importing;
using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Mathematics;
using static SceneEditor.SceneEditor;

namespace SceneEditor.Logic
{
    public class Features
    {

        public static void ImportModel()
        {

            //pasteInstanceToolStripMenuIte
            OpenFile.Filter = "3D Files (*.fbx)|*.fbx|All Files (*.*)|*.*";
            OpenFile.DefaultExt = "fbx";

            OpenFile.ShowDialog();

            var file = OpenFile.FileName;
            if (File.Exists(file) == false)
            {
                MessageBox.Show("File does not exist.");
                return;
            }

            var model = Importer.ImportEntity<Entity>(file);

            EditScene.AddNode(model);
            This.RebuildTree();


        }

        public static void FocusOnNode()
        {
            if (CurrentNode == null)
            {

                return;

            }


            Vivid.Scene.BoundingBox bb = null;


            bb = CurrentNode.Bounds;
            bool less = false;

            if (bb == null)
            {
                bb = new Vivid.Scene.BoundingBox(new Vector3(-1, -1, 1), new Vector3(1, 1, 1));
                less = true;
            }

            float ox, oy, oz;

            ox = 0;
            oy = 2.5f + bb.Max.Y * 0.5f;
            oz = (bb.Min.Z + (-bb.Max.Y));// 5.5f;

            //EditCam.SetRotation(-45, 0, 0);
            if (less)
            {
                CamPitch = -25;
                oz = oz - 4.0f;
            }
            else
            {
                CamPitch = -4;
            }
            CamYaw = 0;



            EditCam.SetRotation(CamPitch, CamYaw, 0);

            var np = new Vector3(CurrentNode.Position.X + ox, CurrentNode.Position.Y + oy, CurrentNode.Position.Z - oz);

            EditCam.Position = np;

        }

        public static void AlignToCamera()
        {
            if (CurrentNode != null)
            {
                CurrentNode.Position = EditCam.Position;
                CurrentNode.Rotation = EditCam.Rotation;
            }
        }

        public static void AlignToObject()
        {
            if (CurrentNode != null)
            {
                EditCam.Position = CurrentNode.Position;
                EditCam.Rotation = CurrentNode.Rotation;
            }
        }

    }
}
