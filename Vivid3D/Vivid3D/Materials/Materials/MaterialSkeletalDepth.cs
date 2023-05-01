using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Materials.Materials
{
    public  class MaterialSkeletalDepth : MaterialBase
    {

        public MaterialSkeletalDepth()
        {
            Shader = new SkeletalDepthFX();
        }

    }

    public class SkeletalDepthFX : GeminiStandardFX
    {
        public Matrix4[] bones;// = new List<Matrix4>();
        public SkeletalDepthFX() : base("gemini/shaders/mat_skeletaldepthvs.glsl","gemini/shaders/mat_skeletaldepthfs.glsl")
        {

        
        }
        public override void SetUniforms()
        {

            //base.SetUniforms();
            base.SetUniforms();
            for (int i = 0; i < 100; i++)
            {
                SetUni("g_finalBones[" + i.ToString() + "]", bones[i]);
            }
        }

    }

}
