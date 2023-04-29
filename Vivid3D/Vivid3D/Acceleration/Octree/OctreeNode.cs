
using Vivid.Scene;
using PhysX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Mesh;
using OpenTK.Mathematics;
using Vivid.Meshes;

namespace Vivid.Acceleration.Octree
{
    public class OctreeNode
    {

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

        public Entity Entity
        {
            get;
            set;
        }

        public List<Entity> Canditates = new List<Entity>();

        public OctreeNode(BoundingBox bounds,Scene.Scene from)
        {
            
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
        }
        Random r = new Random(Environment.TickCount);
        public void CreateBuffers()
        {
            if (Leaf)
            {
                if (Entity.Meshes.Count == 0)
                {
                    int bb = 5;
                    Empty = true;
                    return;
                }
                int none = 0;
                foreach(var mesh in Entity.Meshes)
                {
                    if (mesh.Vertices.Count > 0)
                    {

                    }
                    float cr, g, b;
                    cr = (float)r.NextDouble();
                    g = (float)r.NextDouble();
                    b = (float)r.NextDouble();
                    for(int i = 0; i < mesh.Vertices.Count; i++) 
                    {
                        Vertex v = mesh.Vertices[i];
                        v.Color = new Vector4(cr, g, b, 1.0f);
                    //    mesh.Vertices[i] = v;

                    }

                    mesh.CreateBuffers();
                }
            }
            else
            {
                foreach(var node in SubNodes)
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
            foreach(var sub in sub_bounds)
            {

                var new_node = new OctreeNode(sub, From);
                SubNodes.Add(new_node);

            }
        }

        public void Process()
        {

            int vertexCount = CountVerticesInBounds(Bounds);
            Console.WriteLine("Processing:" + Bounds.Min.ToString() + " Max:" + Bounds.Max.ToString());
            if (vertexCount > ASOctree.VertexLeafLimit)
            {
                Console.WriteLine("Subdividing:");
                Subdivide();
            }
            else
            {
                
                    Leaf = true;
                    LeafVertexCount = vertexCount;
                    GatherLeaf();

              
            }

           
        }

        private void GatherLeaf()
        {
            Console.WriteLine("Leaf found.");
            Entity = new Entity();

            //LeafEntity = new Entity();
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

                }
            }
            if (Canditates.Count == 0)
            {
                int bb = 5;
            }
            Console.WriteLine("Candidates:" + Canditates.Count);

           // Parallel.ForEach(Canditates, ent =>
            foreach (var ent in Canditates)
            {


                Matrix4 world = ent.WorldMatrix;//.Inverted();
                Matrix4 rot = ent.Rotation;




                //Parallel.ForEach(ent.Meshes, mesh =>
                foreach(var mesh in ent.Meshes)
                {

                    List<Vertex> m_Verts = mesh.Vertices;


                    Meshes.Mesh mm = new Meshes.Mesh(Entity);

                   

                    int tc = 0;
                    //foreach (var tri in ent.)
                    foreach (var tri in mesh.Triangles)
                    {

                        Vector3 p0, p1, p2;
                        Vector3 n0, n1, n2;


                        p0 =  Vector3.TransformPosition(m_Verts[tri.V0].Position, world);
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
                           // v0.Normal = Vector3.TransformNormal(norms[tri.V0], rot); ; //new Vector3(n0.X, n0.Y, n0.Z);
                            //v1.Normal = Vector3.TransformNormal(norms[tri.V1], rot); //new Vector3(n1.X, n1.Y, n1.Z);
                            //v2.Normal = Vector3.TransformNormal(norms[tri.V2], rot);// new Vector3(n2.X, n2.Y, n2.Z);

                            mm.AddVertices(new Meshes.Vertex[] { v0, v1, v2 });
                            mm.AddTriangle(new Meshes.Triangle(tc * 3, tc * 3 + 1, tc * 3 + 2));
                            tc++;
                        }
                    }
                    if (tc > 0)
                    {
                        Entity.AddMesh(mm);
                        mm.Material = mesh.Material;

                    }

                };

            };

            Console.WriteLine("Created Leaf: Meshes:" + Entity.Meshes.Count);
            foreach(var mesh in Entity.Meshes)
            {
                Console.WriteLine("Mesh: Verts:" + mesh.Vertices.Count + " Tris:" + mesh.Triangles.Count);

            }

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
                    foreach(var node in SubNodes)
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
                    foreach(var mesh in Entity.Meshes)
                    {
                        vc+=mesh.Vertices.Count;

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
                    foreach (var mesh in Entity.Meshes)
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
                    Console.WriteLine("Leaf: Verts:" + LeafVertexCount);
                    

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
            if (Leaf)
            {
                Entity.Render(From.Lights[0], From.MainCamera, true);
                if (Entity.Meshes.Count == 0)
                {
                    //sint bb = 5;
                }
                lc--;
            }
            else
            {
                
                foreach(var node in SubNodes)
                {
                    node.Render();
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
                    if(Entity.Meshes.Count == 0)
                    {
                        return List;
                    }
                    List.Add(this);
                    return List;
                }
                List<OctreeNode> list = new List<OctreeNode>();
                foreach(var node in SubNodes)
                {
                    var leafs = node.Leafs;
                    foreach(var l in leafs)
                    {
                        list.Add(l);
                    }
                    
                }
                return list;
            }
        }

    }
}
