using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Shaders;

namespace Vivid.PostProcesses
{
    public class OutlineFX : ShaderModule
    {
        public OutlineFX() : base("gemini/shaders/pp_outlineVS.glsl","gemini/shaders/pp_outlineFS.glsl")
        {

        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            SetUni("g_TextureDepth", 0);
            var Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f, 1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
        }

    }
    public class PPOutline : PostProcess
    {
        OutlineFX FX1;

        public PPOutline(Vivid.Scene.Scene scene) : base(scene,2)
        {
            FX1 = new OutlineFX();
            int a = 5;
        }

        public override void Process()
        {
            //base.Process();
            BindTarget(0);

            RenderDepth();

            ReleaseTarget(0);

            Draw.Begin();
            Draw.Draw(GetTexture(0), new Maths.Rect(0,Vivid.App.VividApp.FrameHeight,Vivid.App.VividApp.FrameWidth,-Vivid.App.VividApp.FrameHeight), new Maths.Color(1, 1, 1, 1));
            Draw.End(FX1);

        }

    }
}
