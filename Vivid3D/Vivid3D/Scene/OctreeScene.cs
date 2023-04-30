using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vivid.App;
using Vivid.Mesh;
using Vivid.Meshes;
using Vivid.Renderers;
using Vivid.RenderTarget;

namespace Vivid.Scene
{
    public class OcNode
    {
        public static int VertexLimit = 1024;

        public bool Leaf
        {
            get;
            set;
        }

        public BoundingBox Bounds
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public Entity BoundsMesh = null;

        public List<OcNode> Child = new List<OcNode>();
        public Scene Scene;
        public Entity LeafEntity = null;
        public List<Entity> Canditates = new List<Entity>();
        public OctreeScene Owner;

        public OcNode()
        {
            IsVisible = true;
        }

        public OcNode(Scene scene, BoundingBox b, OctreeScene owner)
        {
            Scene = scene;
            Bounds = b;
            Leaf = true;
            Owner = owner;
            Process();
            IsVisible = true;
        }

        public void CreateBuffers()
        {
            if (Leaf)
            {
                foreach (var mesh in LeafEntity.Meshes)
                {
                    mesh.CreateBuffers();
                }
            }
            else
            {
                foreach (var sub in Child)
                {
                    sub.CreateBuffers();
                }
            }
        }

        public void DebugVisibility()
        {
            if (Leaf)
            {
                RenderGlobals.CurrentCamera = Scene.MainCamera;
                BoundsMesh.RenderSimple();
            }
            foreach (var sub in Child)
            {
                sub.DebugVisibility();
            }
        }

        public void BeginVisibility()
        {
            BoundsMesh = new Entity();
            BoundsMesh.WriteDepth = false;
            BoundsMesh.DepthTest = false;
            //Vivid.Meshes.Mesh mesh = new Meshes.Mesh(BoundsMesh);
            Vivid.Meshes.Mesh mesh = SceneHelper.BoundsToMesh(Bounds, BoundsMesh);
            BoundsMesh.AddMesh(mesh);

            IsVisible = false;

            if (Leaf)
            {
            }
            else
            {
                foreach (var sub in Child)
                {
                    sub.BeginVisibility();
                }
            }
        }

        private OpenTK.Graphics.QueryHandle _q = QueryHandle.Zero;
        private bool query = false;

        public void ComputeVisibility()
        {
            if (_q == QueryHandle.Zero)
            {
                _q = GL.GenQuery();
            }

            //GL.ClearMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);

            if (Leaf)
            {
                IsVisible = true;
            }
            else
            {
                if (query == false)
                {
                    GL.BeginQuery(QueryTarget.AnySamplesPassed, _q);

                    RenderGlobals.CurrentCamera = Scene.MainCamera;

                    BoundsMesh.RenderSimple();

                    GL.EndQuery(QueryTarget.AnySamplesPassed);
                    query = true;
                }
                else
                {
                    int[] pars = new int[3];
                    GL.GetQueryObjecti(_q, QueryObjectParameterName.QueryResultAvailable, pars);

                    if (pars[0] > 0)
                    {
                        query = false;
                        GL.GetQueryObjecti(_q, QueryObjectParameterName.QueryResult, pars);
                        if (pars[0] > 0)
                        {
                            IsVisible = true;
                            if (!Leaf)
                            {
                                foreach (var sub in Child)
                                {
                                    sub.ComputeVisibility();
                                }
                            }
                        }
                        else
                        {
                            IsVisible = false;
                        }
                    }
                    else
                    {
                        //IsVisible = false;
                        return;
                    }
                }
            }
            //   Console.WriteLine("Pixels:" + pars[0]);

            if (Leaf)
            {
            }
            else
            {
                if (IsVisible == true)
                {
                    foreach (var sub in Child)
                    {
                        sub.ComputeVisibility();
                    }
                }
            }
        }

        public static int RC = 0;

