using Assimp;

using Vivid.Anim;
using OpenTK.Mathematics;
using Vivid.Meshes;
using OpenTK.Graphics.OpenGL;

namespace Vivid.Importing
{
    public static class Conv
    {
        public static Matrix4 AssimpMatrixToGLMMatrix(Assimp.Matrix4x4 assimpMat)
        {
            var gemMat = new Matrix4();

            gemMat[0, 0] = assimpMat.A1;
            gemMat[1, 0] = assimpMat.A2;
            gemMat[2, 0] = assimpMat.A3;
            gemMat[3, 0] = assimpMat.A4;

            gemMat[0, 1] = assimpMat.B1;
            gemMat[1, 1] = assimpMat.B2;
            gemMat[2, 1] = assimpMat.B3;
            gemMat[3, 1] = assimpMat.B4;

            gemMat[0, 2] = assimpMat.C1;
            gemMat[1, 2] = assimpMat.C2;
            gemMat[2, 2] = assimpMat.C3;
            gemMat[3, 2] = assimpMat.C4;

            gemMat[0, 3] = assimpMat.D1;
            gemMat[1, 3] = assimpMat.D2;
            gemMat[2, 3] = assimpMat.D3;
            gemMat[3, 3] = assimpMat.D4;

            return gemMat;
            gemMat[0, 0] = assimpMat.A1;
            gemMat[0, 1] = assimpMat.B1;
            gemMat[0, 2] = assimpMat.C1;
            gemMat[0, 3] = assimpMat.D1;

            gemMat[1, 0] = assimpMat.A2;
            gemMat[1, 1] = assimpMat.B2;
            gemMat[1, 2] = assimpMat.C2;
            gemMat[1, 3] = assimpMat.D2;

            gemMat[2, 0] = assimpMat.A3;
            gemMat[2, 1] = assimpMat.B3;
            gemMat[2, 2] = assimpMat.C3;
            gemMat[2, 3] = assimpMat.D3;

            gemMat[3, 0] = assimpMat.A4;
            gemMat[3, 1] = assimpMat.B4;
            gemMat[3, 2] = assimpMat.C4;
            gemMat[3, 3] = assimpMat.D4;

            return gemMat;

            /*
            return new mat4(
                matrix.A1, matrix.B1, matrix.C1, matrix.D1,
                matrix.A2, matrix.B2, matrix.C2, matrix.D2,
                matrix.A3, matrix.B3, matrix.C3, matrix.D3,
                matrix.A4, matrix.B4, matrix.C4, matrix.D4
            );
            */
        }
    }
    public class MediaSource
    {
        public string name = "";
        public string path = "";

    }
    public class Importer
    {
        public static List<MediaSource> Sources = new List<MediaSource>();
        public static void AddSource(string name,string path)
        {
            MediaSource src = new MediaSource();
            src.name = name;
            src.path = path;
            Sources.Add(src);
        }
        /*
        public static T ImportSkeletalEntity<T>(MemoryStream stream) where T : Vivid.Scene.SkeletalEntity, new()
        {
            var imp = new Assimp.AssimpContext();

            var result = new T();

            List<Meshes.Mesh> meshes = new List<Meshes.Mesh>();

            Assimp.Scene s = imp.ImportFileFromStream(stream, PostProcessSteps.GenerateNormals | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.OptimizeGraph);

            var m_GlobalInverseTransform = s.RootNode.Transform;
            m_GlobalInverseTransform.Inverse();
            List<Vivid.Materials.Material> mats = new List<Materials.Material>();
            foreach (var mat in s.Materials)
            {
                    Vivid.Materials.Material nmat = new Materials.Material();
                    mats.Add(nmat);
                if (mat.HasTextureDiffuse)
                {
                    string tex = mat.TextureDiffuse.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    nmat.ColorMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                    //   int a = 5;
                }
            }

            foreach (var mesh in s.Meshes)
            {
                Meshes.Mesh gMesh = new Meshes.Mesh(result);
                gMesh.Material = mats[mesh.MaterialIndex];

                result.ProcessMesh(mesh, s);
            }

            Vivid.Anim.Animation anim = new Anim.Animation(s, result);
            Vivid.Anim.Animator animator = new Animator(anim);

            //node_anim =

            result.Animator = animator;
            result.Animator.Entity = result;

            Assimp.Matrix4x4 tf = s.RootNode.Transform;

            tf.Inverse();
            result.GlobalInverse = Conv.AssimpMatrixToGLMMatrix(tf);

            return result;
        }
        *
        *
        */
        public static void ImportAnimation(Vivid.Scene.SkeletalEntity entity,string path)
        {
            var imp = new Assimp.AssimpContext();

            Assimp.Scene s = imp.ImportFile(path );

            foreach (var aa in s.Animations)
            {
                Vivid.Anim.Animation anim = new Anim.Animation(s,aa, entity);
                entity.Animator.m_Animations.Add(anim);
                entity.Animator.m_CurrentAnimation = anim;
            }
        }
        public static T ImportSkeletalEntity<T>(string path) where T : Vivid.Scene.SkeletalEntity, new()
        {
            var imp = new Assimp.AssimpContext();

            var result = new T();

            List<Meshes.Mesh> meshes = new List<Meshes.Mesh>();

            Assimp.Scene s = imp.ImportFile(path, PostProcessSteps.GenerateNormals | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.Triangulate);

            var m_GlobalInverseTransform = s.RootNode.Transform;
            m_GlobalInverseTransform.Inverse();

            foreach (var mesh in s.Meshes)
            {
                Meshes.Mesh gMesh = new Meshes.Mesh(result);

                result.ProcessMesh(mesh, s);
            }

            Vivid.Anim.Animation anim = new Anim.Animation(s,s.Animations[0], result);
            Vivid.Anim.Animator animator = new Animator(anim);

            animator.m_Animations.Add(anim);
            //node_anim =

            result.Animator = animator;
            result.Animator.Entity = result;

            Assimp.Matrix4x4 tf = s.RootNode.Transform;

            tf.Inverse();
            result.GlobalInverse = Conv.AssimpMatrixToGLMMatrix(tf);

            return result;
        }
   

