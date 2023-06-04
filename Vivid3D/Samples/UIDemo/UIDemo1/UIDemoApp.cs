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

namespace UIDemo1
{
    public class UIDemoApp : Vivid.App.VividApp
    {

        public UIDemoApp(GameWindowSettings game_win,NativeWindowSettings native_win) : base(game_win, native_win)
        {

        }
        UI ui;

        public override void Init()
        {
            ui = new UI();
            IFrame frame1 = new IFrame().Set(new Vivid.Maths.Position(0, 0), new Vivid.Maths.Size(Vivid.App.VividApp.FrameWidth,Vivid.App.VividApp.FrameHeight),"") as IFrame;
            ui.AddForm(frame1);
            IButton but1 = new IButton().Set(new Vivid.Maths.Position(32, 32), new Vivid.Maths.Size(128, 32), "Test 1") as IButton;
            //frame1.AddForm(but1);

            IWindow win1 = new IWindow("Test").Set(64, 256, 300, 500, "Title") as IWindow;

            ui.AddForm(win1);

            IButton b1, b2;

            b1 = new IButton().Set(20, 80, 120, 30, "Test 1") as IButton;
            b2 = new IButton().Set(20, 550, 120, 40, "Window 2") as IButton;

            win1.Content.AddForms(b1, b2);

            var file = ui.Menu.AddItem("File");
            var edit = ui.Menu.AddItem("Edit");
            ui.Menu.AddItem("Project");
            file.AddItem("Load Project");
            file.AddItem("Save Project");
            file.AddItem("Exit");
            var cut = edit.AddItem("Cut");
            var paste = edit.AddItem("Paste");

            paste.Click = (item) =>
            {
                Environment.Exit(0);
            };

            var t1 = new Texture2D("ui/test1.png");

            cut.Icon = t1;

            edit.AddItem("Other test item");

            cut.AddItem("Cut this");
            var co = cut.AddItem("Cut this other");
            cut.AddItem("Cut other");
            co.AddItem("Check 1");
            co.AddItem("Other check 2");
            //base.Init();

            IWindow win2 = new IWindow("Render");
            win2.Set(20, 20, 350, 450,"Render");
            IRenderTarget view = new IRenderTarget();
            view.Set(0, 0,win2.Content.Size.w,win2.Content.Size.h);
            win2.Content.AddForm(view);

            view.ActionRender = (w, h) =>
            {
                GL.ClearColor(0, 0, 0, 1);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            };
            win2.Content.Scissor = false;

            ui.AddForm(win2);

            ITextBox tb1 = new ITextBox().Set(20, 20, 150, 30, "") as ITextBox;
            frame1.AddForm(tb1);
            
        }

        public override void Update()
        {
            ui.Update();
        }

        public override void Render()
        {
            //base.Render();
            ui.Render();
        }

    }
}
