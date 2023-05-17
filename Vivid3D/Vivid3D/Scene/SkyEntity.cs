using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Importing;
using Vivid.Materials.Materials.Sky;
using Vivid.Meshes;
using Vivid.State;

namespace Vivid.Scene
{
    public class SkyEntity : Entity
    {

        SkyFX fx = null;
        public static Entity SkyOrb = null;
        public SkyEntity()
        {

            if (SkyOrb == null)
            {

                SkyOrb = Importer.ImportEntity<Entity>("data/primitive/sphere.fbx");
                Vivid.Meshes.Mesh skyMesh = new Vivid.Meshes.Mesh(this);

                Meshes.Add(skyMesh);

                for (int i = 0; i < SkyOrb.Meshes[0].Vertices.Count;i++) //var vertex in SkyOrb.Meshes[0].Vertices)
                {
                    Vertex vertex = SkyOrb.Meshes[0].Vertices[i];
                    vertex.Position = vertex.Position * 50.0f;
                    skyMesh.AddVertex(vertex,false);
                }
                skyMesh.Triangles = SkyOrb.Meshes[0].Triangles;
                skyMesh.CreateBuffers();
                
               
                Meshes[0].Material = new Materials.Materials.Sky.MaterialSky();
                fx = Meshes[0].Material.Shader as SkyFX;
            }

        }

        public void RenderSky(Camera cam,Light sun)
        {

            GLState.State = CurrentGLState.LightFirstPass;
            OpenTK.Graphics.OpenGL.GL.Disable(OpenTK.Graphics.OpenGL.EnableCap.CullFace);
            Position = cam.Position;
            Position = new OpenTK.Mathematics.Vector3(Position.X, 0, Position.Z);
            fx.SunPosition = sun.Position;

            fx.TopY = Bounds.Max.Y;

            foreach (var mesh in Meshes)
            {
                var material = mesh.LightMaterial;

                mesh.Material.Shader.Camera = cam;
                mesh.Material.Shader.Entity = this;
                mesh.Material.Shader.Light = null;



                mesh.Material.Shader.Bind();

               // mesh.Material.ColorMap.Bind(0);
              //  mesh.Material.NormalMap.Bind(1);
              //  mesh.Material.SpecularMap.Bind(2);
               // l.RTC.Cube.Bind(3);

                mesh.RenderMesh();

                mesh.Material.Shader.Unbind();
             //   mesh.Material.ColorMap.Unbind(0);
              //  mesh.Material.NormalMap.Unbind(1);
             //   mesh.Material.SpecularMap.Unbind(2);
             //   l.RTC.Cube.Release(3);

                //Vec4 eLightDir2;
                //Vec3 eLightDir;

                //eLightDir2 = l.Rotation * new Vec4(0, 0, 1,1);
                //eLightDir = eLightDir2.xyz;

                //IntPtr rtc = l.RTC.rtc;

                //GemBridge.gem_MeshRenderer_RenderLit(RenderGlobals.MeshRenderer, mesh.MeshBuffer,c.Position.handle, c.TransformVector(new Vec3(0, 0, 1)).handle, c.Projection.handle, c.WorldMatrix.handle, WorldMatrix.handle,(int)l.Type,eLightDir.handle,l.InnerCone,l.OuterCone,l.Position.handle ,l.Diffuse.handle,l.Specular.handle ,l.Range, mesh.Material.ColorMap.CObj,mesh.Material.NormalMap.CObj,mesh.Material.SpecularMap.CObj,mesh.Material.DisplaceMap.CObj, rtc,firstPass);
                //Marshal.FreeHGlobal(np);
            }

        }

    }
}
