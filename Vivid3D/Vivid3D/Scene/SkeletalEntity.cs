using Assimp;
using Vivid.Anim;
using Vivid.Importing;

using OpenTK.Mathematics;
using Vivid.Meshes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Vivid.Renderers;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Materials.Materials.Skeletal;

namespace Vivid.Scene
{


    public class ImpHelp
    {

        public static Vector3 AssToDpos(Assimp.Vector3D v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }


    }
    public class SkeletalEntity : Entity
    {
        public virtual SkeletalEntity Spawn()
        {

            return null;
        }
        public void AddAnimation(ActorAnimation animation)
        {
            Animations.Add(animation);
        }

        public void PlayAnimation(string name,bool check_pri)
        {

            Animator.SetAnimation(name,check_pri);
        }

        bool first = true;
        public void UpdateAnimation()
        {

            //if (CurrentAnim != null)
            {
                //CurrentAnim.Update();
                Animator.Update();

                var bones = Animator.GetFinalBoneMatrices();

                if (first)
                {
                    for(int i = 0; i < bones.Length; i++)
                    {
                        LocalBones[i] = bones[i];
                    }
                    first = false;
                }

                float ts = 0.2f;

                for(int i = 0; i < bones.Length; i++)
                {

                    var target = bones[i];
                    var current = LocalBones[i];
                    var res = target * ts;
                    res = res + (current * (1.0f - ts));


                    LocalBones[i] = res;

                }
                //LocalBones = bones;

              //  Animator.SetTime(CurrentAnim.CurTime);
            }

        }

        public Matrix4[] LocalBones = new Matrix4[100];



        public Dictionary<string, BoneInfo> m_BoneInfoMap = new Dictionary<string, BoneInfo>();
        public int m_BoneCounter = 0;
        public Dictionary<string, BoneInfo> GetBoneInfoMap()
        {
            return m_BoneInfoMap;
        }
        public ref int GetBoneCount()
        {
            return ref m_BoneCounter;
        }
        Vertex SetVertexBoneDataToDefault(Vertex v)
        {
            for (int i = 0; i < 4; i++)
            {
                v.BoneIDS[i] = -1;
                v.Weights[i] = 0;
            }
            return v;

        }

        public Vivid.Meshes.Mesh ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene)
        {
            var res = new Vivid.Meshes.Mesh(this);
            res.Material = new Materials.Materials.Skeletal.MaterialSkeletalLight();
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vertex v = new Vertex();

                v = SetVertexBoneDataToDefault(v);
                v.Position = ImpHelp.AssToDpos(mesh.Vertices[i]);
                v.Normal = ImpHelp.AssToDpos(mesh.Normals[i]);
                v.Tangent = ImpHelp.AssToDpos(mesh.Tangents[i]);
                v.BiNormal = ImpHelp.AssToDpos(mesh.BiTangents[i]);
                v.TexCoord = ImpHelp.AssToDpos(mesh.TextureCoordinateChannels[0][i]);
                res.AddVertex(v, false);

            }

            ExtractBoneWeightForMesh(res,mesh, scene);

            Meshes.Add(res);

            for(int i = 0; i < mesh.FaceCount; i++)
            {

                Triangle t = new Triangle();
                t.V0 = mesh.Faces[i].Indices[0];
                t.V1 = mesh.Faces[i].Indices[1];
                t.V2 = mesh.Faces[i].Indices[2];

                res.AddTriangle(t);
                Triangle t2 = new Triangle();
                t2.V0 = t.V0;
                t2.V1 = t.V2;
                t2.V2 = t.V1;
             //   res.AddTriangle(t2);

            }

            res.CreateBuffers();

