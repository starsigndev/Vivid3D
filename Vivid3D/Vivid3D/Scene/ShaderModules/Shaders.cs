using Vivid.FX;

namespace Vivid.Scene.ShaderModules
{
    public class Shaders
    {
        public static MeshLinesFX MeshFX;

        public static void InitShaders()
        {
            MeshFX = new MeshLinesFX();
        }
    }
}