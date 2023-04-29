
using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Vivid
{
    public enum KeyID
    {

        KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, KeyG, KeyH, KeyI, KeyJ, KeyK, KeyL, KeyM, KeyN, KeyO, KeyP, KeyQ, KeyR, KeyS, KeyT, KeyU, KeyV, KeyW, KeyX, KeyY, KeyZ, N0, N1, N2, N3, N4, N5, N6, N7, N8, N9, Space, Return, Backspace, Delete, None, Shift

    };

    public enum MouseID
    {
        Left,Right,Middle,Back,Forwards
    }

    public static class GameInput
    {
        public static Vector2 MousePosition;
        public static Vector2 MouseDelta;
        public static bool[] MouseButton = new bool[32];
        public static bool[] mKeyDown = new bool[512];



        
        public static bool MouseButtonDown(MouseID id)
        {
            return MouseButton[(int)id];
        }
        public static bool KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys key)
        {
            return mKeyDown[(int)key];
           
         //   return GemBridge.gem_GetKey((int)key);

        }
    }
}
