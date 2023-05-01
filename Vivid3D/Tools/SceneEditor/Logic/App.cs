using Vivid.App;
using Vivid.Draw;
using Vivid.Importing;
using Vivid.Scene;
using Vivid.Texture;
using OpenTK.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static SceneEditor.SceneEditor;
namespace SceneEditor.Logic
{
    public class EditApp
    {

        public static void Init()
        {
            Vivid.Scene.ShaderModules.Shaders.InitShaders();
            draw = new SmartDraw();
            LightIcon = new Texture2D("edit/lighticon.png");
            SpawnIcon = new Texture2D("edit/spawnIcon.png");
            EditScene = new Scene();
            EditGrid = new Entity();
            //    Grid = new Mesh(EditGrid);
            //    EditGrid.AddMesh(Grid);

            //App = new GeminiApp(glControl1.Size.Width, glControl1.Size.Height);
            VividApp.FrameWidth = G_Control.Size.Width;
            VividApp.FrameHeight = G_Control.Size.Height;

            var l1 = new Light();
            l1.Range = 80;
            l1.Position = new Vector3(0, 20, 8);
            //EditScene.Lights.Add(l1);


            GizmoTranslate = Importer.ImportEntity<Entity>("edit/gizmo/translate1.fbx");
            GizmoRotate = Importer.ImportEntity<Entity>("edit/gizmo/rotate1.fbx");
            GizmoScale = Importer.ImportEntity<Entity>("edit/gizmo/scale1.fbx");

            CurrentGizmo = GizmoTranslate;

            Texture2D red, blue, green;

            red = new Texture2D("edit/gizmo/red.png");
            green = new Texture2D("edit/gizmo/green.png");
            blue = new Texture2D("edit/gizmo/blue.png");

            GizmoTranslate.Meshes[0].Material.ColorMap = red;
            GizmoTranslate.Meshes[1].Material.ColorMap = blue;
            GizmoTranslate.Meshes[2].Material.ColorMap = green;


            GizmoRotate.Meshes[0].Material.ColorMap = green;
            GizmoRotate.Meshes[1].Material.ColorMap = blue;
            GizmoRotate.Meshes[2].Material.ColorMap = red;


            GizmoScale.Meshes[0].Material.ColorMap = blue;
            GizmoScale.Meshes[1].Material.ColorMap = red;
            GizmoScale.Meshes[2].Material.ColorMap = green;
            This.CreateGrid();
            EditCam = EditScene.MainCamera;
            EditCam.Position = new Vector3(0, 3, 3);
            EditCam.SetRotation(-25, 0, 0);
            CamPitch = -25;
            //t1 = new Texture2D("tex/t1.jpg");
        }

        public static void New_Scene()
        {
            EditScene = new Scene();
            EditCam = EditScene.MainCamera;
            EditSceneOT = null;
            EditCam.Position = new Vector3(0, 3, 3);
            EditCam.SetRotation(-25, 0, 0);
            CamPitch = -25;
            CamYaw = 0;
        }

    }
}
