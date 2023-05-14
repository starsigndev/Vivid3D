using Assimp;
using BepuPhysics.Collidables;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Draw;
using Vivid.RenderTarget;
using Vivid.Scene;
using Vivid.Shaders;
using Vivid.Texture;

namespace Vivid.PostProcesses
{
    public class PostProcess
    {

        private Texture2D LightSphere = null;

        public List<RenderTarget2D> Targets
        {
            get;
            set;
        }

        public Vivid.Scene.Scene Scene
        {
            get;
            set;
        }

        public Vivid.Scene.Entity Specific
        {
            get;
            set;
        }

        public SmartDraw Draw
        {
            get;
            set;
        }

        public PostProcess(Scene.Scene scene, int num_targets)
        {
            Scene = scene;
            Targets = new List<RenderTarget2D>();
            Specific = null;
            for(int i=0;i<num_targets; i++)
            {
                Targets.Add(new RenderTarget2D(Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight));
            }
            Draw = new SmartDraw();
            LightSphere = new Texture2D("gemini/lightSphere.png");
            int a = 5;
        }
 
        public PostProcess(Scene.Entity entity,int num_targets)
        {
            Scene = null;
            Targets = new List<RenderTarget2D>();
            Specific = entity;
            for (int i = 0; i < num_targets; i++)
            {
                Targets.Add(new RenderTarget2D(Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight));
            }
            Draw = new SmartDraw();
        }

        public void BindTarget(int index)
        {

            Targets[index].Bind();

        }

        public void ReleaseTarget(int index)
        {
            Targets[index].Release();
        }
    
        public Texture2D GetTexture(int index)
        {
            return Targets[index].GetTexture();
        }

        public virtual void ProcessAndDraw()
        {

        }

        public virtual Texture2D Process()
        {

            return null;
        }

        public void RenderDepth()
        {
            if (Specific != null)
            {
                Specific.RenderDepth(Scene.MainCamera);
            }
            else
            {
                Scene.RenderDepth();
            }
        }

        public void RenderLit()
        {
            //Scene.RenderShadows();
            Scene.Render();
        }

        public void RenderHalo(int light_id)
        {
            Vector3 point = Scene.MainCamera.TransformVector(new Vector3(0, 0, 1));

            Vivid.Scene.Light light = Scene.Lights[light_id];

            Matrix4 model = light.WorldMatrix;
            //angX = angX + 0.1f;
            Vector3 dif = Scene.MainCamera.Position - light.Position;

            float dp = Vector3.Dot(point, dif);

           // if (dp < 0.4f) return;

            // Camera is at (0, 0, -5) looking along the Z axis
            Matrix4 View = Scene.MainCamera.WorldMatrix;

            Matrix4 Proj = Scene.MainCamera.Projection;

            Matrix4 wvp = model * View * Proj;
            wvp.Transpose();

            Vector4 pos = wvp * new Vector4(0, 0, 0, 1.0f);
            pos.X = pos.X / pos.W;
            pos.Y = pos.Y / pos.W;

            pos.X = (0.5f + pos.X * 0.5f) * Vivid.App.VividApp.FrameWidth;
            pos.Y = (0.5f - pos.Y * 0.5f) * Vivid.App.VividApp.FrameHeight;

            //OpenTK.Graphics.OpenGL.GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);
           // OpenTK.Graphics.OpenGL.GL.ClearColor(0, 0, 0, 1);
            
            Scene.RenderDepth();

            OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);


            if (qscene == null)
            {
                qscene = new Scene.Scene();
                BB = new Entity();
                Meshes.Mesh mesh = new Meshes.Mesh(BB);
                Vivid.Meshes.Vertex v1, v2, v3, v4;

                v1 = new Meshes.Vertex();
                v2 = new Meshes.Vertex();
                v3 = new Meshes.Vertex();
                v4 = new Meshes.Vertex();
                v1.Position = new Vector3(-1, -1, 0);
                v2.Position = new Vector3(1, -1, 0);
                v3.Position = new Vector3(1, 1, 0);
                v4.Position = new Vector3(-1, 1, 0);
                v1.TexCoord = new Vector3(0, 0, 0);
                v2.TexCoord = new Vector3(1, 0, 0);
                v3.TexCoord = new Vector3(1, 1, 0);
                v4.TexCoord = new Vector3(0, 1, 0);

                mesh.AddVertices(v1, v2, v3, v4);

                Vivid.Meshes.Triangle t1, t2;

                t1 = new Vivid.Meshes.Triangle();
                t2 = new Vivid.Meshes.Triangle();

                t1.V0 = 0;
                t1.V1 = 1;
                t1.V2 = 2;

                t2.V0 = 2;
                t2.V1 = 3;
                t2.V2 = 0;

                mesh.AddTriangles(t1, t2);

                mesh.CreateBuffers();

                mesh.Material.ColorMap = LightSphere;

                BB.AddMesh(mesh);

                qscene.AddNode(BB);

            }


            BB.Position = light.Position;
            BB.Scale = new Vector3(1, 1, 1);
            BB.Rotation = Scene.MainCamera.Rotation;
            qscene.MainCamera = Scene.MainCamera;
            qscene.RenderSimple();
           

           // Vivid.Scene.Scene qscene;

           // Draw.Begin();
           // Draw.Blend = Vivid.Draw.BlendMode.Alpha;
           // Draw.Draw(LightSphere, new Maths.Rect((int)pos.X - 64, (int)pos.Y - 64, 128, 128), new Maths.Color(1, 1, 1, 1));
           // Draw.End();



        }

        Vivid.Scene.Entity BB = null;
        Vivid.Scene.Scene qscene = null;

        public void DrawTarget(int index,ShaderModule shader=null)
        {

            Draw.Begin();
            Draw.Draw(Targets[index].GetTexture(), new Maths.Rect(0, Vivid.App.VividApp.FrameHeight, VividApp.FrameWidth, -VividApp.FrameHeight), new Maths.Color(1, 1, 1, 1));
            Draw.End(shader);

        }

    }

}
