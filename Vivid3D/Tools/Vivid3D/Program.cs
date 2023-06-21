using OpenTK.Windowing.Desktop;

namespace Vivid3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entering Vivid3D.");

            if (args.Length > 0)
            {
                Console.WriteLine("Starting project:" + args[0]);

                if (args[0].Length > 0)
                {
                    Editor.ProjectPath = args[0];
                }
                else
                {
                    Editor.ProjectPath = "C:\\Projects\\Simple\\";
                }
            }
            else
            {
                Editor.ProjectPath = "C:\\Projects\\Simple\\";
            }

            int bb = 5;

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
            native_settings.Location = new OpenTK.Mathematics.Vector2i(0, 0);
            native_settings.Size = new OpenTK.Mathematics.Vector2i(1920, 1000);
            native_settings.Title = "Vivid3D - (c)Star Signal 2023";
            native_settings.RedBits = 10;
            native_settings.GreenBits = 10;
            native_settings.BlueBits = 10;
            native_settings.DepthBits = 24;
            native_settings.AlphaBits = 24;
            native_settings.NumberOfSamples = 8;
            //VividApp.InitialState = new StateMainMenu();
            Vivid3DApp game = new Vivid3DApp(game_win, native_settings);

            game.Run();
        }
    }
}