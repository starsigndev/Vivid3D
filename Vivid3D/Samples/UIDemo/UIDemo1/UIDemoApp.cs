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

namespace UIDemo1
{
    public enum TestEnum
    {
        Test1,OpenGL,DirectX,Other,Cool
    }
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

            //ui.AddForm(win1);
            IHorizontalSplitter split1 = new IHorizontalSplitter();
            split1.Set(0, 0, frame1.Size.w, frame1.Size.h);
            //frame1.AddForm(split1);

            IWindow w1 = new IWindow("Win1").Set(0, 0, 200, 200,"Window 1") as IWindow;
            IWindow w2 = new IWindow("Win2").Set(0, 0, 200, 200,"RenderWindow") as IWindow;

            split1.SetTop(w1);
            split1.SetBottom(w2);


           // return;

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

            IToolBar toolbar = new IToolBar().Set(0, 0, VividApp.FrameWidth, 32, "") as IToolBar;

            var tb_but1 = new IButton().Set(0, 0, 65, 30, "Test") as IButton;
            var tb_but2 = new IButton().Set(0, 0, 90, 30, "Transform") as IButton;

            toolbar.AddTool(tb_but1);
            toolbar.AddTool(tb_but2);

            frame1.AddForm(toolbar);

            ITextBox tb1 = new ITextBox().Set(20, 20, 250, 30, "") as ITextBox;
          //  frame1.AddForm(tb1);
            //tb1.Numeric = true;
            //tb1.Password = true;

            ITextArea ta1 = new ITextArea().Set(30, 80, 550, 500, "") as ITextArea;
//            frame1.AddForm(ta1);

            INumericBox num1 = new INumericBox();
            num1.Set(30, 80, 130, 28, "");
            frame1.AddForm(num1);

            IVerticalMenu testMenu = new IVerticalMenu();

            var copy = testMenu.AddItem("Copy this");
            testMenu.AddItem("Cut this");
            testMenu.AddItem("Paste here.");
            copy.Click = (item) =>
            {
                Environment.Exit(1);
            };

            var tv = new ITreeView().Set(80, 200, 250, 450, "") as ITreeView;

            for(int i = 0; i < 80; i++)
            {
                var item = tv.Root.AddItem("Item " + i.ToString());
                item.Click = (item) =>
                {
                    Console.WriteLine("ItemClicked:" + item.Text);
                };
                for(int j = 0; j < 4; j++)
                {
                    item.AddItem("Sub item 234324324324 " + j.ToString());
                }
            }

            tv.Click = (item) =>
            {

                //Console.WriteLine("Click:" + item.Text);
            };

            frame1.AddForm(tv);

            IEnumSelector sel1 = new IEnumSelector(typeof(TestEnum));
            sel1.Set(350, 100, 120, 30, "");

            frame1.AddForm(sel1);

            frame1.ContextForm = testMenu;

            ta1.SetText("This is a test for the text area editor control.This is to see if it works\r\nThis is the continuing test.\r\nAnd so is this, if it works, cool, if it does not, then all health shall feel thy wrath.\r\nor not. you know.\r\n     public override void OnKey(Keys key)\r\n        {\r\n            //base.OnKey(key);\r\n            switch (key)\r\n            {\r\n                case Keys.Left:\r\n                    EditX--;\r\n                    if (EditX < 0)\r\n                    {\r\n                        EditX = 0;\r\n                    }\r\n                    if (EditX < TextStart)\r\n                    {\r\n                        TextStart--;\r\n                    }\r\n                  \r\n                    return;\r\n                    break;\r\n                case Keys.Right:\r\n                    EditX++;\r\n                    if (EditX > Text.Length)\r\n                    {\r\n                        EditX = Text.Length;\r\n                    }\r\n                    return;\r\n                    break;\r\n                case Keys.Backspace:\r\n                    Backspace();\r\n                    return;\r\n                    break;\r\n                case Keys.Delete:\r\n                    Delete();\r\n                    return;\r\n                    break;\r\n               \r\n            }\r\n            string chr = \"\";\r\n            chr = KeyToChr(key);\r\n            InsertChr(chr);\r\n            //Text = Text + chr;\r\n//            EditX++;\r\n\r\n\r\n        }");

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
