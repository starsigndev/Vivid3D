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

namespace Vivid3D
{
    public class Vivid3DApp : Vivid.App.VividApp
    {
        public static UI MainUI = null;
        public FMainMenu MainMenu = null;
        public FMainWindow MainWindow = null;
        public FToolBar MainToolBar;

        public Vivid3DApp(GameWindowSettings game_win, NativeWindowSettings native_win) : base(game_win, native_win)
        {

        }

        public override void Init()
        {
            //base.Init();
            MainUI = new UI();
            MainMenu = new FMainMenu();

            MainUI.Menu = MainMenu;

            MainToolBar = new FToolBar();
    

            MainWindow = new FMainWindow();

            MainWindow.Set(2, 75, VividApp.FrameWidth-7, VividApp.FrameHeight - 83, "Vivid3D");
            MainWindow.Static = true;
            MainUI.ToolBar = MainToolBar;
            MainToolBar.Position = new Vivid.Maths.Position(0, 30);

            //Editor.NewScene();
      


            


        }

        public override void Update()
        {
            //base.Update();
            MainUI.Update();
            Editor.Update();
        }

        public override void Render()
        {
            //base.Render();
            MainUI.Render();
        }

    }
}
