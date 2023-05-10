using OpenTK.Mathematics;
using Vivid.App;
using Vivid.Shaders;

namespace Vivid.Draw
{
    public class SMDraw2D : ShaderModule
    {
        public Matrix4 Projection = Matrix4.Identity;

        public SMDraw2D() : base("gemini/shaders/drawVS.glsl", "gemini/shaders/drawFS.glsl")
        {
        }

        public override void SetUniforms()
        {
            //          if(Projection == null)
            //            {
            Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f,1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
            SetUni("g_ColorTexture", 0);
        }
    }
}