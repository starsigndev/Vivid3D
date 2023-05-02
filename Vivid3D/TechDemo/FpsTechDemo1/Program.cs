using OpenTK.Windowing.Desktop;
using Vivid.App;
using FpsTechDemo1.AppStates;

namespace FpsTechDemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Fps Tech Demo 1.");

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
            native_settings.Title = "Vivid - Application";
            native_settings.RedBits = 10;
            native_settings.GreenBits = 10;
            native_settings.BlueBits = 10;
            native_settings.DepthBits = 24;
            native_settings.AlphaBits = 24;
            native_settings.NumberOfSamples = 8;
            VividApp.InitialState = new StateMainMenu();
            FpsTechDemoApp game = new FpsTechDemoApp(game_win,native_settings);

            game.Run();

            //; ; native_settings


        }
    }
}