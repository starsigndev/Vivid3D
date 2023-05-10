using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials.Entity
{
    public class MaterialUI3D : MaterialBase
    {

        public MaterialUI3D()
        {

            Shader = new UI3DFX();

        }

    }

    public class UI3DFX : GeminiStandardFX
    {

        public UI3DFX() : base("gemini/shaders/mat_UI3DVS.glsl","gemini/shaders/mat_UI3DFS.glsl")
        {

        }

        public override void SetUniforms()
        {
            base.SetUniforms();

        }

    }
}
