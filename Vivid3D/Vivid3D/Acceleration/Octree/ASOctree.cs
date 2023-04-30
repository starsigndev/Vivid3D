using Vivid.App;
using Vivid.RenderTarget;
using Vivid.State;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Vivid.Acceleration.Octree
{
    public class ASOctree
    {
        public Scene.Scene Base
        {
            get;
            set;
        }

        public OctreeNode RootNode
        {
            get;
            set;
        }

        public static int VertexLeafLimit = 1024;

        public ASOctree(Scene.Scene scene)
        {
            Base = scene;

            ProcessScene();
            RootNode.CreateBuffers();
            LightFX = new Materials.Materials.LightFX();
            InitializeVisibility();
        }

        public void ProcessScene()
        {
            RootNode = new OctreeNode(Base.Bounds, Base);
        }

        public int LeafCount()
        {
            return RootNode.LeafCount;
        }

        public int VertexCount()
        {
            return RootNode.VertexCount;
        }

        public int TriangleCount()
        {
            return RootNode.TriangleCount;
        }

        public void Debug()
        {
            RootNode.Debug();
        }

        public void Render()
        {
            LightFX.Camera = Base.MainCamera;
            LightFX.Entity = null;
            GLState.State = CurrentGLState.LightFirstPass;
            foreach (var light in Base.Lights) {
                LightFX.Light = light;
                LightFX.Bind();
                light.RTC.Cube.Bind(3);
                RootNode.Render();
                GLState.State = CurrentGLState.LightSecondPass;
            }
            LightFX.Unbind();
            Console.WriteLine("Leafs Rendered:" + OctreeNode.LeafsRendered);
        }
        public void InitializeVisibility()
        {
            RootNode.InitializeVisibility();
        }

        public void ComputeVisibility()
        {
            OctreeNode.LeafsRendered = 0;

            //   GL.ColorMask(false, false, false, false);
            //    GL.DepthMask(false);
            GL.Enable(EnableCap.ScissorTest);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            GL.Scissor(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
            RootNode.ComputeVisibility();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            // GL.DepthMask(true);
            GL.Disable(EnableCap.ScissorTest);
            // GL.ColorMask(true, true, true, true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //vis_rt.Release();



        }

        public List<OctreeNode> GetLeafs()
        {
            return RootNode.Leafs;
        }
        private Vivid.Materials.Materials.LightFX LightFX = null;
    }
}