        public void RenderDepthLeafs()
        {
            if (Leaf)
            {
                if (IsVisible)
                {
                    LeafEntity.RenderDepth(Scene.MainCamera);

                    RC++;
                }
            }
            else
            {
                if (IsVisible)
                {
                    foreach (var node in Child)
                    {
                        node.RenderDepthLeafs();
                    }
                }
            }
        }

        private int rcc = 0;

        public void RenderLeafs(Light light)
        {
            if (Leaf)
            {
                if (IsVisible)
                {
                    LeafEntity.Render(light, Scene.MainCamera, true);

                    RC++;
                }
            }
            else
            {
                if (IsVisible)
                {
                    foreach (var node in Child)
                    {
                        node.RenderLeafs(light);
                    }
                }
            }
        }

        public void RenderLeafs()
        {
            IsVisible = true;
            if (Leaf)
            {
                if (IsVisible)
                {
                    bool first = true;
                    foreach (var light in Scene.Lights)
                    {
                        LeafEntity.Render(light, Scene.MainCamera, first);
                        first = false;
                    }

                    RC++;
                }
            }
            else
            {
                if (IsVisible)
                {
                    foreach (var node in Child)
                    {
                        node.RenderLeafs();
                    }
                }
            }
        }

        public void Subdivide()
        {
            foreach (var b in Bounds.SubdivideBoundingBox())
            {
                if (b.CountVerticesInBoundingBox(Scene.EntityList) > 0)
                {
                    OcNode nn = new OcNode(Scene, b, Owner);

                    Child.Add(nn);
                }
            }
        }

        public void BuildLeafs()
        {
            if (Leaf)
            {
                LeafEntity = new Entity();
            }
            else
            {
                foreach (var child in Child)
                {
                    child.BuildLeafs();
                }
            }
        }

        public int NodeCount
        {
            get
            {
                int nc = 1;
                foreach (var sub in Child)
                {
                    nc = nc + sub.NodeCount;
                }
                return nc;
            }
        }

        public int LeafCount
        {
            get
            {
                if (this.Leaf) return 1;
                int lc = 0;
                foreach (var sub in Child)
                {
                    lc = lc + sub.LeafCount;
                }
                return lc;
            }
        }

        public void Debug()
        {
            if (Leaf)
            {
                MeshLines lines = new MeshLines();
                lines.AddBox(Bounds.Min, Bounds.Max, new Vector4(1, 1, 1, 1));
                Scene.MeshLines.Add(lines);
                lines.CreateBuffers();
                Console.WriteLine("Leaf - Candidates:" + Canditates.Count);
                Console.WriteLine("Leaf Meshes:" + LeafEntity.Meshes.Count);
                if (LeafEntity.Meshes.Count > 0)
                {
                    Console.WriteLine("Verts:" + LeafEntity.Meshes[0].Vertices.Count);
                }
                else
                {
                }
            }
            else
            {
                foreach (var child in Child)
                {
                    child.Debug();
                }
            }
        }

