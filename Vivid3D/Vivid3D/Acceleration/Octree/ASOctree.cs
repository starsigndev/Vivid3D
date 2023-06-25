using Vivid.App;
using Vivid.RenderTarget;
using Vivid.State;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.IO;
using Vivid.Scene;
using Assimp.Unmanaged;
using Vivid.Audio;
using OpenTK.Graphics;
using Vivid.Renderers;
using Vivid.Physx;
using Vivid.Meshes;
using Vivid.Materials.Materials.Entity;
using Vivid.Draw;
using Vivid.Texture;

namespace Vivid.Acceleration.Octree
{
    public class ASOctree
    {
        public Scene.Scene Base
        {
            get;
            set;
        }

        public OctreeNode RootNode
        {
            get;
            set;
        }

        public static int VertexLeafLimit = 1024;

        public List<Entity> Dynamic = new List<Entity>();
        public static int DynamicC = 0;
        private RenderTarget2D LightTarget;
        private SmartDraw Draw = null;


        public ASOctree(Scene.Scene scene)
        {
            Base = scene;

            ProcessScene();
            RootNode.CreateBuffers();
            LightFX = new Materials.Materials.Entity.LightFX();
            InitializeVisibility();
            LightTarget = new RenderTarget2D(VividApp.FrameWidth, VividApp.FrameHeight);
            Draw = new SmartDraw();
            //Base.Root = new Node();

        }

        public void AddLeafs(OctreeNode node)
        {

            foreach(var mesh in node.Meshes)
            {
                Entity ent = new Entity();
                ent.AddMesh(mesh);
                Base.AddNode(ent);
            }
            foreach(var node2 in node.SubNodes)
            {
                AddLeafs(node2);
            }

        }
        public ASOctree(Scene.Scene scene,MemoryStream stream)
        {
            Base = scene;
         
            BinaryReader r = new BinaryReader(stream);

            ReadScene(r);
            Draw = new SmartDraw();
            LightTarget = new RenderTarget2D(VividApp.FrameWidth, VividApp.FrameHeight);

            Meshes.Mesh mesh = new Meshes.Mesh(null);

            int vc = 0;
            foreach (var m in _Meshes)
            {

                foreach(var vertex in m.Vertices)
                {
                    mesh.AddVertex(vertex,false);
                }
              
                foreach(var tri in m.Triangles)
                {
                    Triangle nt = new Triangle();
                    nt.V0 = tri.V0 + vc;
                    nt.V1 = tri.V1 + vc;
                    nt.V2 = tri.V2 + vc;
                    mesh.AddTriangle(nt);
                }
                vc = mesh.Vertices.Count();

            }

            int b = 5;
            //InternalBufferOverflowException = 5

         //   var tr_mesh = new PXTriMesh(M1,0) ;

            AddLeafs(RootNode);

         
            LightFX = new Materials.Materials.Entity.LightFX();
            foreach (var dy in Dynamic)
            {
                Base.AddNode(dy);

            }
            InitializeVisibility();
        }
        public ASOctree(Scene.Scene scene,string path)
        {

            Base = scene;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            LightTarget = new RenderTarget2D(VividApp.FrameWidth, VividApp.FrameHeight);
            ReadScene(r);
            Draw = new SmartDraw();
            AddLeafs(RootNode);

            fs.Close();
            LightFX = new Materials.Materials.Entity.LightFX();
            foreach(var dy in Dynamic)
            {
                Base.AddNode(dy);

            }
            InitializeVisibility();
        }

        public void ReadScene(BinaryReader r)
        {
            RootNode = ReadNode(r);

            int dc = r.ReadInt32();
            for (int i = 0; i < dc; i++)
            {
                Entity new_ent = new Entity();
                IO.FileHelp.ReadEntityData(new_ent, r);
                Dynamic.Add(new_ent);
            }

            int lc = r.ReadInt32();
            for (int i = 0; i < lc; i++)
            {
                Light light = new Light();
                IO.FileHelp.ReadLightData(light, r);
                Base.Lights.Add(light);
                Base.AddNode(light);
            }

            int sc = r.ReadInt32();
            for (int i = 0; i < sc; i++)
            {
                SpawnPoint spawn = new SpawnPoint();
                IO.FileHelp.ReadSpawnData(spawn, r);
                Base.AddNode(spawn);
            }
        }
        public List<Vivid.Meshes.Mesh> _Meshes = new List<Meshes.Mesh>();
        public OctreeNode ReadNode(BinaryReader r)
        {
            OctreeNode res = new OctreeNode();
            res.From = Base;
            res.Owner = this;
            res.Meshes = new List<Meshes.Mesh>();
            res.SubNodes = new List<OctreeNode>();

            bool leaf = r.ReadBoolean();
            var min = IO.FileHelp.ReadVec3(r);
            var max = IO.FileHelp.ReadVec3(r);
            res.Bounds = new BoundingBox(min, max);


            if (leaf)
            {
                res.Leaf = true;
                int mc = r.ReadInt32();
                for(int i = 0; i < mc; i++)
                {
                    res.Meshes.Add(Vivid.IO.FileHelp.ReadMesh(r));
                    _Meshes.Add(res.Meshes[i]);

                   
                }
               // res.Leaf = leaf;
               // res.LeafEntity = new Entity();
                //IO.FileHelp.ReadMeshData(res.LeafEntity, r);
            }
            else
            {
                res.Leaf = false;
                
                int cc = r.ReadInt32();
                for (int i = 0; i < cc; i++)
                {
                    res.SubNodes.Add(ReadNode(r));
                }
            }
            return res;
        }


