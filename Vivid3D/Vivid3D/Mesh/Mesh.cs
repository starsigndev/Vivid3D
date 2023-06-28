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

        public BoundingBox BB
        {
            get
            {
                if (_BB == null)
                {
                    _BB = Owner.ComputeMeshBoundingBox(this, false);
                }
                return _BB;
            }
            set
            {
                _BB = value;
            }
        }
        BoundingBox _BB = null;

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
        public enum NormalsType
        {
            Smooth,Flat
        }
        public void CalculateNormals(NormalsType type)
        {
            switch (type)
            {
                case NormalsType.Smooth:

                    Vector3[] verts = new Vector3[Vertices.Count];

                    int i = 0;
                    foreach(var v in Vertices)
                    {
                        verts[i] = v.Position;
                        i++;
                    }

                    int[] tris = new int[Triangles.Count * 3];

                    i = 0;
                    foreach(var t in Triangles)
                    {
                        tris[i++] = t.V0;
                        tris[i++] = t.V1;
                        tris[i++] = t.V2;
                    }

                    var norms = CalculateSmoothNormals(verts,tris);

                    i = 0;
                   for(i = 0; i < Vertices.Count; i++)
                    {
                        //Vertices[i].Normal = norms[i];
                        var v = Vertices[i];
                        v.Normal = norms[i];
                        Vertices[i] = v;
                    }

                    break;
            }
        }

        public static Vector3[] CalculateSmoothNormals(Vector3[] vertices, int[] triangles)
        {
            // Step 1: Initialize a dictionary to store the accumulated normals for each vertex
            Vector3[] normals = new Vector3[vertices.Length];

            // Step 2: Iterate over each triangle
            for (int i = 0; i < triangles.Length; i += 3)
            {
                // Step 3: Get the indices of the triangle's vertices
                int index1 = triangles[i];
                int index2 = triangles[i + 1];
                int index3 = triangles[i + 2];

                // Step 4: Calculate the face normal of the current triangle
                Vector3 side1 = vertices[index2] - vertices[index1];
                Vector3 side2 = vertices[index3] - vertices[index1];
                Vector3 faceNormal = Vector3.Cross(side1, side2).Normalized();

                // Step 5: Accumulate the face normal for each vertex of the triangle
                normals[index1] += faceNormal;
                normals[index2] += faceNormal;
                normals[index3] += faceNormal;
            }

            // Step 6: Normalize the accumulated normals for each vertex
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = normals[i].Normalized();
            }

            return normals;
        }

        private static void AccumulateVertexNormal(Dictionary<int, Vector3> vertexNormals, int vertexIndex, Vector3 normal)
        {
            if (vertexNormals.ContainsKey(vertexIndex))
            {
                vertexNormals[vertexIndex] += normal;
            }
            else
            {
                vertexNormals.Add(vertexIndex, normal);
            }
        }
    }
}