            return res;

        }

        Vertex SetVertexBoneData(Vertex v, int boneID, float weight)
        {
           
            for (int i = 0; i < 4; i++)
            {
                if (v.BoneIDS[i] < 0)
                {
                    v.BoneIDS[i] = boneID;
                    v.Weights[i] = weight;
                    break;
                }
            }

            return v;
        }

        public Matrix4 GlobalInverse;

        void ExtractBoneWeightForMesh(Vivid.Meshes.Mesh mesh, Assimp.Mesh amesh, Assimp.Scene scene)
        {

            for (int boneIndex = 0; boneIndex < amesh.BoneCount; boneIndex++)
            {
                int boneID = -1;
                string boneName = amesh.Bones[boneIndex].Name;
                if (!m_BoneInfoMap.ContainsKey(boneName))
                {

                    BoneInfo bone = new BoneInfo();
                    bone.id = m_BoneCounter;
                    bone.offset = Vivid.Importing.Conv.AssimpMatrixToGLMMatrix(amesh.Bones[boneIndex].OffsetMatrix) ;
                    m_BoneInfoMap.Add(boneName, bone);
                    boneID = m_BoneCounter;
                    m_BoneCounter++;
                }
                else
                {
                    boneID = m_BoneInfoMap[boneName].id;
                }
                if (boneID == -1)
                {
                    Debug.Assert(false);
                }
                var weights = amesh.Bones[boneIndex].VertexWeights;
                int numWeights = amesh.Bones[boneIndex].VertexWeightCount;
                for (int weightIndex = 0; weightIndex < numWeights; weightIndex++)
                {
                    int vertexID = weights[weightIndex].VertexID;
                    float weight = weights[weightIndex].Weight;
                    Debug.Assert(vertexID < mesh.Vertices.Count);
                    var v = SetVertexBoneData(mesh.Vertices[vertexID], boneID, weight);
                    
                    mesh.Vertices[vertexID] = v;
                }
            
            }

        }

        public override void RenderDepth(Camera c,bool ignore_child)
        {
            RenderGlobals.CurrentNode = this as Node;
            //if (first)
            //  {
            Vivid.State.GLState.State = State.CurrentGLState.LightFirstPass;
          

            foreach (var mesh in Meshes)
            {
                var material = mesh.SkeletalDepthMaterial;

                material.Shader.Camera = c;
                material.Shader.Entity = this;
                material.Shader.Light = null;

                var ms = material.Shader as SkeletalDepthFX;

                ms.bones = LocalBones;


                material.Shader.Bind();

               // mesh.Material.ColorMap.Bind(0);
                //mesh.Material.NormalMap.Bind(1);
                //mesh.Material.SpecularMap.Bind(2);
                //l.RTC.Cube.Bind(3);

                mesh.RenderMesh();

                material.Shader.Unbind();
                //mesh.Material.ColorMap.Unbind(0);
                //mesh.Material.NormalMap.Unbind(1);
                //mesh.Material.SpecularMap.Unbind(2);
                //l.RTC.Cube.Release(3);


            }

            foreach (var node in Nodes)
            {
                node.RenderDepth(c);
                //base.RenderDepth();
            }
        }

        public override void Render(Light l,Camera c,bool first)
        {
            //base.Render();
            RenderGlobals.CurrentNode = this as Node;
            if (first)
            {
                Vivid.State.GLState.State = State.CurrentGLState.LightFirstPass;
                // GLHelper.PreRenderStandard(WriteDepth,DepthTest);
            }
            else
            {
                Vivid.State.GLState.State = State.CurrentGLState.LightSecondPass;
                //   GLHelper.PreRenderStandardSecondPass();
            }

            foreach (var mesh in Meshes)
            {
              //  var material = mesh.SkeletalLightMaterial;

                mesh.Material.Shader.Camera = c;
                mesh.Material.Shader.Entity = this;
                mesh.Material.Shader.Light = l;

                //var ms = material.Shader as ActorLightFX;

                var ms = mesh.Material.Shader as ActorLightFX;

                ms.bones = LocalBones;


                mesh.Material.Shader.Bind();

                mesh.Material.ColorMap.Bind(0);
                mesh.Material.NormalMap.Bind(1);
                mesh.Material.SpecularMap.Bind(2);
                l.RTC.Cube.Bind(3);

                mesh.RenderMesh();

                //material.Shader.Unbind();
                //mesh.Material.ColorMap.Unbind(0);
                //mesh.Material.NormalMap.Unbind(1);
                //mesh.Material.SpecularMap.Unbind(2);
                //l.RTC.Cube.Release(3);



                /*
                 * eLightDir2 = RenderGlobals.CurrentLight.Rotation * new vec4(0, 0, 1, 1);
                eLightDir = new vec3(eLightDir2.xyz);


                IntPtr pm = Marshal.AllocHGlobal(Marshal.SizeOf(proj));
                Marshal.StructureToPtr<mat4>(proj, pm, true);
                IntPtr vm = Marshal.AllocHGlobal(Marshal.SizeOf(view));
                Marshal.StructureToPtr<mat4>(view, vm, true);
                IntPtr mm = Marshal.AllocHGlobal(Marshal.SizeOf(model));
                Marshal.StructureToPtr<mat4>(model, mm, true);

                IntPtr camPos, lightPos, lightDiff, lightSpec, lightDir;

                camPos = Marshal.AllocHGlobal(Marshal.SizeOf(eCamPos));
                Marshal.StructureToPtr<vec3>(eCamPos, camPos, true);

                lightPos = Marshal.AllocHGlobal(Marshal.SizeOf(eLightPos));
                Marshal.StructureToPtr<vec3>(eLightPos, lightPos, true);

                lightDiff = Marshal.AllocHGlobal(Marshal.SizeOf(eLightDiff));
                Marshal.StructureToPtr<vec3>(eLightDiff, lightDiff, true);

                lightSpec = Marshal.AllocHGlobal(Marshal.SizeOf(eLightSpec));
                Marshal.StructureToPtr<vec3>(eLightSpec, lightSpec, true);

                lightDir = Marshal.AllocHGlobal(Marshal.SizeOf(eLightDir));
                Marshal.StructureToPtr<vec3>(eLightDir, lightDir, true);

                //gb_MeshAddVertex(GHandle, np);
                //Marshal.FreeHGlobal(np);
                int lightType = (int)RenderGlobals.CurrentLight.Type;

                float cone1 = RenderGlobals.CurrentLight.InnerCone;
                float cone2 = RenderGlobals.CurrentLight.OuterCone;
                //GemBridge.gem_MeshBufferAddVertex(MeshBuffer, np);

                IntPtr rtc = RenderGlobals.CurrentLight.RTC.rtc;

                var act_bones = Animator.GetFinalBoneMatrices();


                List<mat4> bones = new List<mat4>();
                for(int i = 0; i < 100; i++)
                {

                    bones.Add(act_bones[i]);

                }

                mat4[] boneArray = bones.ToArray();

                // Pin the temporary array so that the GC does not move the memory
                GCHandle gcHandle = GCHandle.Alloc(boneArray, GCHandleType.Pinned);

                // Get a pointer to the pinned bones array
                IntPtr bonesPtr = gcHandle.AddrOfPinnedObject();

 int lightType = (int)RenderGlobals.CurrentLight.Type;

                float cone1 = RenderGlobals.CurrentLight.InnerCone;
                float cone2 = RenderGlobals.CurrentLight.OuterCone;
                // Get a pointer to the pinned bones list
                //IntPtr bonesPtr = gcHandle.AddrOfPinnedObject();
                */



                //List<IntPtr> bones = new List<IntPtr>();

                for (int i = 0; i < 100; i++)
                {
                  //  act_bones[i].Debug();
                    //bones.Add(act_bones[i].handle);

                }

               // Matrix4[] boneArray = bones.ToArray();

                // Pin the temporary array so that the GC does not move the memory
                //GCHandle gcHandle = GCHandle.Alloc(boneArray, GCHandleType.Pinned);


                // Get a pointer to the pinned bones array
            //    IntPtr bonesPtr = gcHandle.AddrOfPinnedObject();
           

                //GemBridge.gem_MeshRenderer_RenderActor(RenderGlobals.MeshRenderer, mesh.MeshBuffer, eCamPos.handle, proj.handle, view.handle, model.handle, lightType, eLightDir.handle, cone1, cone2, eLightPos.handle, eLightDiff.handle, eLightSpec.handle, eLightRange, mesh.Material.ColorMap.CObj, mesh.Material.NormalMap.CObj, mesh.Material.SpecularMap.CObj, rtc,ab, true);
                //Marshal.FreeHGlobal(np);
                


            }


            foreach (var node in Nodes)
            {

                node.Render(l,c,first);
            }


        }

        List<ActorAnimation> Animations = new List<ActorAnimation>();
        public ActorAnimation CurrentAnim = null;



        public Animator Animator = null;
    }
}