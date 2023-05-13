using OpenTK.Graphics;
using OpenTK.Mathematics;
using Vivid.Mesh;
using Vivid.Meshes;
using Vivid.Renderers;
using Vivid.Scene;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Vivid.Acceleration.Octree
{
    public class OctreeNode
    {
        public static int LeafsRendered = 0;
        public List<OctreeNode> SubNodes
        {
            get;
            set;
        }

        public BoundingBox Bounds
        {
            get;
            set;
        }

        public Scene.Scene From
        {
            get;
            set;
        }

        public bool Leaf
        {
            get;
            set;
        }

        public bool Empty
        {
            get;
            set;
        }

        public int LeafVertexCount
        {
            get;
            set;
        }

        public List<Meshes.Mesh> Meshes
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        private Entity BoundsMesh = null;


        public List<Entity> Canditates = new List<Entity>();

        private OpenTK.Graphics.QueryHandle _q = QueryHandle.Zero;
        private bool query = false;
        public ASOctree Owner
        {
            get;
            set;
        }

        public OctreeNode Root
        {
            get;
            set;
        }

        public OctreeNode()
        {
            Root = null;
        }
        public OctreeNode(BoundingBox bounds, Scene.Scene from,ASOctree owner,OctreeNode root=null)
        {
            Owner = owner;
            Root = root;

            Meshes = new List<Meshes.Mesh>();
            Bounds = bounds;
            From = from;
            SubNodes = new List<OctreeNode>();
            LeafVertexCount = 0;
            Process();
            Empty = false;
            MeshLines ml = new MeshLines();
            ml.AddBox(Bounds.Min, Bounds.Max, new Vector4(0, 1, 1, 1));
            ml.CreateBuffers();
            From.MeshLines.Add(ml);
            IsVisible = false;
        }
        public void InitializeVisibility()
        {
            BoundsMesh = new Entity();
            BoundsMesh.WriteDepth = false;
            BoundsMesh.DepthTest = false;
            //Vivid.Meshes.Mesh mesh = new Meshes.Mesh(BoundsMesh);
            Vivid.Meshes.Mesh mesh = SceneHelper.BoundsToMesh(Bounds, BoundsMesh);
            BoundsMesh.AddMesh(mesh);
            if (Leaf)
            {

            }
            else
            {
                foreach (var sub in SubNodes)
                {
                    sub.InitializeVisibility();
                }
            }
        }

        private Random r = new Random(Environment.TickCount);

        public void CreateBuffers()
        {
            if (Leaf)
            {
                if (Meshes.Count == 0)
                {
                    Empty = true;
                    return;
                }
                foreach (var mesh in Meshes)
                {
                    mesh.CreateBuffers();
                }
            }
            else
            {
                foreach (var node in SubNodes)
                {
                    node.CreateBuffers();
                }
            }
        }

        public int CountVerticesInBounds(BoundingBox bounds)
        {
            return bounds.CountVerticesInBoundingBox(From.EntityList);
        }

        public void Subdivide()
        {
            var sub_bounds = Bounds.SubdivideBoundingBox();
            foreach (var sub in sub_bounds)
            {
                var new_node = new OctreeNode(sub, From,Owner,this);
                SubNodes.Add(new_node);
            }
        }

        public void Process()
        {
            int vertexCount = CountVerticesInBounds(Bounds);
           // Console.WriteLine("Processing:" + Bounds.Min.ToString() + " Max:" + Bounds.Max.ToString());
            if (vertexCount > ASOctree.VertexLeafLimit)
            {
         //       Console.WriteLine("Subdividing:");
                Subdivide();
            }
            else
            {
                Leaf = true;
                LeafVertexCount = vertexCount;
                GatherLeaf();
                if (Meshes.Count == 0)
                {

                    List<OctreeNode> subs = new List<OctreeNode>();
                    foreach(var pnode in Root.SubNodes)
                    {

                        if(pnode == this)
                        {

                        }
                        else
                        {
                            subs.Add(pnode);
                        }

                    }
                    Root.SubNodes = subs;
                    //int b = 5;

                }
            }
        }

        private void GatherLeaf()
        {
            //   Console.WriteLine("Leaf found.");

            foreach (var ent in From.EntityList)
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
            if (Canditates.Count == 0)
            {
            }
           // Console.WriteLine("Candidates:" + Canditates.Count);

            // Parallel.ForEach(Canditates, ent =>
            foreach (var ent in Canditates)
            {
                Matrix4 world = ent.WorldMatrix;//.Inverted();
                Matrix4 rot = ent.Rotation;

                //Parallel.ForEach(ent.Meshes, mesh =>
                foreach (var mesh in ent.Meshes)
                {
                    List<Vertex> m_Verts = mesh.Vertices;

                    Meshes.Mesh mm = new Meshes.Mesh(null);

                    int tc = 0;
                    //foreach (var tri in ent.)
                    foreach (var tri in mesh.Triangles)
                    {
                        Vector3 p0, p1, p2;
                        Vector3 n0, n1, n2;

                        p0 = Vector3.TransformPosition(m_Verts[tri.V0].Position, world);
                        p1 = Vector3.TransformPosition(m_Verts[tri.V1].Position, world);
                        p2 = Vector3.TransformPosition(m_Verts[tri.V2].Position, world);

                        if (Bounds.IntersectsBoundingBox(p0, p1, p2))
                        {
                            Vertex v0, v1, v2;
                            v0 = m_Verts[tri.V0];
                            v1 = m_Verts[tri.V1];
                            v2 = m_Verts[tri.V2];

                            v0.Position = p0;// new Vector3(p0.X, p0.Y, p0.Z);
                            v1.Position = p1;// new Vector3(p1.X, p1.Y, p1.Z);
                            v2.Position = p2;// new Vector3(p2.X, p2.Y, p2.Z);

                            v0.Normal = Vector3.TransformNormal(m_Verts[tri.V0].Normal, rot); ; //new Vector3(n0.X, n0.Y, n0.Z);
                            v1.Normal = Vector3.TransformNormal(m_Verts[tri.V1].Normal, rot); //new Vector3(n1.X, n1.Y, n1.Z);
                            v2.Normal = Vector3.TransformNormal(m_Verts[tri.V2].Normal, rot);// new Vector3(n2.X, n2.Y, n2.Z);

                            mm.AddVertices(new Meshes.Vertex[] { v0, v1, v2 });
                            mm.AddTriangle(new Meshes.Triangle(tc * 3, tc * 3 + 1, tc * 3 + 2));
                            tc++;
                        }
                    }
                    if (tc > 0)
                    {
                        //Entity.AddMesh(mm);
                        Meshes.Add(mm);
                        mm.Material = mesh.Material;
                    }
                };
            };


        }

        public int LeafCount
        {
            get
            {
                if (Leaf)
                {
                    return 1;
                }
                else
                {
                    int lc = 0;
                    foreach (var node in SubNodes)
                    {
                        lc = lc + node.LeafCount;
                    }
                    return lc;
                }
            }
        }

        public int VertexCount
        {
            get
            {
                if (Leaf)
                {
                    int vc = 0;
                    foreach (var mesh in Meshes)
                    {
                        vc += mesh.Vertices.Count;
                    }
                    return vc;
                }
                else
                {
                    int lc = 0;
                    foreach (var node in SubNodes)
                    {
                        lc = lc + node.VertexCount;
                    }
                    return lc;
                }
            }
        }

        public int TriangleCount
        {
            get
            {
                if (Leaf)
                {
                    int tc = 0;
                    foreach (var mesh in Meshes)
                    {
                        tc += mesh.Triangles.Count;
                    }
                    return tc;
                }
                else
                {
                    int lc = 0;
                    foreach (var node in SubNodes)
                    {
                        lc = lc + node.TriangleCount;
                    }
                    return lc;
                }
            }
        }

        public void Debug()
        {
            if (Leaf && !Empty)
            {
                if (VertexCount > 0)
                {
                   // Console.WriteLine("Leaf: Verts:" + LeafVertexCount);
                }
                else
                {
                }
            }
            else
            {
                foreach (var node in SubNodes)
                {
                    node.Debug();
                }
            }
        }

        public static int lc = 0;

        public void Render()
        {
            if (lc < 0)
            {
            }
            //IsVisible = true;

            if (Leaf)
            {

                if (IsVisible)
                {
                    // Entity.Render(From.Lights[0], From.MainCamera, true);
                    foreach (var mesh in Meshes)
                    {

                        mesh.Material.ColorMap.Bind(0);
                        mesh.Material.NormalMap.Bind(1);
                        mesh.Material.SpecularMap.Bind(2);
                        mesh.RenderMesh();
                    }
                    OctreeNode.LeafsRendered++;
                }


                lc--;
            }
            else
            {
                if (IsVisible)
                {
                    foreach (var node in SubNodes)
                    {
                        node.Render();
                    }
                }
            }
        }

        public List<OctreeNode> Leafs
        {
            get
            {
                List<OctreeNode> List = new List<OctreeNode>();
                if (Leaf)
                {
                    if (Meshes.Count == 0)
                    {
                        return List;
                    }
                    List.Add(this);
                    return List;
                }
                List<OctreeNode> list = new List<OctreeNode>();
                foreach (var node in SubNodes)
                {
                    var leafs = node.Leafs;
                    foreach (var l in leafs)
                    {
                        list.Add(l);
                    }
                }
                return list;
            }
        }
        public void ComputeVisibility()
        {

            if (_q == QueryHandle.Zero)
            {
                _q = GL.GenQuery();
            }


            GL.BeginQuery(QueryTarget.AnySamplesPassed, _q);

            RenderGlobals.CurrentCamera = From.MainCamera;

            BoundsMesh.RenderSimple();

            GL.EndQuery(QueryTarget.AnySamplesPassed);
            query = true;

            int[] pars = new int[3];

            query = false;
            GL.GetQueryObjecti(_q, QueryObjectParameterName.QueryResult, pars);
            if (pars[0] > 0)
            {
                IsVisible = true;
                if (!Leaf)
                {
                    foreach (var sub in SubNodes)
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

        public void Write(BinaryWriter w)
        {
            w.Write(Leaf);
            IO.FileHelp.WriteVec3(w, Bounds.Min);
            IO.FileHelp.WriteVec3(w, Bounds.Max);
            if (Leaf)
            {
                w.Write((int)Meshes.Count);
                foreach (var mesh in Meshes)
                {
                   IO.FileHelp.WriteMesh(w, mesh);
                }
            }
            else
            {
                w.Write(SubNodes.Count);
                foreach (var sub in SubNodes)
                {
                    sub.Write(w);
                }
            }
        }
        public static bool RayToBounds(Ray ray, BoundingBox box)
        {
            float tmin = (box.Min.X - ray.Pos.X) / ray.Dir.X;
            float tmax = (box.Max.X - ray.Pos.X) / ray.Dir.X;

            if (tmin > tmax)
            {
                float temp = tmin;
                tmin = tmax;
                tmax = temp;
            }

            float tymin = (box.Min.Y - ray.Pos.Y) / ray.Dir.Y;
            float tymax = (box.Max.Y - ray.Pos.Y) / ray.Dir.Y;

            if (tymin > tymax)
            {
                float temp = tymin;
                tymin = tymax;
                tymax = temp;
            }

            if ((tmin > tymax) || (tymin > tmax))
                return false;

            if (tymin > tmin)
                tmin = tymin;

            if (tymax < tmax)
                tmax = tymax;

            float tzmin = (box.Min.Z - ray.Pos.Z) / ray.Dir.Z;
            float tzmax = (box.Max.Z - ray.Pos.Z) / ray.Dir.Z;

            if (tzmin > tzmax)
            {
                float temp = tzmin;
                tzmin = tzmax;
                tzmax = temp;
            }

            if ((tmin > tzmax) || (tzmin > tmax))
                return false;

            if (tzmin > tmin)
                tmin = tzmin;

            if (tzmax < tmax)
                tmax = tzmax;

            return true;
        }
        private const float EPSILON = 0.0000001f;
        private RaycastResult RayToTri(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            Vector3 edge1, edge2, h, s, q;
            float a, f, u, v;
            RaycastResult res = new RaycastResult();

            edge1 = v1 - v0;// vertex1 - vertex0;
            edge2 = v2 - v0;
            h = Vector3.Cross(ray.Dir, edge2);


            a = Vector3.Dot(edge1, h);
            if (a > -EPSILON && a < EPSILON)
                return res;    // This ray is parallel to this triangle.
            f = 1.0f / a;
            s = ray.Pos - v0;
            u = f * Vector3.Dot(s, h);
            if (u < 0.0f || u > 1.0f)
                return res;
            q = Vector3.Cross(s, edge1);
            v = f * Vector3.Dot(ray.Dir, q);
            if (v < 0.0f || u + v > 1.0f)
                return res;
            // At this stage we can compute t to find out where the intersection point is on the line.
            float t = f * Vector3.Dot(edge2, q);

            if (t > EPSILON) // ray intersection
            {
                //outIntersectionPoint = rayOrigin + rayVector * t;
                res.Hit = true;
                res.Point = ray.Pos + ray.Dir * t;

                return res;
            }
            else // This means that there is a line intersection but not a ray intersection.
                return res;

            res.Hit = false;
            return res;

            return res;
        }
        public static object lockObj = new object();
        public static object triLock = new object();
        public RaycastResult Raycast(Ray ray)
        {


            if (!Leaf)
            {
                if (RayToBounds(ray, Bounds))
                {
                    RaycastResult res = new RaycastResult();
                    float close = 999999;
                    RaycastResult closeres = null;

             //       foreach (var node in this.SubNodes)
                        Parallel.ForEach(this.SubNodes, node =>
                        {


                            var r1 = node.Raycast(ray);
                            if (r1 != null)
                            {
                                if (r1.Hit)
                                {
                                    //r1.Node = mesh.Owner;
                                    //r1.Entity = mesh.Owner as Entity;

                                    lock (lockObj)
                                    {
                                        if (closeres == null)
                                        {
                                            closeres = r1;
                                            close = (ray.Pos - r1.Point).LengthSquared;
                                        }
                                        else
                                        {
                                            float dist = (ray.Pos - r1.Point).LengthSquared;
                                            if (dist < close)
                                            {
                                                close = dist;
                                                closeres = r1;
                                            }
                                        }
                                    }
                                }
                            }

                        });

                    return closeres;
                }
                else
                {
                    return null;
                }
            }
            else
            {

                RaycastResult res = new RaycastResult();
                float close = 999999;
                RaycastResult closeres = null;

                // Console.WriteLine("Checking Leaf");
             //   foreach (var mesh in Meshes)
                    Parallel.ForEach(Meshes, mesh =>
                    {

                          foreach (var tri in mesh.Triangles)
                        //Parallel.ForEach(mesh.Triangles, tri =>
                    {

                                Vector3 v0, v1, v2;

                                v0 = mesh.Vertices[tri.V0].Position;
                                v1 = mesh.Vertices[tri.V1].Position;
                                v2 = mesh.Vertices[tri.V2].Position;

                                RaycastResult r1 = RayToTri(ray, v0, v1, v2);

                                lock (triLock)
                                {

                                    if (r1.Hit)
                                    {
                                        r1.Node = mesh.Owner;
                                        r1.Entity = mesh.Owner as Entity;

                                        if (closeres == null)
                                        {
                                            closeres = r1;
                                            close = (ray.Pos - r1.Point).LengthSquared;
                                        }
                                        else
                                        {
                                            float dist = (ray.Pos - r1.Point).LengthSquared;
                                            if (dist < close)
                                            {
                                                close = dist;
                                                closeres = r1;
                                            }
                                        }
                                    }
                                }



                            };


                    });

                return closeres;

            }

            return null;

        }

    }
}