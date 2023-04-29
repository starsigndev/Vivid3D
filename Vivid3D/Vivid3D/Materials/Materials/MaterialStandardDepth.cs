using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials
{
    public  class MaterialStandardDepth : MaterialBase
    {

        public MaterialStandardDepth()
        {

            Shader = new DepthFX();

        }

    }

    public class DepthFX : GeminiStandardFX
    {
        public DepthFX() : base("gemini/shaders/mat_depthVS.glsl", "gemini/shaders/mat_depthFS.glsl")
        {

        }

        public override void SetUniforms()
        {
            base.SetUniforms();
        }

    }

}
