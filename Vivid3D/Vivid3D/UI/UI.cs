using Vivid.App;
using Vivid.Draw;
using Vivid.Font;
using Vivid.Maths;
using Vivid.Texture;
using Vivid.UI.Forms;
using OpenTK.Graphics.OpenGL;
namespace Vivid.UI
{
    public class UI
    {
        public static Content.Content UIBase
        {
            get;
            set;
        }

        public static Texture2D UICursor
        {
            get;
            set;
        }

        public IForm Root
        {
            get;
            set;
        }

        public IMenu Menu
        {
            get;
            set;
        }

        public static SmartDraw Draw
        {
            get;
            set;
        }

        public Position MousePosition
        {
            get;
            set;
        }

        public Delta MouseDelta
        {
            get;
            set;
        }

        public static UITheme Theme
        {
            get;
            set;
        }

        public static kFont SystemFont
        {
            get;
            set;
        }

        public IForm Over
        {
            get;
            set;
        }

        public IForm[] Pressed
        {
            get;
            set;
        }

        public IForm Active
        {
            get;
            set;
        }

        public IForm ContextForm
        {
            get;
            set;
        }

        public static void DrawString(string text, int x, int y, Vivid.Maths.Color col)
        {
            SystemFont.DrawString(text, x, y, col.r, col.g, col.b, col.a, Draw);
        }

        public UI()
        {
            if (UIBase == null)
            {
                //UIBase = new Content.Content(VividApp.ContentPath + "uibase");
                //    var cursor = Content.Content.GlobalFindItem("cursor1");
                //  UICursor = new Texture2D(cursor.GetStream(), cursor.Width, cursor.Height);
                UICursor = new Texture2D("edit/cursor.png");
                Draw = new SmartDraw();

                if (Theme == null)
                {
                    Theme = new UITheme("light");
                }

                SystemFont = new kFont("gemini/font/neo2.pf");
                SystemFont.Scale = 0.65f;  
            }

            Over = null;

            Pressed = new IForm[32];

            for (int i = 0; i < 32; i++)
            {
                Pressed[i] = null;
            }

            Active = null;
            for(int i = 0; i < 255; i++)
            {
                LastKey[i] = false;
            }

            GetMouse();
            Root = new IForm();
            Root.Set(0, 30, Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight-30);
            Menu = new IMenu().Set(0, 0, VividApp.FrameWidth, 30, "") as IMenu;
        }

        public void AddForm(IForm form)
        {
            Root.AddForm(form);
            form.Root = Root;
        }

        public void AddForms(params IForm[] forms)
        {
            foreach(var form in forms)
            {
                AddForm(form);
            }
        }

        public void GetMouse()
        {
            MousePosition = new Position((int)GameInput.MousePosition.X,(int)GameInput.MousePosition.Y);
            MouseDelta = new Delta(GameInput.MouseDelta.X, GameInput.MouseDelta.Y);
        }

        public void Update()
        {
            GetMouse();
            Root.Update();

            List<IForm> form_list = new List<IForm>();

            AddToList(form_list, Root);
            if (ContextForm != null)
            {
                AddToList(form_list, ContextForm);
            }
            AddToList(form_list, Menu);

            form_list.Reverse();

            UpdateList(form_list);
            UpdateKeys();
        }
        bool firstKey = true;
        int nextKey = 0;
        int curKey = -1;
        private bool[] LastKey = new bool[512];
        private bool LastShift = false;
        public void UpdateKeys()
        {
            if (!firstKey)
            {
                if (GameInput.mKeyDown[curKey] == false)
                {
                    firstKey = true;
                    
                }
            }

            if(GameInput.mShiftDown && !LastShift)
            {
                if (Active != null)
                {
                    Active.OnKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift);
                }
                LastShift = true;
            }
            if(!GameInput.mShiftDown && LastShift)
            {
                if (Active != null)
                {
                    Active.OnKeyUp(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift);
                }
                LastShift = false;
            }

            if (GameInput.mKeyIsDown)
            {
                
                if (curKey == -1)
                {
                    curKey = (int)GameInput.mCurrentKey;
                    nextKey = Environment.TickCount + 350;
                    if (Active != null)
                    {
                        Active.OnKey(GameInput.mCurrentKey);
                    }
                }
                else
                {

                    if (curKey == (int)GameInput.mCurrentKey)
                    {
                        int tick = Environment.TickCount;
                        if (tick > nextKey)
                        {
                            nextKey = tick + 60;
                            if (Active != null)
                            {
                                Active.OnKey(GameInput.mCurrentKey);
                            }
                        }
                    }
                    else
                    {
                        curKey = -1;
                    }
                }
            }
            else
            {
                curKey = -1;
            }

