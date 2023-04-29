using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
