namespace Vivid.Materials.Materials.Entity
{
    public class FullBrightFX : GeminiStandardFX
    {
        public FullBrightFX() : base("gemini/shaders/mat_FullBrightVS.glsl", "gemini/shaders/mat_FullBrightFS.glsl")
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