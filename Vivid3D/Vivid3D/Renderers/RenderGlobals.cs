using Vivid.Graphics;
using Vivid.Scene;

namespace Vivid.Renderers
{
    public struct Constants
    {
    }

    public static class RenderGlobals
    {
        public static IntPtr MeshRenderer
        {
            get;
            set;
        }

        public static Camera CurrentCamera
        {
            get;
            set;
        }

        public static Node CurrentNode
        {
            get;
            set;
        }

        public static Light CurrentLight
        {
            get;
            set;
        }

        public static GraphicsPipeline MeshLit
        {
            get;
            set;
        }

        public static GraphicsPipeline MeshDepth
        {
            get;
            set;
        }

        public static bool FirstPass = false;

        public static void InitPipelines()
        {
            return;
            MeshLit = new GraphicsPipeline("data/mesh_lit.vsh", "data/mesh_lit.psh");
            int a = 5;
        }
    }
}