        public void Save(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);

            RootNode.Write(w);



            w.Write(Dynamic.Count);
            foreach (var dyn in Dynamic)
            {
                IO.FileHelp.WriteEntityData(w, dyn);
            }

            w.Write(Base.Lights.Count);
            foreach (var light in Base.Lights)
            {
                IO.FileHelp.WriteLightData(w, light);
            }

            w.Write(Base.Spawns.Count);
            foreach (var s in Base.Spawns)
            {
                IO.FileHelp.WriteSpawnData(w, s);
            }

            w.Flush();
            fs.Close();
        }

        public void ProcessScene()
        {
            RootNode = new OctreeNode(Base.Bounds, Base,this);
        }

        public int LeafCount()
        {
            return RootNode.LeafCount;
        }

        public int VertexCount()
        {
            return RootNode.VertexCount;
        }

        public int TriangleCount()
        {
            return RootNode.TriangleCount;
        }

        public void Debug()
        {
            RootNode.Debug();
        }
        Texture2D test = null;
        public void Render()
        {

            LightFX.Camera = Base.MainCamera;
            LightFX.Entity = null;
            bool first = true;
    
            foreach (var light in Base.Lights)
            {
                first = RenderLight(first, light);

            }
           
            LightFX.Unbind();
        
           
           // Console.WriteLine("Dynamic Rendered:" + dy_d);
        //    Console.WriteLine("Leafs Rendered:" + OctreeNode.LeafsRendered);
        }

        private bool RenderLight(bool first, Light light)
        {
            LightTarget.Bind();
            if (first)
            {
                Base.RenderLines();
            }
            LightFX.Light = light;
            LightFX.Bind();
            light.RTC.Cube.Bind(3);
            GLState.State = CurrentGLState.LightFirstPass;
            RootNode.Render();

            foreach (var dy in Dynamic)
            {
                if (dy.IsVisible == false) continue;
                bool firstPass = true;
    
                    dy.Render(light, Base.MainCamera, firstPass);
                    firstPass = false;
      

            }
            //light.RTC.Cube.Release(3);
            LightTarget.Release();
            GLState.State = CurrentGLState.Draw;
            if (first)
            {
                Draw.Blend = BlendMode.Alpha;
            }
            else
            {
                Draw.Blend = BlendMode.Additive;
            }
       
            first = false;
            Draw.DrawNow(LightTarget.GetTexture(), new Maths.Rect(0, 0, VividApp.FrameWidth, VividApp.FrameHeight), new Maths.Color(1, 1, 1, 1), true);
            if (first)
            {
                Draw.Blend = BlendMode.Additive;
                first = false;
            }
          //  GLState.State = CurrentGLState.LightSecondPass;
            return first;
        }

        public void InitializeVisibility()
        {
            RootNode.InitializeVisibility();
            foreach(var dy in Dynamic)
            {
                dy.BoundsMesh = new Entity();
                dy.BoundsMesh.WriteDepth = false;
                dy.BoundsMesh.DepthTest = false;
                //Vivid.Meshes.Mesh mesh = new Meshes.Mesh(BoundsMesh);
                Vivid.Meshes.Mesh mesh = SceneHelper.BoundsToMesh(dy.BoundsNoTransform,dy.BoundsMesh);
                dy.BoundsMesh.AddMesh(mesh);

            }
        }
        private QueryHandle _q = QueryHandle.Zero;
        public void ComputeVisibility()
        {
            OctreeNode.LeafsRendered = 0;

               GL.ColorMask(false, false, false, false);
                GL.DepthMask(false);
            GL.Enable(EnableCap.ScissorTest);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            GL.Scissor(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
            RootNode.ComputeVisibility();
            foreach (var dy in Dynamic)
            {
                if (_q == QueryHandle.Zero)
                {
                    _q = GL.GenQuery();
                }


                GL.BeginQuery(QueryTarget.AnySamplesPassed, _q);

                RenderGlobals.CurrentCamera = Base.MainCamera;

                dy.BoundsMesh.Position = dy.Position;
                dy.BoundsMesh.RenderSimple();


                GL.EndQuery(QueryTarget.AnySamplesPassed);


                int[] pars = new int[3];

                GL.GetQueryObjecti(_q, QueryObjectParameterName.QueryResult, pars);
                if (pars[0] > 0)
                {
                    dy.IsVisible = true;
                }
                else
                {
                    dy.IsVisible = false;
                }
            }
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
             GL.DepthMask(true);
            GL.Disable(EnableCap.ScissorTest);
             GL.ColorMask(true, true, true, true);
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //vis_rt.Release();



        }

        public RaycastResult Raycast(Ray ray)
        {
            return RootNode.Raycast(ray);

        }

        public List<OctreeNode> GetLeafs()
        {
            return RootNode.Leafs;
        }
        private LightFX LightFX = null;
    }
}