        /*
        public static T ImportActor<T>(string path) where T: Vivid.Scene.Actor, new()
        {
            var imp = new Assimp.AssimpContext();

            var actor = new T();

            Assimp.Scene s = imp.ImportFile(path, PostProcessSteps.GenerateNormals | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.OptimizeGraph);

            var m_GlobalInverseTransform = s.RootNode.Transform;
            m_GlobalInverseTransform.Inverse();

            return null;

            return actor;
        }

        */

        public static T ImportEntity<T>(MemoryStream stream) where T : Vivid.Scene.Entity, new()
        {
            var imp = new Assimp.AssimpContext();

            T result = new T();

            Assimp.Scene s = imp.ImportFileFromStream(stream, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals | PostProcessSteps.CalculateTangentSpace);

            List<Meshes.Mesh> meshes = new List<Meshes.Mesh>();

            List<Vivid.Materials.MaterialBase> mats = new List<Materials.MaterialBase>();
            foreach (var mat in s.Materials)
            {
                Vivid.Materials.MaterialBase nmat = new Materials.MaterialBase();
                mats.Add(nmat);
                if (mat.HasTextureDiffuse)
                {
                    string tex = mat.TextureDiffuse.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    nmat.ColorMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                    //   int a = 5;
                }
                if (mat.HasTextureNormal)
                {
                    string tex = mat.TextureNormal.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    nmat.NormalMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                }
                if (mat.HasTextureSpecular)
                {
                    string tex = mat.TextureSpecular.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    nmat.SpecularMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                }
            }

            foreach (var mesh in s.Meshes)
            {
                Meshes.Mesh gMesh = new Meshes.Mesh(result);
                gMesh.Material = mats[mesh.MaterialIndex];
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    Meshes.Vertex nv = new Meshes.Vertex();
                    nv.Position = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);
                    nv.Normal = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);
                    nv.TexCoord = new Vector3(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y, 0);
                    nv.Color = new Vector4(1, 1, 1, 1);
                    nv.BiNormal = new Vector3(mesh.BiTangents[i].X, mesh.BiTangents[i].Y, mesh.BiTangents[i].Z);
                    nv.Tangent = new Vector3(mesh.Tangents[i].X, mesh.Tangents[i].Y, mesh.Tangents[i].Z);

                    gMesh.AddVertex(nv, false);
                }

                for (int i = 0; i < mesh.FaceCount; i++)
                {
                    Triangle tri = new Triangle();
                    tri.V0 = mesh.Faces[i].Indices[0];
                    tri.V1 = mesh.Faces[i].Indices[1];
                    tri.V2 = mesh.Faces[i].Indices[2];

                    gMesh.AddTriangle(tri);
                }

                result.AddMesh(gMesh);

