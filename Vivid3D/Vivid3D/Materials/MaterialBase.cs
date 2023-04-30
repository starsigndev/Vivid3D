using Vivid.Texture;

namespace Vivid.Materials
{
    public class MaterialBase
    {
        public static Texture2D bcol = null, bnorm, bspec;

        public Texture2D ColorMap
        {
            get;
            set;
        }

        public Texture2D NormalMap
        {
            get;
            set;
        }

        public Texture2D SpecularMap
        {
            get;
            set;
        }

        public Texture2D DisplaceMap
        {
            get;
            set;
        }

        public GeminiStandardFX Shader
        {
            get;
            set;
        }

        //public GeminiStandardFX

        public MaterialBase()
        {
            if (bcol == null)
            {
                bcol = new Texture2D("gemini/white.png");
                bnorm = new Texture2D("gemini/normal.png");
                bspec = new Texture2D("gemini/black.png");
            }
            ColorMap = bcol;
            NormalMap = bnorm;
            SpecularMap = ColorMap;
            DisplaceMap = bspec;
        }
    }
}