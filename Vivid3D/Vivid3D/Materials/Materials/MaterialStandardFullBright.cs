﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials
{
    public class FullBrightFX : GeminiStandardFX
    {
        public FullBrightFX() : base("gemini/shaders/mat_FullBrightVS.glsl","gemini/shaders/mat_FullBrightFS.glsl")
        {

        }

        public override void SetUniforms()
        {
            base.SetUniforms();
        }

    }
    public class MaterialStandardFullBright : MaterialBase
    {
        public MaterialStandardFullBright()
        {

            Shader = new FullBrightFX();

        }
    }
}
