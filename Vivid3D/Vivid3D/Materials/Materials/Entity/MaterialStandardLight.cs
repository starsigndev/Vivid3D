namespace Vivid.Materials.Materials.Entity
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
        public LightFX() : base("gemini/shaders/mat_lightVS.glsl", "gemini/shaders/mat_lightFS.glsl")
        {
        }

        public LightFX(string vertex,string fragment) : base(vertex, fragment)
        {

        }

        public override void SetUniforms()
        {
            base.SetUniforms();
        }
    }
}