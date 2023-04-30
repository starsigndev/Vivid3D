using OpenTK.Mathematics;
using Vivid.Materials;
using Vivid.Meshes;
using Vivid.Physx;
using Vivid.Scene;

namespace Vivid.IO
{
    public static class FileHelp
    {
        public static void WriteVec3(BinaryWriter w, Vector3 v)
        {
            w.Write(v.X);
            w.Write(v.Y);
            w.Write(v.Z);
        }

        public static Vector3 ReadVec3(BinaryReader r)
        {
            Vector3 res = new Vector3();

            res.X = r.ReadSingle();
            res.Y = r.ReadSingle();
            res.Z = r.ReadSingle();

            return res;
        }

        public static Matrix4 ReadMat4(BinaryReader r)
        {
            Matrix4 res = new Matrix4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    res[i, j] = r.ReadSingle();
                }
            }

            return res;
        }

        public static void WriteMat4(BinaryWriter w, Matrix4 m)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    w.Write(m[i, j]);
                }
            }
        }

        public static void WriteNodeData(BinaryWriter w, Vivid.Scene.Node node)
        {
            WriteVec3(w, node.Position);
            WriteMat4(w, node.Rotation);
            WriteVec3(w, node.Scale);
            w.Write(node.Name);
            w.Write(node.Enabled);
            w.Write(node.NodeType);
        }

        public static void ReadNodeData(Vivid.Scene.Node node, BinaryReader r)
        {
            node.Position = ReadVec3(r);
            node.Rotation = ReadMat4(r);
            node.Scale = ReadVec3(r);
            node.Name = r.ReadString();
            node.Enabled = r.ReadBoolean();
            node.NodeType = r.ReadString();
        }

        public static void WriteMaterial(BinaryWriter w, Vivid.Materials.MaterialBase mat)
        {
            w.Write(mat.ColorMap.Path);
            w.Write(mat.NormalMap.Path);
            w.Write(mat.SpecularMap.Path);
        }
        public static Vivid.Meshes.Mesh ReadMesh(BinaryReader r)
        {
            Vector3 Readdpos3()
            {
                float x, y, z;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                return new Vector3(x, y, z);
            }

            Vector4 Readdpos4()
            {
                float x, y, z, w;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                w = r.ReadSingle();
                return new Vector4(x, y, z, w);
            }
            int vc = r.ReadInt32();
            Vivid.Meshes.Mesh mesh = new Meshes.Mesh(null);
            for (int i = 0; i < vc; i++)
            {
                Vertex v = new Vertex();
                v.Position = Readdpos3();
                v.Normal = Readdpos3();
                v.Tangent = Readdpos3();
                v.BiNormal = Readdpos3();
                v.Color = Readdpos4();
                v.TexCoord = Readdpos3();
                v.BoneIDS = Readdpos4();
                v.Weights = Readdpos4();
                mesh.AddVertex(v, false);

            }
            int tc = r.ReadInt32();
            for(int i = 0; i < tc; i++)
            {
                int v0, v1, v2;

                v0 = r.ReadInt32();
                v1 = r.ReadInt32();
                v2 = r.ReadInt32();

                Triangle tri = new Triangle();
                tri.V0 = v0;
                tri.V1 = v1;
                tri.V2 = v2;
                mesh.AddTriangle(tri);
                    
            }

            mesh.Material = ReadMaterial(r);

            mesh.CreateBuffers();

            return mesh;

        }
        public static void WriteMesh(BinaryWriter w, Vivid.Meshes.Mesh mesh)
        {
            void Writedpos3(Vector3 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
            }
            void Writedpos4(Vector4 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
                w.Write(p.W);
            }

            w.Write(mesh.Vertices.Count);
            foreach (var v in mesh.Vertices)
            {
                Writedpos3(v.Position);
                Writedpos3(v.Normal);
                Writedpos3(v.Tangent);
                Writedpos3(v.BiNormal);
                Writedpos4(v.Color);
                Writedpos3(v.TexCoord);
                Writedpos4(v.BoneIDS);
                Writedpos4(v.Weights);
            }

            w.Write(mesh.Triangles.Count);
            foreach (var t in mesh.Triangles)
            {
                w.Write(t.V0);
                w.Write(t.V1);
                w.Write(t.V2);
            }

            WriteMaterial(w, mesh.Material);

        }
        public static void WriteMeshData(BinaryWriter w, Entity ent)
        {
            void Writedpos3(Vector3 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
            }
            void Writedpos4(Vector4 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
                w.Write(p.W);
            }
            w.Write(ent.Meshes.Count);
            foreach (var mesh in ent.Meshes)
            {
                w.Write(mesh.Vertices.Count);
                foreach (var v in mesh.Vertices)
                {
                    Writedpos3(v.Position);
                    Writedpos3(v.Normal);
                    Writedpos3(v.Tangent);
                    Writedpos3(v.BiNormal);
                    Writedpos4(v.Color);
                    Writedpos3(v.TexCoord);
                    Writedpos4(v.BoneIDS);
                    Writedpos4(v.Weights);
                }

                w.Write(mesh.Triangles.Count);
                foreach (var t in mesh.Triangles)
                {
                    w.Write(t.V0);
                    w.Write(t.V1);
                    w.Write(t.V2);
                }

                WriteMaterial(w, mesh.Material);
            }
        }

        public static void WritePhysicsData(BinaryWriter w, Entity ent)
        {
            w.Write((int)ent.BodyKind);
        }

        public static void WriteEntityData(BinaryWriter w, Entity ent)
        {
            WriteNodeData(w, ent);
            WriteMeshData(w, ent);
            WritePhysicsData(w, ent);
        }

        // public static void WriteSkeletalData(BinaryWriter w, SkeletalEntity actor)
        //{
        //   WriteEntityData(w, actor);
        //}
        public static void WriteLightData(BinaryWriter w, Light light)
        {
            WriteNodeData(w, light);
            WriteVec3(w, light.Diffuse);
            WriteVec3(w, light.Specular);
            w.Write(light.Range);
            w.Write(light.CastShadows);
            w.Write(light.InnerCone);
            w.Write(light.OuterCone);
            w.Write(light.VolumetricShafts);
        }

        public static void WriteSpawnData(BinaryWriter w, SpawnPoint spawn)
        {
            WriteNodeData(w, (Node)spawn);
            w.Write((int)spawn.Index);
            w.Write(spawn.Type);
        }

        public static Materials.MaterialBase ReadMaterial(BinaryReader r)
        {
            MaterialBase mat = new MaterialBase();

            string col = r.ReadString();
            string norm = r.ReadString();
            string spec = r.ReadString();
            string col1 = Path.GetFileName(col);
            string norm1 = Path.GetFileName(norm);
            string spec1 = Path.GetFileName(spec);

            var cc = Content.Content.GlobalFindItem(col1);
            var nc = Content.Content.GlobalFindItem(norm1);
            var sc = Content.Content.GlobalFindItem(spec1);

            if (cc != null)
            {
                mat.ColorMap = new Texture.Texture2D(cc.GetStream(), cc.Width, cc.Height, cc.FullName);
                mat.NormalMap = new Texture.Texture2D(nc.GetStream(), nc.Width, nc.Height, nc.FullName);
                if (sc != null)
                {
                    mat.SpecularMap = new Texture.Texture2D(sc.GetStream(), sc.Width, sc.Height, sc.FullName);
                }
            }
            else
            {
                mat.ColorMap = new Texture.Texture2D(col);
                mat.NormalMap = new Texture.Texture2D(norm);
                mat.SpecularMap = new Texture.Texture2D(spec);
                //int b = 5;
            }

            return mat;
        }

        public static void ReadMeshData(Entity ent, BinaryReader r)
        {
            Vector3 Readdpos3()
            {
                float x, y, z;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                return new Vector3(x, y, z);
            }

            Vector4 Readdpos4()
            {
                float x, y, z, w;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                w = r.ReadSingle();
                return new Vector4(x, y, z, w);
            }

            int mesh_c = r.ReadInt32();
            for (int i = 0; i < mesh_c; i++)
            {
                Vivid.Meshes.Mesh mesh = new Vivid.Meshes.Mesh(ent);

                int vert_c = r.ReadInt32();
                for (int v = 0; v < vert_c; v++)
                {
                    Vertex vert = new Vertex();
                    vert.Position = Readdpos3();
                    vert.Normal = Readdpos3();
                    vert.Tangent = Readdpos3();
                    vert.BiNormal = Readdpos3();
                    vert.Color = Readdpos4();
                    vert.TexCoord = Readdpos3();
                    vert.BoneIDS = Readdpos4();
                    vert.Weights = Readdpos4();
                    mesh.AddVertex(vert, false);
                }

                int tri_c = r.ReadInt32();
                for (int t = 0; t < tri_c; t++)
                {
                    Triangle tri = new Triangle();

                    tri.V0 = r.ReadInt32();
                    tri.V1 = r.ReadInt32();
                    tri.V2 = r.ReadInt32();

                    mesh.AddTriangle(tri);
                }

                mesh.Material = ReadMaterial(r);
                mesh.CreateBuffers();
                ent.AddMesh(mesh);
            }
        }

        public static void ReadPhysicsData(Entity ent, BinaryReader r)
        {
            ent.BodyKind = (BodyType)r.ReadInt32();
        }

        public static void ReadEntityData(Entity ent, BinaryReader r)
        {
            ReadNodeData(ent as Node, r);
            ReadMeshData(ent, r);
            ReadPhysicsData(ent, r);
        }

        public static void ReadLightData(Light light, BinaryReader r)
        {
            ReadNodeData(light as Node, r);
            light.Diffuse = ReadVec3(r);
            light.Specular = ReadVec3(r);
            light.Range = r.ReadSingle();
            light.CastShadows = r.ReadBoolean();
            light.InnerCone = r.ReadSingle();
            light.OuterCone = r.ReadSingle();
            light.VolumetricShafts = r.ReadBoolean();
        }

        public static void ReadSpawnData(SpawnPoint spawn, BinaryReader r)
        {
            ReadNodeData(spawn as Node, r);
            spawn.Index = r.ReadInt32();
            spawn.Type = r.ReadString();
        }
    }
}