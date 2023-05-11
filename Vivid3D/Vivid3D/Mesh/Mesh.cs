using OpenTK.Mathematics;
using Vivid.Materials;
using Vivid.Mesh;
using Vivid.Scene;

namespace Vivid.Meshes
{
    public class Mesh
    {
        public Entity Owner
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<Vertex> Vertices
        {
            get;
            set;
        }

        public Vertex[] VertexArray
        {
            get
            {
                if (_VertexArray == null)
                {
                    _VertexArray = Vertices.ToArray();
                }
                return _VertexArray;
            }
        }

        private Vertex[] _VertexArray = null;

        public List<Triangle> Triangles
        {
            get;
            set;
        }

        public MaterialBase Material
        {
            get;
            set;
        }

        public MaterialBase LightMaterial
        {
            get;
            set;
        }

        public MaterialBase DepthMaterial
        {
            get;
            set;
        }

        public MaterialBase SkeletalDepthMaterial
        {
            get;
            set;
        }

        public MaterialBase FullBrightMaterial
        {
            get;
            set;
        }

        public MaterialBase SkeletalLightMaterial
        {
            get;
            set;
        }

        public MaterialBase UIMaterial
        {
            get;
            set;
        }
        public MeshBuffer Buffer
        {
            get;
            set;
        }

        public Vector3[] TFPositions = null;

        public Mesh(Entity owner)
        {
            Owner = owner;
            Vertices = new List<Vertex>();
            Triangles = new List<Triangle>();
            Material = new MaterialBase();
            Name = "Mesh";
            Buffer = null;
            FullBrightMaterial = new Materials.Materials.Entity.MaterialStandardFullBright();
            UIMaterial = new Materials.Materials.Entity.MaterialUI3D();
            LightMaterial = new Materials.Materials.Entity.MaterialStandardLight();
            DepthMaterial = new Materials.Materials.Entity.MaterialStandardDepth();
            SkeletalLightMaterial = new Materials.Materials.Skeletal.MaterialSkeletalLight();
            SkeletalDepthMaterial = new Materials.Materials.Skeletal.MaterialSkeletalDepth();
            int bb = 5;
        }

        public void RenderMesh()
        {
            Buffer.Render();
        }

        public void AddVertex(Vertex v, bool reset)
        {
            if (reset)
            {
                for (int i = 0; i < 4; i++)
                {
                    v.BoneIDS[i] = -1;
                    v.Weights[i] = 0.0f;
                }
            }
            Vertices.Add(v);
        }

        public void AddVertices(params Vertex[] vertices)
        {
            foreach (Vertex v in vertices)
            {
                AddVertex(v, false);
            }
        }

        public void AddTriangle(int v0, int v1, int v2)
        {
            Triangle t = new Triangle();
            t.V0 = v0;
            t.V1 = v1;
            t.V2 = v2;
            Triangles.Add(t);
        }

        public void AddTriangles(params Triangle[] triangles)
        {
            foreach (Triangle t in triangles)
            {
                AddTriangle(t);
            }
        }

        public void SetBoneData(int index, int boneID, float weight)
        {
            int MAX_BONE_WEIGHTS = 4;

            bool f = false;
            Vertex vertex = Vertices[index];
            for (int i = 0; i < MAX_BONE_WEIGHTS; ++i)
            {
                if (vertex.BoneIDS[i] < 0)
                {
                    vertex.Weights[i] = weight;
                    vertex.BoneIDS[i] = boneID;

                    break;
                }
            }
            Vertices[index] = vertex;
        }

        public void AddTriangle(Triangle t)
        {
            Triangles.Add(t);
        }

        //Call after mesh is defined or changed.
        public void CreateBuffers()
        {
            Buffer = new MeshBuffer();
            Buffer.SetBuffer(this);
            return;
        }

        public uint[] GenerateIndexData()
        {
            uint[] data = new uint[Triangles.Count * 3];

            int loc = 0;
            foreach (var tri in Triangles)
            {
                data[loc++] = (uint)tri.V0;
                data[loc++] = (uint)tri.V1;
                data[loc++] = (uint)tri.V2;
            }

            return data;
        }

        public float[] GenerateVertexData()
        {
            float[] data = new float[Vertices.Count * 27];

            int loc = 0;

            void wv3(Vector3 v)
            {
                data[loc++] = v.X;
                data[loc++] = v.Y;
                data[loc++] = v.Z;
            }

            void wv4(Vector4 v)
            {
                data[loc++] = v.X;
                data[loc++] = v.Y;
                data[loc++] = v.Z;
                data[loc++] = v.W;
            }

            foreach (var vertex in Vertices)
            {
                wv3(vertex.Position);
                wv4(vertex.Color);
                wv3(vertex.TexCoord);
                wv3(vertex.Normal);
                wv3(vertex.BiNormal);
                wv3(vertex.Tangent);
                wv4(vertex.BoneIDS);
                wv4(vertex.Weights);
            }

            return data;
        }

        private Vector3[] _Pos;

        public Vector3[] Normals
        {
            get
            {
                if (_Normals == null)
                {
                    var Normals = new List<Vector3>();
                    foreach (var v in Vertices)
                    {
                        Normals.Add(new Vector3(v.Normal.X, v.Normal.Y, v.Normal.Z));
                    }
                    _Normals = Normals.ToArray();
                }
                return _Normals;
            }
        }

        private Vector3[] _Normals = null;// new List<Vec3>();

        public Vector3[] Positions
        {
            get
            {
                if (_Pos == null)
                {
                    List<Vector3> pos = new List<Vector3>();
                    int i = 0;
                    foreach (var v in Vertices)
                    {
                        pos.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));
                    }
                    return pos.ToArray();
                    // _Pos = pos.ToArray();
                }

                return _Pos;
            }
        }
    }
}