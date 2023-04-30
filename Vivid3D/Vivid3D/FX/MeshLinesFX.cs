using OpenTK.Mathematics;
using Vivid.Scene;
using Vivid.Shaders;

namespace Vivid.FX
{
    public class MeshLinesFX : ShaderModule
    {
        public Camera Camera;

        public MeshLinesFX() : base("gemini/shaders/meshLinesVS.glsl", "gemini/shaders/meshLinesFS.glsl")
        {
        }

        public override void InitUniforms()
        {
        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            SetUni("g_Projection", Camera.Projection);
            SetUni("g_Model", Matrix4.Identity);
            SetUni("g_View", Camera.WorldMatrix);
        }
    }
}