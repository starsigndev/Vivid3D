using OpenTK.Mathematics;
using Vivid.RenderTarget;

namespace Vivid.Scene
{
    public enum LightType
    {
        Point, Spot, Directional
    }

    public class Light : Node
    {
        public Vector3 Diffuse
        {
            get;
            set;
        }

        public Vector3 Specular
        {
            get;
            set;
        }

        public float Range
        {
            get;
            set;
        }

        public RenderTargetCube RTC
        {
            get;
            set;
        }

        public LightType Type
        {
            get;
            set;
        }

        public float InnerCone
        {
            get;
            set;
        }

        public float OuterCone
        {
            get;
            set;
        }

        public bool CastShadows
        {
            get;
            set;
        }

        public bool VolumetricShafts
        {
            get;
            set;
        }

        public Light()
        {
            VolumetricShafts = false;
            Diffuse = Vector3.One;
            Specular = Vector3.One;
            Range = 100.0f;
            RTC = new RenderTargetCube(2048, 2048);
            Type = LightType.Point;
            InnerCone = 40;
            OuterCone = 40;
            CastShadows = true;

            //ShadowRT = GemBridge.gem_CreateRTC(512, 512);
        }
    }
}