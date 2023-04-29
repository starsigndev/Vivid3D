using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials
{

    public class MaterialStandardLight : MaterialBase
    {

        public MaterialStandardLight()
        {

            Shader = new LightFX();

        }


    }
    public class LightFX : GeminiStandardFX
    {

        public LightFX() : base("gemini/shaders/mat_lightVS.glsl","gemini/shaders/mat_lightFS.glsl")
        {

        }

        public override void SetUniforms()
        {
            base.SetUniforms();

        }

    }
}
