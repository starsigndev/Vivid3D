using OpenTK.Mathematics;

namespace Vivid
{
    public enum KeyID
    {
        KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, KeyG, KeyH, KeyI, KeyJ, KeyK, KeyL, KeyM, KeyN, KeyO, KeyP, KeyQ, KeyR, KeyS, KeyT, KeyU, KeyV, KeyW, KeyX, KeyY, KeyZ, N0, N1, N2, N3, N4, N5, N6, N7, N8, N9, Space, Return, Backspace, Delete, None, Shift
    };

    public enum MouseID
    {
        Left, Right, Middle, Back=3, Forwards=4
    }

    public static class GameInput
    {
        public static Vector2 MousePosition;
        public static Vector2 MouseDelta;
        public static Vector2 WheelDelta;
        public static bool[] MouseButton = new bool[32];
        public static bool[] mKeyDown = new bool[512];
        public static bool mShiftDown = false;
        public static OpenTK.Windowing.GraphicsLibraryFramework.Keys mCurrentKey;
        public static bool mKeyIsDown = false;

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