                gMesh.CreateBuffers();
            }

            return result;
        }

        public static T ImportEntity<T>(string path) where T : Vivid.Scene.Entity, new()
        {
            var imp = new Assimp.AssimpContext();

            T result = new T();

            Assimp.Scene s = imp.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals | PostProcessSteps.CalculateTangentSpace);

            result.Name = s.RootNode.Name;

            // result.Rotation = Conv.AssimpMatrixToGLMMatrix(s.RootNode.Transform);

            List<Meshes.Mesh> meshes = new List<Meshes.Mesh>();

            List<Vivid.Materials.MaterialBase> mats = new List<Materials.MaterialBase>();
            foreach (var mat in s.Materials)
            {
                Vivid.Materials.MaterialBase nmat = new Materials.MaterialBase();
                mats.Add(nmat);
                if (mat.HasTextureDiffuse)
                {
                    string tex = mat.TextureDiffuse.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    if (item == null)
                    {
                        if (File.Exists(mat.TextureDiffuse.FilePath))
                        {
                            nmat.ColorMap = new Texture.Texture2D(mat.TextureDiffuse.FilePath);
                        }
                        else
                        {

                            var col_path = Path.GetFileName(mat.TextureDiffuse.FilePath);
                            foreach(var src in Sources)
                            {

                                var name = src.name;

                                if (name == col_path)
                                {
                                    //int bb = 5;
                                    nmat.ColorMap = new Texture.Texture2D(src.path);

                                }

                            }



                        }
                    }
                    else
                    {
                        nmat.ColorMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                        //   int a = 5;
                    }
                }
                if (mat.HasTextureNormal)
                {
                    string tex = mat.TextureNormal.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);

                    if (item == null)
                    {
                        if (File.Exists(mat.TextureNormal.FilePath))
                        {
                            nmat.NormalMap = new Texture.Texture2D(mat.TextureNormal.FilePath);
                        }
                        else
                        {
                            var norm_path = Path.GetFileName(mat.TextureNormal.FilePath);
                            foreach (var src in Sources)
                            {

                                var name = src.name;

                                if (name == norm_path)
                                {
                                    //int bb = 5;
                                    nmat.NormalMap = new Texture.Texture2D(src.path);

                                }

                            }
                        }
                    }
                    else
                    {
                        nmat.NormalMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                        //   int a = 5;
                    }

                    //nmat.NormalMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                }
                if (mat.HasTextureSpecular)
                {
                    string tex = mat.TextureSpecular.FilePath;
                    tex = Path.GetFileName(tex);
                    var item = Content.Content.GlobalFindItem(tex);
                    if (item == null)
                    {
                        if (File.Exists(mat.TextureSpecular.FilePath))
                        {
                            nmat.SpecularMap = new Texture.Texture2D(mat.TextureSpecular.FilePath);
                        }
                        else
                        {
                            var spec_path = Path.GetFileName(mat.TextureSpecular.FilePath);
                            foreach (var src in Sources)
                            {

                                var name = src.name;

                                if (name == spec_path)
                                {
                                    //int bb = 5;
                                    nmat.SpecularMap = new Texture.Texture2D(src.path);

                                }

                            }
                        }
                    }
                    else
                    {
                        nmat.SpecularMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                        //   int a = 5;
                    }
                    //nmat.SpecularMap = new Texture.Texture2D(item.GetStream(), item.Width, item.Height);
                }
            }

            foreach (var mesh in s.Meshes)
            {
                Meshes.Mesh gMesh = new Meshes.Mesh(result);
                gMesh.Material = mats[mesh.MaterialIndex];

                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    Meshes.Vertex nv = new Meshes.Vertex();
                    nv.Position = new Vector3(-mesh.Vertices[i].X, mesh.Vertices[i].Z, mesh.Vertices[i].Y);
                    nv.Normal = new Vector3(-mesh.Normals[i].X, mesh.Normals[i].Z, mesh.Normals[i].Y);
                    nv.TexCoord = new Vector3(mesh.TextureCoordinateChannels[0][i].X, 1.0f - mesh.TextureCoordinateChannels[0][i].Y, 0);
                    nv.Color = new Vector4(1, 1, 1, 1);
                    nv.BiNormal = new Vector3(-mesh.BiTangents[i].X, mesh.BiTangents[i].Z, mesh.BiTangents[i].Y);
                    nv.Tangent = new Vector3(-mesh.Tangents[i].X, mesh.Tangents[i].Z, mesh.Tangents[i].Y);

                    gMesh.AddVertex(nv, false);
                }

                for (int i = 0; i < mesh.FaceCount; i++)
                {
                    Triangle tri = new Triangle();
                    tri.V0 = mesh.Faces[i].Indices[0];
                    tri.V1 = mesh.Faces[i].Indices[1];
                    tri.V2 = mesh.Faces[i].Indices[2];

                    gMesh.AddTriangle(tri);
                }

                result.AddMesh(gMesh);

                gMesh.CreateBuffers();
            }

            return result;
        }
    }
}