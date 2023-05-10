using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Mesh;
using Vivid.Meshes;
using Vivid.Scene;

namespace Vivid.UI
{
    public class UI3DContainer
    {

        public RenderTarget.RenderTarget2D RT
        {
            get;
            set;
        }

        public IForm Form
        {
            get;
            set;
        }

        public Meshes.Mesh Mesh
        {
            get;
            set;
        }

        public Entity Entity
        {
            get;
            set;

        }

        public Vector3 Rotation
        {
            get;set;
        }

        public Vector3 Scale
        {
            get;
            set;
        }

        public void SetForm(IForm form)
        {

            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 1, 1);
            RT = new RenderTarget.RenderTarget2D(form.Size.w, form.Size.h);
            //Buffer = new MeshBuffer();
            int v = 5;
            Form = form;

            Entity = new Entity();

            Mesh = new Meshes.Mesh(Entity);

            Vertex v1, v2, v3, v4;

            v1 = new Vertex();
            v2 = new Vertex();
            v3 = new Vertex();
            v4 = new Vertex();

            v1.Position = new OpenTK.Mathematics.Vector3(-1.5f, -1, 0);
            v2.Position = new OpenTK.Mathematics.Vector3(1.5f, -1, 0);
            v3.Position = new OpenTK.Mathematics.Vector3(1.5f, 1, 0);
            v4.Position = new OpenTK.Mathematics.Vector3(-1.5f, 1, 0);
            v1.TexCoord = new Vector3(0, 0, 0);
            v2.TexCoord = new Vector3(1, 0, 0);
            v3.TexCoord = new Vector3(1, 1, 0);
            v4.TexCoord = new Vector3(0, 1, 0);

            Mesh.AddVertices(v1, v2, v3, v4);

            Triangle t1, t2;

            t1 = new Triangle();
            t2 = new Triangle();

            t1.V0 = 0;
            t1.V1 = 1;
            t1.V2 = 2;

            t2.V0 = 2;
            t2.V1 = 3;
            t2.V2 = 0;

            Mesh.AddTriangles(t1, t2);
            Mesh.CreateBuffers();

            Entity.AddMesh(Mesh);
            Mesh.Material.ColorMap = RT.GetTexture();
            Form.Position = new Maths.Position(0, 0);
            Rotation = new Vector3(-45, 0, 0);
        }

        public void Update()
        {

            RT.Bind();
            Form.Render();
            RT.Release();


        }

        

        public void Render()
        {

            Scale = new Vector3(1.0f, 0.7f, 1.0f);
            Vector3 offset = VividApp.CurrentScene.MainCamera.TransformVector(new Vector3(0, 0, -5));

            Entity.Position = VividApp.CurrentScene.MainCamera.Position + offset;
            Entity.Rotation = VividApp.CurrentScene.MainCamera.Rotation;
            Entity.Scale = Scale;
            Entity.Turn(Rotation.X,Rotation.Y, Rotation.Z);
            Entity.RenderUI();



        }

    }
}
