using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid3D
{
    public enum EditorMode
    {
        Translate,Rotate,Scale
    }

    public enum SpaceMode
    {
        Local,Global,Screen,Smart
    }

    public class Editor
    {

        public static EditorMode EditMode = EditorMode.Translate;
        public static SpaceMode SpaceMode = SpaceMode.Local;
        public static Vivid.Scene.Scene CurrentScene = null;
        public static Vivid.Scene.Camera EditCamera = null;
        public static Vivid.Scene.Camera GameCamera = null;

        public static void NewScene()
        {

            CurrentScene = new Vivid.Scene.Scene();
            EditCamera = CurrentScene.MainCamera;
            GameCamera = EditCamera;

        }

    }
}