        public void Process()
        {
            if (Bounds.CountVerticesInBoundingBox(Scene.EntityList) > VertexLimit)
            {
                Leaf = false;
                Subdivide();
                //GC.Collect();
            }
            else
            {
                Leaf = true;
                LeafEntity = new Entity();
                foreach (var ent in Scene.EntityList)
                {
                    if (ent.EntityType == EntityType.Static)
                    {
                        if (Bounds.Intersects(ent.Bounds))
                        {
                            Canditates.Add(ent);
                        }
                    }
                    else
                    {
                        if (!Owner.Dynamic.Contains(ent))
                        {
                            Owner.Dynamic.Add(ent);
                        }
                    }
                }

                Parallel.ForEach(Canditates, ent =>
                //foreach (var ent in Canditates)
                {
                    Matrix4 world = ent.WorldMatrix.Inverted();
                    Matrix4 rot = ent.Rotation;

                    Parallel.ForEach(ent.Meshes, mesh =>

                     {
                         Vertex[] m_Verts = mesh.VertexArray;

                         Meshes.Mesh mm = new Meshes.Mesh(LeafEntity);

                         var poss = mesh.Positions;
                         var norms = mesh.Normals;

                         int tc = 0;
                         //foreach (var tri in ent.)
                         foreach (var tri in mesh.Triangles)
                         {
                             Vector3 p0, p1, p2;
                             Vector3 n0, n1, n2;

                             p0 = Vector3.TransformPosition(poss[tri.V0], world);
                             p1 = Vector3.TransformPosition(poss[tri.V1], world);
                             p2 = Vector3.TransformPosition(poss[tri.V2], world);

                             if (Bounds.IntersectsBoundingBox(p0, p1, p2))
                             {
                                 Vertex v0, v1, v2;
                                 v0 = m_Verts[tri.V0];
                                 v1 = m_Verts[tri.V1];
                                 v2 = m_Verts[tri.V2];

                                 v0.Position = p0;// new Vector3(p0.X, p0.Y, p0.Z);
                                 v1.Position = p1;// new Vector3(p1.X, p1.Y, p1.Z);
                                 v2.Position = p2;// new Vector3(p2.X, p2.Y, p2.Z);
                                 v0.Normal = Vector3.TransformNormal(norms[tri.V0], rot); ; //new Vector3(n0.X, n0.Y, n0.Z);
                                 v1.Normal = Vector3.TransformNormal(norms[tri.V1], rot); //new Vector3(n1.X, n1.Y, n1.Z);
                                 v2.Normal = Vector3.TransformNormal(norms[tri.V2], rot);// new Vector3(n2.X, n2.Y, n2.Z);

                                 mm.AddVertices(new Meshes.Vertex[] { v0, v1, v2 });
                                 mm.AddTriangle(new Meshes.Triangle(tc * 3, tc * 3 + 1, tc * 3 + 2));
                                 tc++;
                             }
                         }
                         if (tc > 0)
                         {
                             LeafEntity.AddMesh(mm);
                             mm.Material = mesh.Material;
                         }
                     });
                });
            }
        }

        public void Write(BinaryWriter w)
        {
            w.Write(Leaf);
            IO.FileHelp.WriteVec3(w, Bounds.Min);
            IO.FileHelp.WriteVec3(w, Bounds.Max);
            if (Leaf)
            {
                IO.FileHelp.WriteMeshData(w, LeafEntity);
            }
            else
            {
                w.Write(Child.Count);
                foreach (var sub in Child)
                {
                    sub.Write(w);
                }
            }
        }
    }

    public class OctreeScene
    {
        public Scene Scene;
        public OcNode Root = null;
        public List<Entity> Dynamic = new List<Entity>();
        public static int DynamicC = 0;

        public OctreeScene(Scene scene, MemoryStream stream)
        {
            Scene = scene;
            stream.Position = 0;
            BinaryReader r = new BinaryReader(stream);
            ReadScene(r);
            r.Close();
        }

        public OctreeScene(Scene scene, string path)
        {
            Scene = scene;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            ReadScene(r);

            fs.Close();
        }

        public OctreeScene(Vivid.Scene.Scene scene)
        {
            Scene = scene;
            Root = new OcNode(scene, scene.Bounds, this);
            Root.CreateBuffers();
        }

        public int NodeCount
        {
            get
            {
                return Root.NodeCount;
            }
        }

        public int LeafCount
        {
            get
            {
                return Root.LeafCount;
            }
        }

        public void DebugVisibility()
        {
            Root.DebugVisibility();
        }

        public void Render()
        {
            RenderLeafs();
        }

        public void RenderLight(Light light)
        {
            RenderLeafs(light);
        }

        public void RenderShadows()
        {
            foreach (var light in Scene.Lights)
            {
                Scene.ShadowRender.mRT = light.RTC;
                Scene.ShadowRender.RenderDepth(light.Position, light.Range, this);
            }
        }

