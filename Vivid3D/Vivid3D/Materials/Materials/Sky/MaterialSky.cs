using Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Shaders;
using OpenTK.Mathematics;
using System.Data;

namespace Vivid.Materials.Materials.Sky
{
    public enum SkyTime
    {
        Dawn,Dusk,Daytime,Nighttime,MoonLight
    }
    public class MaterialSky : MaterialBase
    {

        public SkyTime TimeOfDay
        {
            get
            {
                return _Cur;
            }
            set
            {
                _Cur = value;
                var skyFX = Shader as SkyFX;
                Vector3[] gradientColors;
                float[] gradientPositions;
                switch (_Cur)
                {
                    case SkyTime.Nighttime:

                        ToNighttime(out gradientColors, out gradientPositions);
                        skyFX.gradientColors = gradientColors;
                        skyFX.gradientPositions = gradientPositions;
                        break;
                    case SkyTime.Dawn:
                   
                        ToDawn(out gradientColors, out gradientPositions);
                        skyFX.gradientColors = gradientColors;
                        skyFX.gradientPositions = gradientPositions;

                        break;
                }
                
            }
        }
        private static void ToNighttime(out Vector3[] gradientColors,out float[] gradientPositions)
        {
             gradientColors = new Vector3[]
{
    new Vector3(0.0f, 0.0f, 0.2f),   // Deep blue
    new Vector3(0.0f, 0.0f, 0.5f),   // Midnight blue
    new Vector3(0.2f, 0.2f, 0.6f),   // Dark indigo
    new Vector3(0.4f, 0.4f, 0.7f),   // Lighter indigo
    new Vector3(0.5f, 0.5f, 0.8f),   // Lightest indigo
    new Vector3(0.6f, 0.6f, 0.9f)    // Very light blue (horizon color)
};

            gradientPositions = new float[]
            {
    0.0f,   // Position for Deep blue
    0.2f,   // Position for Midnight blue
    0.4f,   // Position for Dark indigo
    0.6f,   // Position for Lighter indigo
    0.8f,   // Position for Lightest indigo
    1.0f    // Position for Very light blue (horizon position)
            };
        }
        private static void ToDawn(out Vector3[] gradientColors, out float[] gradientPositions)
        {
            gradientColors = new Vector3[]
{
    new Vector3(0.0f, 0.0f, 0.3f),   // Deep blue
    new Vector3(0.6f, 0.0f, 0.4f),   // Purple
    new Vector3(0.9f, 0.3f, 0.2f),   // Dark orange/red
    new Vector3(1.0f, 0.6f, 0.4f),   // Light orange
    new Vector3(1.0f, 0.8f, 0.6f),   // Lighter orange/pinkish
    new Vector3(1.0f, 1.0f, 0.9f)    // Off-white (horizon color)
};
            gradientPositions = new float[]
            {
    0.0f,   // Position for deep blue
    0.1f,   // Position for purple
    0.3f,   // Position for dark orange/red
    0.5f,   // Position for light orange
    0.7f,   // Position for lighter orange/pinkish
    1.0f    // Position for off-white (horizon position)
            };
        }

        SkyTime _Cur = SkyTime.Dawn;
        public MaterialSky()
        {
            Shader = new SkyFX();
            int a = 5;
            TimeOfDay = SkyTime.Dawn;

        }


    }

    public class SkyFX : GeminiStandardFX
    {
        public Vector3 TopColor;
        public Vector3 BotColor;
        public Vector3 MidColor;
        public Vector3 SunPosition = new Vector3(0, 0, 0);
        public float TopY = 10.0f;
        public Vector3[] gradientColors = new Vector3[6];
        public float[] gradientPositions = new float[6];

        public SkyFX() : base("gemini/shaders/mat_skyVS.glsl", "gemini/shaders/mat_sky2FS.glsl")
        {

            TopColor = new Vector3(0.3f, 0.4f, 0.6f);
            MidColor = new Vector3(0.8f, 0.6f, 0.4f);
            BotColor = new Vector3(0.1f, 0.1f, 0.2f);


        }

        public override void SetUniforms()
        {
            base.SetUniforms();

           

            SetUni("g_TopColor", TopColor);
            SetUni("g_MidColor", MidColor);
            SetUni("g_BotColor", BotColor);
            SetUni("g_TopY", TopY);
            SetUni("g_SunDirection", SunPosition.Normalized());
            SetUni("g_SunSize", 0.000001f);
            SetUni("g_SunIntensity", 0.01f);
            SetUni("g_AtmosphereDensity", 0.2f);
            SetUni("g_Colors", gradientColors);
            SetUni("g_ColorPositions", gradientPositions);
            SetUni("g_SunGlow", 0.35f);
            SetUni("g_Rayleigh", 1.0f);
            SetUni("g_Mie", 0.1f);

        }

    }

}
