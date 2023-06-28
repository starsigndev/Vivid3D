using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Texture;
using Vivid.UI;
using Vivid.UI.Forms;
using OpenTK.Graphics.OpenGL;
using Vivid.App;
using Vivid3D.Forms;
using Vivid3D.Windows;
using Vivid.AI;
using Vivid.Draw;

namespace Vivid3D
{
    public class Vivid3DApp : Vivid.App.VividApp
    {
        public static UI MainUI = null;
        public FMainMenu MainMenu = null;
        public FMainWindow MainWindow = null;
        public FToolBar MainToolBar;
        public AIMind Mind;
        bool Splash = true;
        public SmartDraw Draw;
        Texture2D splash_bg;
        Texture2D sat1;
        Texture2D logo1;
        public Vivid3DApp(GameWindowSettings game_win, NativeWindowSettings native_win) : base(game_win, native_win)
        {

        }

        int start;

        public override void Init()
        {
            //base.Init();

            if (Splash)
            {
                Draw = new SmartDraw();
                splash_bg = new Texture2D("splash/bg1.png");
                sat1 = new Texture2D("splash/sat1.png");
                logo1 = new Texture2D("splash/logo1.png");
                start = Environment.TickCount + 2000;
            }
            else
            {

                MainUI = new UI();
                MainMenu = new FMainMenu();

                MainUI.Menu = MainMenu;

                MainToolBar = new FToolBar();


                MainWindow = new FMainWindow();

                MainWindow.Set(2, 75, VividApp.FrameWidth - 7, VividApp.FrameHeight - 83, "Vivid3D");
                MainWindow.Static = true;
                MainUI.ToolBar = MainToolBar;
                MainToolBar.Position = new Vivid.Maths.Position(0, 30);

                //Editor.NewScene();

                SetupPostProcessing();

            }
    





        }

        public override void Update()
        {
            //base.Update();

            if (!Splash)
            {
                MainUI.Update();
                Editor.Update();

            }
            else
            {
                if (Environment.TickCount > start)
                {
                    Splash = false;
                    Size = new OpenTK.Mathematics.Vector2i(1920, 1000);
                    Location = new OpenTK.Mathematics.Vector2i(0, 0);

                    Init();

                }
            }
        }

        public override void Render()
        {
            //base.Render();
            if (!Splash)
            {
                MainUI.Render();
            }
            else { 
            Draw.Blend = BlendMode.Alpha;
            Draw.Begin();
            Draw.Draw(splash_bg, new Vivid.Maths.Rect(0, 0, FrameWidth, FrameHeight), new Vivid.Maths.Color(1, 1, 1, 1));
            Draw.Draw(logo1, new Vivid.Maths.Rect(0, 64, FrameWidth, FrameHeight), new Vivid.Maths.Color(1, 1, 1, 1));
            Draw.Draw(sat1, new Vivid.Maths.Rect(32, 32, 128, 128), new Vivid.Maths.Color(1, 1, 1, 1));
            Draw.End();
        }
        }


        public static void SetupPostProcessing()
        {

            WPostProcessing.PostProcesses.Clear();
            WPostProcessing.PostProcesses.Add(new Vivid.PostProcesses.PPBloom(Editor.CurrentScene));
            WPostProcessing.PostProcesses.Add(new Vivid.PostProcesses.PPOutline(Editor.CurrentScene));
            WPostProcessing.PostProcesses.Add(new Vivid.PostProcesses.PPLightShafts(Editor.CurrentScene));


        }

    }
}
