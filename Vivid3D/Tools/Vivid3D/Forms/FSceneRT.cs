using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.App;
using Vivid.Maths;
using Vivid.State;
using Vivid.UI.Forms;
using Vivid.Scene;
using OpenTK.Graphics.OpenGL;
using static Vivid3D.Editor;
using Vivid.Draw;
using Vivid.Texture;
using Vivid.IO;
using Vivid.RenderTarget;
using Vivid.PostProcesses;

namespace Vivid3D.Forms
{
    public class FSceneRT : IRenderTarget
    {

        public static Vector2 RenderSize = new Vector2(0, 0);

        public Vivid.Scene.Scene CurrentScene;
        public Vivid.Scene.Camera EditCam;

        private bool RotateCam = false;

        public Vector3 MoveVector = new Vector3();

        private Vector2 MousePos = new Vector2(0, 0);

        private SmartDraw draw;

        private Texture2D LightIcon, SpawnIcon;

        public RenderTarget2D RenderBuffer;
        public PPRenderer PPRenderer;

        public FSceneRT()
        {

            PPRenderer = new PPRenderer();
            PPRenderer.PP.Add(new PPBloom(Editor.CurrentScene));
            CurrentScene = Editor.CurrentScene;
            ActionRender += SceneRT_RenderScene;
            PreRender += FSceneRT_PreRender;
            OnGetBuffer += () =>
            {
                return RenderBuffer;
            };
            var grid = Features.Grid.CreateGrid();
            CurrentScene.MeshLines.Add(grid);
            EditCam = Editor.EditCamera;
            g_x = g_y = g_z = false;

            EditCam.Position = new OpenTK.Mathematics.Vector3(0, 5, 5);
            EditCam.SetRotation(Features.Global.CameraRotation.X, Features.Global.CameraRotation.Y, 0);
            DirectKeys = true;
            GizmoMove = Vivid.Importing.Importer.ImportEntity<Entity>("edit/gizmo/translate1.fbx");
            GizmoRotate = Vivid.Importing.Importer.ImportEntity<Entity>("edit/gizmo/rotate1.fbx");
            GizmoScale = Vivid.Importing.Importer.ImportEntity<Entity>("edit/gizmo/scale1.fbx");
            LightIcon = new Texture2D("edit/lighticon.png");
            int a = 5;
            CurrentGizmo = GizmoMove;
            draw = new SmartDraw();
            OnDrop += (form, data) =>
            {
                if (Path.GetExtension(data.Path) == ".fbx")
                {

                    var node = Vivid.Importing.Importer.ImportEntity<Entity>(data.Path);
                    Editor.CurrentScene.AddNode(node);
                    Editor.UpdateSceneGraph();

                }
                else if(Path.GetExtension(data.Path)==".node")
                {
                    Editor.Stop();
                    SceneIO io2 = new SceneIO();
                    var node2 = io2.LoadNode(data.Path);
                    Editor.CurrentScene.AddNode(node2);
                    Editor.UpdateSceneGraph();
                }else if(Path.GetExtension(data.Path)==".scene")
                {
                    Editor.Stop();
                    SceneIO io = new SceneIO();
                    var scene = io.LoadScene(data.Path);
                    Editor.SetScene(scene);
                    FConsoleOutput.LogMessage("Loaded scene from:" +data.Path);
                }
            };
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
            if (button == MouseID.Left)
            {
                //Environment.Exit(1);

                //check the current gizmo, if any.

                if (CheckIcons())
                {
                    return;
                }


                if (CurrentGizmo != null)
                {

                    var check = Editor.CamPick(MousePos - new Vector2(RenderPosition.x, RenderPosition.y), CurrentGizmo);
                    if (check == null)
                    {

                    }
                    else
                    {
                        if (check.Hit)
                        {

                            // Mesh bindings for GizmoTranslate
                            //mesh 0 == Z 
                            //mesh 1 == X
                            //mesh 2 = Y

                            //For Rotate
                            // X(Pitch) = 2
                            // Z(Roll) = 1


                            //for scale
                            // x = 0
                            // z = 1
                            // y = 2;


                            int g_arm = -1;

                            for (int i = 0; i < 3; i++)
                            {
                                if (CurrentGizmo.Meshes[i] == check.Mesh)
                                {
                                    g_arm = i;
                                    break;
                                }
                            }

                            switch (Editor.EditMode)
                            {
                                case EditorMode.Translate:

                                    switch (g_arm)
                                    {
                                        case 0:
                                            g_z = true;
                                            break;
                                        case 1:
                                            g_x = true;
                                            break;
                                        case 2:
                                            g_y = true;
                                            break;
                                    }

                                    break;
                                case EditorMode.Rotate:
                                    switch (g_arm)
                                    {
                                        case 0:
                                            g_y = true;
                                            break;
                                        case 1:
                                            g_z = true;
                                            break;

                                        case 2:
                                            g_x = true;
                                            break;
                                    }
                                    break;
                                case EditorMode.Scale:
                                    switch (g_arm)
                                    {
                                        case 0:
                                            g_x = true;
                                            break;
                                        case 1:
                                            g_z = true;
                                            break;
                                        case 2:
                                            g_y = true;
                                            break;
                                    }
                                    break;
                            }

                            return;
                        }
                        //return;
                    }

                }


                //Check the scene for results.
                var res = Editor.CamPick(MousePos - new Vector2(RenderPosition.x, RenderPosition.y));
                if (res == null)
                {
                    Editor.SceneTree.SelectedItem = null;
                    Editor.SelectedNode = null;
                    //CurrentGizmo = null;

                    return;
                }
                if (res.Hit == false)
                {
                    //CurrentGizmo = null;
                    Editor.SceneTree.SelectedItem = null;
                    Editor.SelectedNode = null;
                    return;
                }
                if (res.Hit)
                {


                    switch (Editor.EditMode)
                    {
                        case EditorMode.Translate:
                            CurrentGizmo = GizmoMove;
                            break;
                        case EditorMode.Rotate:
                            CurrentGizmo = GizmoRotate;
                            break;
                        case EditorMode.Scale:
                            CurrentGizmo = GizmoScale;
                            break;
                    }

                    Editor.SetSelectedNode(res.Node);


                    switch (Editor.SpaceMode)
                    {
                        case SpaceMode.Local:
                            CurrentGizmo.Rotation = res.Node.Rotation;
                            break;
                        case SpaceMode.Global:
                            CurrentGizmo.Rotation = Matrix4.Identity;
                            break;
                    }
                }

            }
            if (button == MouseID.Right)
            {
                RotateCam = true;
            }
        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            if (button == MouseID.Right)
            {
                RotateCam = false;
            }
            if (button == MouseID.Left)
            {
                g_x = g_y = g_z = false;
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
            MousePos = GameInput.MousePosition;

            if (Editor.SelectedNode != null)
            {

                switch (Editor.EditMode)
                {
                    case EditorMode.Translate:

                        switch (Editor.SpaceMode)
                        {
                            case SpaceMode.Local:


                                if (g_x)
                                {
                                    //move along the x-axis
                                    Editor.SelectedNode.Move(-delta.x * Editor.GizmoSpeed, 0, 0);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                    //int b = 5;
                                }

                                if (g_y)
                                {
                                    //move along the y-axis.
                                    Editor.SelectedNode.Move(0, -delta.y * Editor.GizmoSpeed, 0);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);

                                }

                                if (g_z)
                                {

                                    //move along the z-axis.
                                    Editor.SelectedNode.Move(0, 0, delta.x * Editor.GizmoSpeed);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                break;
                            case SpaceMode.Global:

                                if (g_x)
                                {
                                    //move along the x-axis
                                    Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(-delta.x * Editor.GizmoSpeed, 0, 0);
                                    //int b = 5;
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                if (g_y)
                                {
                                    //move along the y-axis.
                                    Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, -delta.y * Editor.GizmoSpeed, 0);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                if (g_z)
                                {

                                    //move along the z-axis.
                                    Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, 0, delta.x * Editor.GizmoSpeed);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }



                                break;
                        }

                        break;
                    case EditorMode.Rotate:

                        switch (Editor.SpaceMode)
                        {
                            case SpaceMode.Local:


                                if (g_x)
                                {
                                    Editor.SelectedNode.Turn(delta.x * Editor.GizmoSpeed*5.5f, 0, 0, true);
                                    //move along the x-axis
                                    //Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(-delta.x * Editor.GizmoSpeed, 0, 0);
                                    //int b = 5;
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                if (g_y)
                                {

                                    Editor.SelectedNode.Turn(0, delta.x * Editor.GizmoSpeed*5.5f, 0, true);
                                    int bb = 5;
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                    //move along the y-axis.
                                    //   Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, -delta.y * Editor.GizmoSpeed, 0);

                                }

                                if (g_z)
                                {
                                    Editor.SelectedNode.Turn(0, 0, delta.x * Editor.GizmoSpeed * 5.5f, true); ;
                                    //move along the z-axis.
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                    // Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, 0, delta.x * Editor.GizmoSpeed);

                                }

                                break;
                            case SpaceMode.Global:

                                if (g_x)
                                {
                                    Editor.SelectedNode.Turn(delta.x * Editor.GizmoSpeed * 5.5f, 0, 0, false) ;
                                    //move along the x-axis
                                    //Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(-delta.x * Editor.GizmoSpeed, 0, 0);
                                    //int b = 5;
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                if (g_y)
                                {

                                    Editor.SelectedNode.Turn(0, delta.x * Editor.GizmoSpeed*5.5f, 0, false);
                                    int bb = 5;
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                    //move along the y-axis.
                                    //   Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, -delta.y * Editor.GizmoSpeed, 0);

                                }

                                if (g_z)
                                {
                                    Editor.SelectedNode.Turn(0, 0, delta.x * Editor.GizmoSpeed * 5.5f) ;
                                    //move along the z-axis.
                                    // Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, 0, delta.x * Editor.GizmoSpeed);
                                    FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                                }

                                break;
                        }

                        break;
                    case EditorMode.Scale:



                        if (g_x)
                        {
                            Editor.SelectedNode.Scale = Editor.SelectedNode.Scale + new Vector3(delta.x * Editor.GizmoSpeed, 0, 0); ;// Turn(delta.x * Editor.GizmoSpeed, 0, 0, false);
                                                                                                                                     //move along the x-axis
                            FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                            //Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(-delta.x * Editor.GizmoSpeed, 0, 0);
                            //int b = 5;
                        }

                        if (g_y)
                        {

                            Editor.SelectedNode.Scale = Editor.SelectedNode.Scale + new Vector3(0, delta.y * Editor.GizmoSpeed, 0); ;// Turn(delta.x * Editor.GizmoSpeed, 0, 0, false);
                            int bb = 5;
                            FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                            //move along the y-axis.
                            //   Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, -delta.y * Editor.GizmoSpeed, 0);

                        }

                        if (g_z)
                        {
                            Editor.SelectedNode.Scale = Editor.SelectedNode.Scale + new Vector3(0, 0, delta.x * Editor.GizmoSpeed); ;// Turn(delta.x * Editor.GizmoSpeed, 0, 0, false);
                            FNodeEditor.Editor.UpdateIf(Editor.SelectedNode);
                            // Editor.SelectedNode.Position = Editor.SelectedNode.Position + new Vector3(0, 0, delta.x * Editor.GizmoSpeed);

                        }
                        break;

                }

            }

        }

        public override void OnUpdate()
        {
            //base.OnUpdate();
            EditCam.Move(MoveVector.X * Features.Global.MoveSpeed, MoveVector.Y * Features.Global.MoveSpeed, MoveVector.Z * Features.Global.MoveSpeed);
            int a = 5;
        }

        private void SceneRT_RenderScene(int w, int h)
        {
            var scene = Editor.CurrentScene;
            RenderSize = new Vector2(Size.w, Size.h);

            if (RenderBuffer == null)
            {
                RenderBuffer = new RenderTarget2D(w, h);
            }
            if (RenderBuffer.Width != w || RenderBuffer.Height != h)
            {
                RenderBuffer = new RenderTarget2D(w, h);
            }

            RenderBuffer.Bind();

            scene.RenderLines();

            if (scene.Lights.Count == 0)
            {
                scene.RenderSimple();
            }
            else
            {
                scene.Render();
            }
            GL.Clear(ClearBufferMask.DepthBufferBit);

            if (CurrentGizmo != null && SelectedNode != null)
            {
                switch (Editor.EditMode)
                {
                    case EditorMode.Translate:
                        CurrentGizmo = GizmoMove;
                        break;
                    case EditorMode.Rotate:
                        CurrentGizmo = GizmoRotate;
                        break;
                    case EditorMode.Scale:
                        CurrentGizmo = GizmoScale;
                        break;
                }
                if (SelectedNode != null)
                {
                    CurrentGizmo.Position = Editor.SelectedNode.Position;
                    switch (Editor.SpaceMode)
                    {
                        case SpaceMode.Local:
                            CurrentGizmo.Rotation = Editor.SelectedNode.Rotation;
                            break;
                        case SpaceMode.Global:
                            CurrentGizmo.Rotation = Matrix4.Identity;
                            break;
                    }

                    CurrentGizmo.RenderSimple();
                }
            }

            DrawIcons();

            GLState.State = CurrentGLState.Draw;
            //  scene.RenderShadows();
            //    scene.Render();
            RenderBuffer.Release();

            PPRenderer.SetSize(w, h);

            var out_buf = PPRenderer.Render(RenderBuffer.GetTexture());

            RenderBuffer.Bind();

            Draw(out_buf, 0, 0, w, h);

            RenderBuffer.Release();

            //throw new NotImplementedException();
        }

        public void DrawIcons()
        {

            GLState.State = CurrentGLState.Draw;
            draw.Begin();
            //draw.DrawTexture(LightIcon, 64, 64, 64, 64, 1, 1, 1, 1);

            draw.Blend = Vivid.Draw.BlendMode.Alpha;
            Vector3 point = Editor.CurrentScene.MainCamera.TransformVector(new Vector3(0, 0, 1));

            foreach (var spawn in Editor.CurrentScene.Spawns)
            {
                Matrix4 model = spawn.WorldMatrix;
                //angX = angX + 0.1f;


                Vector3 dif = Editor.CurrentScene.MainCamera.Position - spawn.Position;

                float dp = Vector3.Dot(point, dif);

                if (dp < 0.4f) continue;

                // Camera is at (0, 0, -5) looking along the Z axis
                Matrix4 View = Editor.CurrentScene.MainCamera.WorldMatrix;

                Matrix4 Proj = Editor.CurrentScene.MainCamera.Projection;

                Matrix4 wvp = model * View * Proj;
                wvp.Transpose();

                Vector4 pos = wvp * new Vector4(0, 0, 0, 1.0f);
                pos.X = pos.X / pos.W;
                pos.Y = pos.Y / pos.W;

                pos.X = (0.5f + pos.X * 0.5f) * Vivid.App.VividApp.FrameWidth;
                pos.Y = (0.5f - pos.Y * 0.5f) * Vivid.App.VividApp.FrameHeight;
                //  Console.WriteLine("====:PX:" + pos.x + " PY:" + pos.y);

                if (pos.X > 0 && pos.X < (Vivid.App.VividApp.FrameWidth - 64))
                {
                    if (pos.Y > 0 && pos.Y < (Vivid.App.VividApp.FrameHeight - 64))
                    {
                        draw.Draw(SpawnIcon, new Rect((int)pos.X - 32, (int)pos.Y - 32, 64, 64), new Vivid.Maths.Color(1, 1, 1, 1));
                        spawn.DrawnX = pos.X;
                        spawn.DrawnY = pos.Y;
                    }
                }


            }
            foreach (var light in Editor.CurrentScene.Lights)
            {

                Matrix4 model = light.WorldMatrix;
                //angX = angX + 0.1f;
                Vector3 dif = Editor.CurrentScene.MainCamera.Position - light.Position;

                float dp = Vector3.Dot(point, dif);

                if (dp < 0.4f) continue;

                // Camera is at (0, 0, -5) looking along the Z axis
                Matrix4 View = Editor.CurrentScene.MainCamera.WorldMatrix;

                Matrix4 Proj = Editor.CurrentScene.MainCamera.Projection;

                Matrix4 wvp = model * View * Proj;
                wvp.Transpose();

                Vector4 pos = wvp * new Vector4(0, 0, 0, 1.0f);
                pos.X = pos.X / pos.W;
                pos.Y = pos.Y / pos.W;

                pos.X = (0.5f + pos.X * 0.5f) * Vivid.App.VividApp.FrameWidth;
                pos.Y = (0.5f - pos.Y * 0.5f) * Vivid.App.VividApp.FrameHeight;
                //  Console.WriteLine("====:PX:" + pos.x + " PY:" + pos.y);

                if (pos.X > 0 && pos.X < (Vivid.App.VividApp.FrameWidth - 64))
                {
                    if (pos.Y > 0 && pos.Y < (Vivid.App.VividApp.FrameHeight - 64))
                    {
                        draw.Draw(LightIcon, new Rect((int)pos.X - 32, (int)pos.Y - 32, 64, 64), new Vivid.Maths.Color(1, 1, 1, 1));
                        light.DrawnX = pos.X;
                        light.DrawnY = pos.Y;
                    }
                }




            }



            draw.End();
        }

        public bool CheckIcons()
        {

            Vector2 mp = MousePos;
            mp.X = mp.X - RenderPosition.x;
            mp.Y = mp.Y - RenderPosition.y;

            foreach (var light in Editor.CurrentScene.Lights)
            {

                if ( mp.X> light.DrawnX - 32 && mp.Y > light.DrawnY - 32 && mp.X< light.DrawnX + 32 && mp.Y < light.DrawnY + 32)
                {
                    Editor.SetSelectedNode(light);
                    //SceneEditor.CurrentNode = light;

                    //SceneEditor.NodeTree.SelectedNode = FindNode(SceneEditor.CurrentNode);
                    return true;
                }

            }
            return false;
        }

    }
}
