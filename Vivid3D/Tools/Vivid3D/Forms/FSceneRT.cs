using OpenTK.Windowing.GraphicsLibraryFramework;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.App;
using Vivid.Maths;
using Vivid.State;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FSceneRT : IRenderTarget
    {

        public Vivid.Scene.Scene CurrentScene;
        public Vivid.Scene.Camera EditCam;

        private bool RotateCam = false;

        public Vector3 MoveVector = new Vector3();

        public FSceneRT()
        {

            CurrentScene = Editor.CurrentScene;
            ActionRender += SceneRT_RenderScene;
            PreRender += FSceneRT_PreRender;
            var grid = Features.Grid.CreateGrid();
            CurrentScene.MeshLines.Add(grid);
            EditCam = Editor.EditCamera;

            EditCam.Position = new OpenTK.Mathematics.Vector3(0, 5, 5);
            EditCam.SetRotation(Features.Global.CameraRotation.X, Features.Global.CameraRotation.Y, 0);
            DirectKeys = true;


        }

        private void FSceneRT_PreRender(int w, int h)
        {
            //throw new NotImplementedException();
            OpenTK.Graphics.OpenGL.GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.ScissorTest);
            Editor.CurrentScene.RenderShadows();
        }

        public override void OnKeyDown(Keys keys)
        {
            //base.OnKeyDown(keys);
            switch (keys)
            {
                case Keys.W:
                    MoveVector.Z = -1.0f;
                    break;
                case Keys.A:
                    MoveVector.X = -1.0f;
                    break;
                case Keys.D:
                    MoveVector.X = 1.0f;
                    break;
                case Keys.S:
                    MoveVector.Z = 1.0f;
                    break;
                case Keys.Space:

                    Editor.CurrentScene.Lights[0].Position = Editor.EditCamera.Position;

                    break;
            }

        }

        public override void OnKeyUp(Keys keys)
        {
            //base.OnKeyUp(key);
            switch (keys)
            {
                case Keys.W:
                    MoveVector.Z = 0;
                    break;
                case Keys.A:
                    MoveVector.X = 0;
                    break;
                case Keys.D:
                    MoveVector.X = 0;
                    break;
                case Keys.S:
                    MoveVector.Z = 0;
                    break;
            }
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if(button == MouseID.Right)
            {
                RotateCam = true;
            }
        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            if(button == MouseID.Right)
            {
                RotateCam = false;
            }
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            if (RotateCam)
            {
                Features.Global.CameraRotation.X -= delta.y * Features.Global.RotationSpeed;
                Features.Global.CameraRotation.Y -= delta.x * Features.Global.RotationSpeed;
                EditCam.SetRotation(Features.Global.CameraRotation.X, Features.Global.CameraRotation.Y, 0);
            }
        }

        public override void OnUpdate()
        {
            //base.OnUpdate();
            EditCam.Move(MoveVector.X*Features.Global.MoveSpeed, MoveVector.Y*Features.Global.MoveSpeed, MoveVector.Z*Features.Global.MoveSpeed);
            int a = 5;
        }

        private void SceneRT_RenderScene(int w, int h)
        {
            var scene = Editor.CurrentScene;


            scene.RenderLines();
          
            scene.Render();
            GLState.State = CurrentGLState.Draw;
            //  scene.RenderShadows();
            //    scene.Render();


            //throw new NotImplementedException();
        }

    }
}
