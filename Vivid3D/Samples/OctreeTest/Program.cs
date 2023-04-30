using OpenTK.Windowing.Desktop;

namespace OctreeTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            GameWindowSettings game_win = new GameWindowSettings();
            NativeWindowSettings native_settings = new NativeWindowSettings();

            game_win.RenderFrequency = 0;
            game_win.UpdateFrequency = 120;
            game_win.IsMultiThreaded = false;

            native_settings.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            native_settings.APIVersion = new Version(4, 6);
            native_settings.AutoLoadBindings = true;
            native_settings.Flags = OpenTK.Windowing.Common.ContextFlags.ForwardCompatible;
            native_settings.IsEventDriven = false;
            native_settings.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            native_settings.Size = new OpenTK.Mathematics.Vector2i(1024, 768);
            native_settings.Title = "Vivid - Application";
            Vivid.App.VividApp.InitialState = new StateTestPhysics();
            TestOctreeApp app = new TestOctreeApp(game_win, native_settings);

            app.Run();
        }
    }
}