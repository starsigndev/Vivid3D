using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials.Skeletal
{
    public class MaterialSkeletalCelShaded : MaterialBase
    {

        public MaterialSkeletalCelShaded()
        {

            Shader = new ActorCelShadedFX();

        }

    }

    public class ActorCelShadedFX : ActorLightFX
    {
        public Matrix4[] bones;// = new List<Matrix4>();
        public float CelShadeValue
        {
            get;
            set;
        }
        public ActorCelShadedFX() : base("gemini/shaders/mat_skeletalCelShadeVS.glsl", "gemini/shaders/mat_skeletalCelShadeFS.glsl")
        {
            CelShadeValue = 0.25f;
        }

        public override void SetUniforms()
        {
            base.SetUniforms();
            SetUni("g_CelShadeValue", CelShadeValue);

        }
    }

}
