using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Materials.Materials.Skeletal;

namespace Vivid.Materials.Materials.Entity
{
    public class MaterialCelShaded : MaterialBase
    {

        public MaterialCelShaded()
        {
            Shader = new CelShadedFX();
        }

    }

    public class CelShadedFX : LightFX
    {
        public float CelShadeValue
        {
            get;
            set;
        }
        public CelShadedFX() : base("gemini/shaders/mat_CelShadedVS.glsl","gemini/shaders/mat_CelShadedFS.glsl")
        {
            CelShadeValue = 0.45f;
        }

        public override void SetUniforms()
        {
            base.SetUniforms();
            SetUni("g_CelShadeValue", CelShadeValue);
        }

    }

}