            return;
            for(int i = 0; i < 512; i++)
            {
                if (i == (int)OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift || i==(int)OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightShift)
                {
                    continue;
                }
                bool key = GameInput.mKeyDown[i];
                if(key && LastKey[i]==false)
                {
                   // Console.WriteLine("::::" + (OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                    if (Active != null)
                    {
                        Active.OnKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                    }
                }
                if(!key && LastKey[i] == true)
                {
                    if (Active != null)
                    {
                        Active.OnKeyUp((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                    }
                }
                if (GameInput.mKeyDown[i])
                {
                    if (Active != null)
                    {
                        if (firstKey)
                        {
                            Active.OnKey((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                            nextKey = Environment.TickCount + 750;
                            firstKey = false;
                                curKey = i;
                        }
                        else
                        {
                            if (i != curKey)
                            {
                                firstKey = true;

                            }
                            else
                            {
                                if (Environment.TickCount > nextKey)
                                {
                                    Active.OnKey((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                                    nextKey = Environment.TickCount + 280;
                                }
                            }
                        }
                    }
                }

                LastKey[i] = key;
            }
        }

        public void UpdateList(List<IForm> list)
        {
            bool over_leave = false;
            if (Pressed[0] == null)
            {
                foreach (var form in list)
                {
                    if (form.InBounds(MousePosition))
                    {
                        if (Over != null)
                        {
                            if (Over != form)
                            {
                                Over.OnLeave();
                                over_leave = true;
                            }
                        }
                        if (Over != form)
                        {
                            Over = form;
                            Over.OnEnter();
                        }
                        break;
                    }
                }
            }

            if (Over != null && Pressed[0]==null)
            {
                if (over_leave == false)
                {
                    if (Over.InBounds(MousePosition) == false)
                    {
                        Over.OnLeave();
                        Over = null;
                    }
                }
            }
            if (Over != null)
            {
                Over.OnMouseMove(MousePosition, MouseDelta);
                if (Pressed[0] == null)
                {
                 
                    if (GameInput.MouseButtonDown(MouseID.Left))
                    {
                        if (Over != ContextForm)
                        {
                            if (ContextForm != null)
                            {
                                ContextForm = null;
                            }
                        }
                        Pressed[0] = Over;
                        Over.Active = true;
                        if (Active != null && Active != Over)
                        {
                            Active.OnDeactivate();
                            Active.Active = false;
                        }
                        Active = Over;
                        Active.OnActivate();
                        Pressed[0].OnMouseDown(MouseID.Left);
                    }
                    if (GameInput.MouseButtonDown(MouseID.Right))
                    {
                        //Environment.Exit(1);
                        if (Over.ContextForm != null)
                        {
                            bool use = true;
                            if (ContextForm != null)
                            {
                                if (ContextForm == Over.ContextForm)
                                {
                                    use = false;
                                }

                            }
                            if (use)
                            {
                                ContextForm = Over.ContextForm;
                                ContextForm.Position = new Position(MousePosition.x, MousePosition.y);
                            }
                        }

                    }
                }
                else
                {
                    if (!GameInput.MouseButtonDown(MouseID.Left))
                    {
                        Pressed[0].OnMouseUp(MouseID.Left);
                        if (Over != Pressed[0])
                        {
                            Pressed[0].OnLeave();
                        }
                        Pressed[0] = null;
                    }
                }
            }
        }

        private void AddToList(List<IForm> list, IForm form)
        {
            list.Add(form);

            foreach (var f in form.Forms)
            {
                AddToList(list, f);
            }
        }

        public void Render()
        {

            
           // Draw.Begin();
            Root.Render();
            if (ContextForm != null)
            {
                ContextForm.Render();
            }
            Menu.Render();

            // Draw.End();

            GL.Clear(ClearBufferMask.DepthBufferBit);// //..Disable(EnableCap.DepthTest);

            Draw.Blend = BlendMode.Alpha;
            Draw.Begin();
            Draw.Draw(UICursor, new Rect(MousePosition.x, MousePosition.y, 32, 32),new Maths.Color(1, 1, 1, 0.75f));
            Draw.End();
          

            GL.Enable(EnableCap.DepthTest);

        }
    }
}