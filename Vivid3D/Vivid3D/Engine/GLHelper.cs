using OpenTK.Graphics.OpenGL;

namespace Vivid.Engine
{
    public class GLHelper
    {
        public static void PreRenderStandardSecondPass()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(TriangleFace.Back);
        }

        public static void PreRenderStandard(bool write_depth, bool depth_test)
        {
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(TriangleFace.Back);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
        }
    }
}