        public void RenderDepthLeafs()
        {
            Root.RenderDepthLeafs();

            foreach (var obj in Dynamic)
            {
                //   if (Scene.MainCamera.IsVisible(obj.Bounds))
                {
                    if (Scene.Lights.Count == 0) return;
                    obj.RenderDepth(Scene.MainCamera, true);
                    DynamicC++;
                }
            }
        }

        public void RenderLeafs(Light light)
        {
            Scene.MainCamera.UpdateFS();
            Root.RenderLeafs(light);
        }

        public void RenderLeafs()
        {
            OcNode.RC = 0;
            Scene.MainCamera.UpdateFS();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Root.RenderLeafs();
            int oc = 0;
            DynamicC = 0;
            foreach (var obj in Dynamic)
            {
                //    if (Scene.MainCamera.IsVisible(obj.Bounds))
                {
                    bool first = true;
                    foreach (var light in Scene.Lights)
                    {
                        obj.Render(light, Scene.MainCamera, first);
                        first = false;
                    }

                    DynamicC++;
                }
            }
            Console.WriteLine("Rendered:" + OcNode.RC + " Leafs");
        }

        public void Debug()
        {
            Root.Debug();
        }

        public void ReadScene(BinaryReader r)
        {
            Root = ReadNode(r);

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
                Scene.Lights.Add(light);
                Scene.AddNode(light);
            }

            int sc = r.ReadInt32();
            for (int i = 0; i < sc; i++)
            {
                SpawnPoint spawn = new SpawnPoint();
                IO.FileHelp.ReadSpawnData(spawn, r);
                Scene.AddNode(spawn);
            }
        }

        public OcNode ReadNode(BinaryReader r)
        {
            OcNode res = new OcNode();
            res.Scene = Scene;
            res.Owner = this;

            bool leaf = r.ReadBoolean();
            var min = IO.FileHelp.ReadVec3(r);
            var max = IO.FileHelp.ReadVec3(r);
            res.Bounds = new BoundingBox(min, max);
            if (leaf)
            {
                res.Leaf = leaf;
                res.LeafEntity = new Entity();
                IO.FileHelp.ReadMeshData(res.LeafEntity, r);
            }
            else
            {
                res.Leaf = false;
                res.LeafEntity = null;
                int cc = r.ReadInt32();
                for (int i = 0; i < cc; i++)
                {
                    res.Child.Add(ReadNode(r));
                }
            }
            return res;
        }

        public void Save(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);

            Root.Write(w);

            w.Write(Dynamic.Count);
            foreach (var dyn in Dynamic)
            {
                IO.FileHelp.WriteEntityData(w, dyn);
            }

            w.Write(Scene.Lights.Count);
            foreach (var light in Scene.Lights)
            {
                IO.FileHelp.WriteLightData(w, light);
            }

            w.Write(Scene.Spawns.Count);
            foreach (var s in Scene.Spawns)
            {
                IO.FileHelp.WriteSpawnData(w, s);
            }

            w.Flush();
            fs.Close();
        }

        private RenderTarget2D VisibilityRT;

        public void InitializeVisibility()
        {
            Root.BeginVisibility();
        }

        private RenderTarget2D vis_rt = null;
        private bool init_vis_needed = true;

        public void ComputeVisibility()
        {
            if (vis_rt == null)
            {
                vis_rt = new RenderTarget2D(512, 512);
            }
            if (init_vis_needed)
            {
                //InitializeVisibility();
                //init_vis_needed = false;
                // return;
            }
            vis_rt.Bind();
            //   GL.ColorMask(false, false, false, false);
            //    GL.DepthMask(false);
            GL.Enable(EnableCap.ScissorTest);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            GL.Scissor(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
            Root.ComputeVisibility();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            // GL.DepthMask(true);
            GL.Disable(EnableCap.ScissorTest);
            // GL.ColorMask(true, true, true, true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            vis_rt.Release();
        }
    }
}