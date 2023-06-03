using OpenTK.Windowing.Desktop;
using Vivid.App;

namespace UIDemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings game_win = new GameWindowSettings();
            NativeWindowSettings native_settings = new NativeWindowSettings();

            game_win.RenderFrequency = 0;
            game_win.UpdateFrequency = 60;
            game_win.IsMultiThreaded = false;


            native_settings.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            native_settings.APIVersion = new Version(4, 6);
            native_settings.AutoLoadBindings = true;
            native_settings.Flags = OpenTK.Windowing.Common.ContextFlags.ForwardCompatible;
            native_settings.IsEventDriven = false;
            native_settings.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            native_settings.Size = new OpenTK.Mathematics.Vector2i(1024, 768);
            native_settings.Title = "UI Demo - Application";
            native_settings.RedBits = 10;
            native_settings.GreenBits = 10;
            native_settings.BlueBits = 10;
            native_settings.DepthBits = 24;
            native_settings.AlphaBits = 24;
            native_settings.NumberOfSamples = 8;
            //VividApp.InitialState = new StateMainMenu();
            UIDemoApp game = new UIDemoApp(game_win, native_settings);

            game.Run();
        }
    }
}