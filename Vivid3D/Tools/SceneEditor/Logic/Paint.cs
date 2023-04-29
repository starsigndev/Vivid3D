
using Vivid.Maths;
using Vivid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static SceneEditor.SceneEditor;
using System.Diagnostics.Eventing.Reader;

namespace SceneEditor.Logic
{
    public class Paint
    {

        public static void Draw(PaintEventArgs e)
        {
        

            RenderGlobals.CurrentCamera = EditScene.MainCamera;

            //      grid_node.RenderSimple();
            //  EditScene.RenderLines();
            EditGrid.RenderSimple();
            if (EditScene.Lights.Count == 0)
            {
                EditScene.RenderSimple();
            }
            else
            {
                if (EditSceneOT != null)
                {
                    // EditSceneOT.RenderShadows();
                    EditScene.RenderShadows();
                    EditSceneOT.ComputeVisibility();
                    EditSceneOT.RenderLeafs();
                    Console.WriteLine("Rendering Octre-----------------");
                }
                else
                {
                    EditScene.RenderShadows();
                    EditScene.Render();
                }
            }
        //    GemBridge.gem_ClearZBuffer();
            //EditScene.RenderSimple(currentGiz);

            if (CurrentNode != null)
            {
                CurrentGizmo.Position = CurrentNode.Position;
                if (Space == EditSpace.Local)
                {
                    CurrentGizmo.Rotation = CurrentNode.Rotation;
                }
                else if(Space == EditSpace.Global)
                {
                    CurrentGizmo.Rotation = Matrix4.Identity;
                }
                else
                {
                    CurrentGizmo.Rotation = EditCam.Rotation;
                }

                Vector3 dif = EditScene.MainCamera.Position - CurrentGizmo.Position;

                float dist = (float)Math.Sqrt(dif.X * dif.X + dif.Y * dif.Y + dif.Z * dif.Z);


                float scale = dist / 12.0f;
      
                CurrentGizmo.Scale = new Vector3(scale, scale, scale);

                //currentGiz.Turn(180, 0, 0);
                OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);
                CurrentGizmo.RenderSimple();
            }

            DrawIcons();
            //G_Control.Invalidate();

            //GemBridge.gem_EndFrame();
        }

        private static void DrawIcons()
        {
            draw.Begin();
            //draw.DrawTexture(LightIcon, 64, 64, 64, 64, 1, 1, 1, 1);


            Vector3 point = EditScene.MainCamera.TransformVector(new Vector3(0, 0, 1));

            foreach (var spawn in EditScene.Spawns)
            {
                Matrix4 model = spawn.WorldMatrix;
                //angX = angX + 0.1f;


                Vector3 dif = EditScene.MainCamera.Position - spawn.Position;

                float dp = Vector3.Dot(point, dif);

                if (dp < 0.4f) continue;

                // Camera is at (0, 0, -5) looking along the Z axis
                Matrix4 View = EditScene.MainCamera.WorldMatrix;

                Matrix4 Proj = EditScene.MainCamera.Projection;

                Matrix4 wvp = model * View * Proj;
                wvp.Transpose();

                Vector4 pos = wvp * new Vector4(0, 0, 0, 1.0f);
                pos.X = pos.X / pos.W;
                pos.Y = pos.Y / pos.W;

                pos.X = (0.5f + pos.X * 0.5f) * Vivid.App.GeminiApp.FrameWidth;
                pos.Y = (0.5f - pos.Y * 0.5f) * Vivid.App.GeminiApp.FrameHeight;
                //  Console.WriteLine("====:PX:" + pos.x + " PY:" + pos.y);

                if (pos.X > 0 && pos.X < (Vivid.App.GeminiApp.FrameWidth - 64))
                {
                    if (pos.Y > 0 && pos.Y < (Vivid.App.GeminiApp.FrameHeight - 64))
                    {
                        draw.Draw(SpawnIcon, new Rect((int)pos.X - 32, (int)pos.Y - 32, 64, 64), new Vivid.Maths.Color(1, 1, 1, 1));
                        spawn.DrawnX = pos.X;
                        spawn.DrawnY = pos.Y;
                    }
                }


            }
            foreach (var light in EditScene.Lights)
            {

                Matrix4 model = light.WorldMatrix;
                //angX = angX + 0.1f;
                Vector3 dif = EditScene.MainCamera.Position - light.Position;

                float dp = Vector3.Dot(point, dif);

                if (dp < 0.4f) continue;

                // Camera is at (0, 0, -5) looking along the Z axis
                Matrix4 View = EditScene.MainCamera.WorldMatrix;

                Matrix4 Proj = EditScene.MainCamera.Projection;

                Matrix4 wvp = model * View * Proj;
                wvp.Transpose();

                Vector4 pos = wvp * new Vector4(0, 0, 0, 1.0f);
                pos.X = pos.X / pos.W;
                pos.Y = pos.Y / pos.W;

                pos.X = (0.5f + pos.X * 0.5f) * Vivid.App.GeminiApp.FrameWidth;
                pos.Y = (0.5f - pos.Y * 0.5f) * Vivid.App.GeminiApp.FrameHeight;
                //  Console.WriteLine("====:PX:" + pos.x + " PY:" + pos.y);

                if (pos.X > 0 && pos.X < (Vivid.App.GeminiApp.FrameWidth - 64))
                {
                    if (pos.Y > 0 && pos.Y < (Vivid.App.GeminiApp.FrameHeight - 64))
                    {
                        draw.Draw(LightIcon, new Rect((int)pos.X - 32, (int)pos.Y - 32, 64, 64), new Vivid.Maths.Color(1, 1, 1, 1));
                        light.DrawnX = pos.X;
                        light.DrawnY = pos.Y;
                    }
                }




            }



            draw.End();
        }

    }
}
