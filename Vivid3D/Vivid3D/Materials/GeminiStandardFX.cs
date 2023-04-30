using OpenTK.Mathematics;
using Vivid.Shaders;

namespace Vivid.Materials
{
    public class GeminiStandardFX : ShaderModule
    {
        public Vivid.Scene.Camera Camera;
        public Vivid.Scene.Entity Entity;
        public Vivid.Scene.Light Light = null;

        public GeminiStandardFX(string vertex_path, string fragment_path) : base(vertex_path, fragment_path)
        {
        }

        private int g_Proj, g_Model, g_View;
        private int g_LightPos, g_LightDiff, g_LightSpec, g_LightRange, g_LightDepth;
        private int g_CamPos, g_CamMinZ, g_CamMaxZ;

        public override void SetUniforms()
        {
            //base.SetUniforms();
            //InitUniforms();

            SetUni(g_Proj, Camera.Projection);
            if (Entity == null)
            {
                SetUni(g_Model, Matrix4.Identity);
            }
            else
            {
                SetUni(g_Model, Entity.WorldMatrix);
            }
            SetUni(g_View, Camera.WorldMatrix);
            if (Light != null)
            {
                SetUni(g_LightPos, Light.Position);
                SetUni(g_LightDiff, Light.Diffuse);
                SetUni(g_LightSpec, Light.Specular);
                SetUni(g_LightRange, Light.Range);
                SetUni(g_LightDepth, Light.Range);
            }
            if (Camera != null)
            {
                SetUni(g_CamPos, Camera.Position);
                //  SetUni("g_CameraDir", Camera.TransformVector(new Vector3(0, 0, 1)));
                SetUni(g_CamMinZ, Camera.DepthStart);
                SetUni(g_CamMaxZ, Camera.DepthEnd);
            }
            //SetUni("g_TextureColor", 0);
            // SetUni("g_TextureNormal", 1);
            //  SetUni("g_TextureSpecular", 2);
            //   SetUni("g_TextureShadow", 3);
        }

        public override void InitUniforms()
        {
            //base.InitUniforms();
            g_Proj = GetLocation("g_Projection");
            g_View = GetLocation("g_View");
            g_Model = GetLocation("g_Model");
            g_LightPos = GetLocation("g_LightPosition");
            g_LightDiff = GetLocation("g_LightDiffuse");
            g_LightSpec = GetLocation("g_LightSpecular");
            g_LightRange = GetLocation("g_LightRange");
            g_LightDepth = GetLocation("g_LightDepth");
            g_CamPos = GetLocation("g_CameraPosition");
            g_CamMinZ = GetLocation("g_CameraMinZ");
            g_CamMaxZ = GetLocation("g_CameraMaxZ");
            SetUni("g_TextureColor", 0);
            SetUni("g_TextureNormal", 1);
            SetUni("g_TextureSpecular", 2);
            SetUni("g_TextureShadow", 3);
        }
    }
}