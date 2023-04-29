using Vivid.RenderTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;
using Vivid.Texture;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
namespace Vivid.Renderers
{
    public class CubeRenderer
    {

        

        public RenderTargetCube mRT
        {
            get;
            set;
        }
        public Vivid.Scene.Scene Graph
        {
            get;
            set;
        }

        public TextureCube mTexCube
        {
            get;
            set;
        }

        public CubeRenderer(Vivid.Scene.Scene graph,RenderTargetCube rt)
        {

            Graph = graph;
            mRT = rt;
            setMatrices();

        }
        private Camera new_cam = new Camera();
        public void setMatrices()
        {

            m0 = Matrix4.CreateRotationY(MathHelper.Pi / 2.0f);


            m1 = Matrix4.CreateRotationY(-MathHelper.Pi / 2.0f);


            m3 = Matrix4.CreateRotationX(-MathHelper.Pi / 2.0f);


            m2 = Matrix4.CreateRotationX(MathHelper.Pi / 2.0f);


            m4 = Matrix4.Identity;

            m5 = Matrix4.CreateRotationY(MathHelper.Pi);




        }
        Matrix4 m0, m1, m2, m3, m4, m5;
        public void RenderDepth(Vector3 pos, float maxz, OctreeScene ot)
        {

            var pcam = Graph.MainCamera;

            Graph.MainCamera = new_cam;
            new_cam.Position = pos;

            new_cam.DepthStart = pcam.DepthStart;
            new_cam.DepthEnd = maxz;
            new_cam.FOV = 90;


            //ot.BeginComputeVisibility();

            var ShadowFB = mRT;

            Scene.Scene graph = Graph;

            TextureTarget f = ShadowFB.SetFace(0);

            SetCam(f, new_cam);

            // graph.RenderingShadows = true;
          //  ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            SetCam(ShadowFB.SetFace(1), new_cam);
           // ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            // ShadowFB.Release(); graph.CamOverride = null;

            SetCam(ShadowFB.SetFace(2), new_cam);
            //ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            SetCam(ShadowFB.SetFace(3), new_cam);
           // ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            SetCam(ShadowFB.SetFace(4), new_cam);
           // ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            SetCam(ShadowFB.SetFace(5), new_cam);
            //ot.ComputeVisibility();
            ot.RenderDepthLeafs();

            ShadowFB.Release();

            //  mRT.SetFace(i);

      //      ot.EndComputeVisibility();
            

       //     mRT.Release();




            Graph.MainCamera = pcam;

        }
        private static void SetCam(TextureTarget f, Camera Cam)
        {
            switch (f)
            {
                case TextureTarget.TextureCubeMapPositiveX:
                    Cam.LookAtZero(new Vector3(1, 0, 0), new Vector3(0, -1, 0));
                    break;

                case TextureTarget.TextureCubeMapNegativeX:
                    Cam.LookAtZero(new Vector3(-1, 0, 0), new Vector3(0, -1, 0));
                    break;

                case TextureTarget.TextureCubeMapPositiveY:

                    Cam.LookAtZero(new Vector3(0, -1, 0), new Vector3(0, 0, -1));
                    break;

                case TextureTarget.TextureCubeMapNegativeY:
                    Cam.LookAtZero(new Vector3(0, 1, 0), new Vector3(0, 0, 1));
                    break;

                case TextureTarget.TextureCubeMapPositiveZ:
                    Cam.LookAtZero(new Vector3(0, 0, 1), new Vector3(0, -1, 0));
                    break;

                case TextureTarget.TextureCubeMapNegativeZ:
                    Cam.LookAtZero(new Vector3(0, 0, -1), new Vector3(0, -1, 0));
                    break;
            }
        }
        public void RenderDepth(Vector3 pos,float maxz)
        {

            var pcam = Graph.MainCamera;
           
            Graph.MainCamera = new_cam;
            new_cam.Position = pos;
           
            new_cam.DepthStart = pcam.DepthStart;
            new_cam.DepthEnd = maxz;
            new_cam.FOV = 90;

            var ShadowFB = mRT;

            Scene.Scene graph = Graph;

            TextureTarget f = ShadowFB.SetFace(0);

            SetCam(f, new_cam);

            // graph.RenderingShadows = true;

            graph.RenderDepth();

            SetCam(ShadowFB.SetFace(1), new_cam);
            graph.RenderDepth();

            // ShadowFB.Release(); graph.CamOverride = null;

            SetCam(ShadowFB.SetFace(2), new_cam);
            graph.RenderDepth();

            SetCam(ShadowFB.SetFace(3), new_cam);
            graph.RenderDepth();

            SetCam(ShadowFB.SetFace(4), new_cam);
            graph.RenderDepth();

            SetCam(ShadowFB.SetFace(5), new_cam);
            graph.RenderDepth();

            ShadowFB.Release();

            Graph.MainCamera = pcam;

        }


    }
}
