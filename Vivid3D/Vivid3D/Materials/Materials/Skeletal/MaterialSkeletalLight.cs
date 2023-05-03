using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Vivid.Materials.Materials.Skeletal
{
    public class MaterialSkeletalLight : MaterialBase
    {

        public MaterialSkeletalLight()
        {
            Shader = new ActorLightFX();
        }

    }

    public class ActorLightFX : GeminiStandardFX
    {
        public Matrix4[] bones;// = new List<Matrix4>();
        public ActorLightFX() : base("gemini/shaders/mat_skeletallightVS.glsl", "gemini/shaders/mat_skeletallightFS.glsl")
        {
        }
        public ActorLightFX(string vertex,string fragment) : base(vertex, fragment)
        {

        }
        public override void SetUniforms()
        {
            base.SetUniforms();
            for (int i = 0; i < 100; i++)
            {
                SetUni("g_finalBones[" + i.ToString() + "]", bones[i]);
            }
            int aa = 5;
        }
    }

}
