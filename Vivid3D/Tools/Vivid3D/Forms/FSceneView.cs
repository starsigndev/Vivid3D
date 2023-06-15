using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.State;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FSceneView : IWindow
    {

        public Vivid.Scene.Scene CurrentScene;
        public Vivid.Scene.Camera EditCam;
        public IRenderTarget SceneRT;

        public FSceneView() : base("Scene Editor")
        {
            //Set()
            SceneRT = new IRenderTarget();
            Content.AddForm(SceneRT);
            Content.Scissor = false;

            CurrentScene = Editor.CurrentScene;
            SceneRT.ActionRender += SceneRT_RenderScene;
            var grid = Features.Grid.CreateGrid();
            CurrentScene.MeshLines.Add(grid);
            EditCam = Editor.EditCamera;

            EditCam.Position = new OpenTK.Mathematics.Vector3(0, 5, 5);
            EditCam.SetRotation(0, 0, 0);

        }

        private void SceneRT_RenderScene(int w, int h)
        {
            var scene = Editor.CurrentScene;


            scene.RenderLines();
            scene.RenderShadows();
            scene.Render();
            GLState.State = CurrentGLState.Draw;
          //  scene.RenderShadows();
        //    scene.Render();
          

            //throw new NotImplementedException();
        }

        public override void AfterSet()
        {
            base.AfterSet();
            SceneRT.Set(0,0,Content.Size.w, Content.Size.h);
        }


    }
}
