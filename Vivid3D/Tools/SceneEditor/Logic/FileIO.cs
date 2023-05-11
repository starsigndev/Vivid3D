using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Editor.SceneEditor;
namespace Editor.Logic
{
    public class FileIO
    {
        public static void LoadOctreeScene()
        {
            OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Octree Scene Files (*.ocscene)|*.ocscene|All Files (*.*)|*.*";
            OpenFile.DefaultExt = "ocscene";
            OpenFile.ShowDialog();
            if (File.Exists(OpenFile.FileName) == false)
            {
                MessageBox.Show("File does not exist.");
                return;
            }

            EditSceneOT = new Vivid.Acceleration.Octree.ASOctree(EditScene,OpenFile.FileName);
            //EditSceneOT.InitializeVisibility();


        }
        public static void SaveOctreeScene()
        {

            SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Octree Scene Files (*.ocscene)|*.ocscene|All Files (*.*)|*.*";
            SaveFile.DefaultExt = "ocscene";
            SaveFile.ShowDialog();
            //EditScene.SaveOctree(SaveFile.FileName);
            try
            {
                EditSceneOT.Save(SaveFile.FileName);
            }
            catch (Exception e)
            {

                MessageBox.Show("Failed to save Octree scene.");

            }
        }
        public static void SaveScene()
        {

            SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Scene Files (*.scene)|*.scene|All Files (*.*)|*.*";
            SaveFile.DefaultExt = "scene";
            SaveFile.ShowDialog();
            EditScene.Save(SaveFile.FileName);

        }

        public static void OpenScene()
        {

            OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Scene Files (*.scene)|*.scene|All Files (*.*)|*.*";
            OpenFile.DefaultExt = "scene";
            OpenFile.ShowDialog();
            if (File.Exists(OpenFile.FileName) == false)
            {
                MessageBox.Show("File does not exist.");
                return;
            }
            EditScene.Load(OpenFile.FileName);

        }

    }
}
