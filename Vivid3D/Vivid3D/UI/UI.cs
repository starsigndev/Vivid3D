using Vivid.App;
using Vivid.Draw;
using Vivid.Font;
using Vivid.Maths;
using Vivid.Texture;
using Vivid.UI.Forms;
using OpenTK.Graphics.OpenGL;
using Vivid.PostProcesses;
using BepuPhysics;

namespace Vivid.UI
{
    public class UI
    {
        public static UI This = null;
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

        public List<IWindow> Windows
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

        public IForm ToolBar
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
            This = this;
            if (UIBase == null)
            {
                //UIBase = new Content.Content(VividApp.ContentPath + "uibase");
                //    var cursor = Content.Content.GlobalFindItem("cursor1");
                //  UICursor = new Texture2D(cursor.GetStream(), cursor.Width, cursor.Height);
                UICursor = new Texture2D("edit/cursor.png");
                Draw = new SmartDraw();
                
                if (Theme == null)
                {
                    Theme = new UITheme("vividdark");
                }

                SystemFont = new kFont("gemini/font/systemfont2.pf");
                SystemFont.Scale = 0.36f;  
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
            Windows = new List<IWindow>();
        }


        public void ResizeUI(int w,int h)
        {
            Root.Resized(w, h);
           
        }

        public void AddForm(IForm form)
        {
            Root.AddForm(form);
            form.Root = Root;
        }

        public void AddWindow(IWindow window)
        {
            Windows.Add(window);
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


            if (Pressed[0] == null || Pressed[0] == MovingZones)
            {
                UpdateWindows();
            }

            if (Pressed[0] == MovingZones)
            {
                if (GameInput.MouseButtonDown(MouseID.Left) == false)
                {
                    Pressed[0] = null;
                }
                else
                {
                    return;
                }
            }
            Root.Update();

            List<IForm> form_list = new List<IForm>();

            AddToList(form_list, Root);


            if (draggingSpaces == false)
            {
                foreach (var win in Windows)
                {
                    Console.WriteLine("Updating windows.");
                    AddToList(form_list, win);
                    win.Update();
                }
            }
            else
            {
                Pressed[0] = null;
                foreach(IWindow win in Windows)
                {
                    win.OnMouseUp(MouseID.Left);
                    win.Drag = false;
                }
            }

            if (ContextForm != null)
            {
                AddToList(form_list, ContextForm);
            }

            AddToList(form_list, ToolBar);

            ToolBar.Update();

            AddToList(form_list, Menu);

            form_list.Reverse();

            UpdateList(form_list);
            UpdateKeys();

           

        }
        IWindow dragWin = null;
        bool draggingSpaces;
        private List<DockingSpace> dragSpaces = new List<DockingSpace>();
        List<DockingSpace> top = new List<DockingSpace>();
        List<DockingSpace> bot = new List<DockingSpace>();
        List<DockingSpace> left = new List<DockingSpace>();
        List<DockingSpace> right = new List<DockingSpace>();
        private IForm MovingZones = new IForm();

        public void UpdateWindows()
        {

            if (DockWindow != null) return;

            int mx, my;
            mx = MousePosition.x;
            my = MousePosition.y;

          

            foreach (var win in Windows)
            {

                int cx, cy;
                cx = mx - win.Position.x;
                cy = my - win.Position.y;

                if (draggingSpaces == false)
                {

                    dragSpaces.Clear();
                    if (win.WindowDock)
                    {

                        void CollectTop(List<DockingSpace> list, List<DockingSpace> spaces)
                        {
                            foreach (var space in spaces)
                            {

                                if (space.Y < 10) continue;

                                if (cx > 0 && cx < win.Size.w)
                                {

                                    if (cy > space.Y-5 && cy < space.Y + 5)
                                    {
                                        //Console.WriteLine("Moving Top:");
                                        list.Add(space);
                                        //Environment.Exit(0);
                                    }

                                }

                            }
                        }

                        void CollectBot(List<DockingSpace> list,List<DockingSpace> spaces)
                        {
                            foreach(var sp in spaces)
                            {

                                if (sp.Y+sp.Height >= win.Size.h-30) continue;

                                if (cx > 0 && cx < win.Size.w)
                                {

                                    if (cy > sp.Y + sp.Height-5 && cy < (sp.Y+sp.Height+5))
                                    {
                                        //Console.WriteLine("Moving Top:");
                                        list.Add(sp);

                                        //Environment.Exit(0);
                                    }

                                }


                            }
                        }

                        void CollectLeft(List<DockingSpace> list, List<DockingSpace> spaces)
                        {
                            foreach (var sp in spaces)
                            {

                                if (sp.X <= 1) continue;

                                if (cy > 0 && cy < win.Size.h)
                                {

                                    if (cx > sp.X - 5 && cx < (sp.X + 5) )
                                    {
                                        //Console.WriteLine("Moving Top:");
                                        list.Add(sp);

                                        //Environment.Exit(0);
                                    }

                                }


                            }
                        }

                        void CollectRight(List<DockingSpace> list, List<DockingSpace> spaces)
                        {
                            foreach (var sp in spaces)
                            {

                                if (sp.X+sp.Width>=win.Size.w-15) continue;

                                if (cy > 0 && cy < win.Size.h)
                                {

                                    if (cx > sp.X+sp.Width - 5 && cx < (sp.X + sp.Width+5))
                                    {
                                        //Console.WriteLine("Moving Top:");
                                        list.Add(sp);

                                        //Environment.Exit(0);
                                    }

                                }


                            }
                        }



                        top.Clear();
                        bot.Clear();
                        left.Clear();
                        right.Clear();

                        
                        CollectTop(top, win.dockingSpaces);
                        CollectBot(bot, win.dockingSpaces);
                        CollectLeft(left, win.dockingSpaces);
                        CollectRight(right, win.dockingSpaces);

                 

                        if (top.Count > 0 || bot.Count>0 || left.Count>0 || right.Count>0)

                        {

                            
                            //Console.WriteLine("Moving Top.");

                            if (!draggingSpaces)
                            {

                                if (GameInput.MouseButtonDown(MouseID.Left))
                                {
                                    Pressed[0] = MovingZones;
                                    draggingSpaces = true;
                                    dragWin = win;

                                    foreach (var space in top)
                                    {
                                        dragSpaces.Add(space);
                                    }

                                }

                            }

                        }

                    }

                }
            }

            if (draggingSpaces)
            {

                int cx, cy;
                cx = mx - dragWin.Position.x;
                cy = my - dragWin.Position.y;

                if(cx<35 || cy<15 || cx>= dragWin.Size.w-15 || cy >= dragWin.Size.h - 25)
                {
                    draggingSpaces = false;
                    return;
                }

                int dy = (int)MouseDelta.y;
                int dx = (int)MouseDelta.x;

                
                foreach(var sp in top)
                {

                    sp.Y = sp.Y + dy;
                    sp.Height = sp.Height - dy;
                    sp.UpdateProportions(dragWin);
                    sp.UpdateDocked();

                }

                foreach(var sp in bot)
                {

                    sp.Height = sp.Height + dy;
                    sp.UpdateProportions(dragWin);
                    sp.UpdateDocked();

                }

                foreach(var sp in left)
                {
                    sp.X = sp.X + dx;
                    sp.Width = sp.Width - dx;
                    sp.UpdateProportions(dragWin);
                    sp.UpdateDocked();
                    //Environment.Exit(0);


                }

                foreach(var sp in right)
                {

                    sp.Width = sp.Width + dx;
                    sp.UpdateProportions(dragWin);
                    sp.UpdateDocked();

                }

                List<DockingSpace> all = new List<DockingSpace>();
                void add(List<DockingSpace> all,List<DockingSpace> sp)
                {
                    foreach(var s in sp)
                    {
                        all.Add(s);
                    }
                }

              

                //foreach(var sp in left)
                //{

                

                if (GameInput.MouseButtonDown(MouseID.Left) == false)
                {
                    draggingSpaces = false;
                    dragWin = null;
                    //Environment.Exit(0);
                }
            }

        }

        bool firstKey = true;
        int nextKey = 0;
        int curKey = -1;
        private bool[] LastKey = new bool[512];
        private bool LastShift = false;
        bool directKeyStart = true;
        public void UpdateKeys()
        {
            if (!firstKey)
            {
                if (GameInput.mKeyDown[curKey] == false)
                {
                    firstKey = true;
                    
                }
            }

            if (Active != null)
            {
                if (Active.DirectKeys)
                {
                    if (directKeyStart)
                    {
                        for(int i = 0; i < 512; i++)
                        {
                            LastKey[i] = false;
                            GameInput.mKeyDown[i] = false;
                        }
                        directKeyStart = false;
                    }
                    for(int i = 0; i < 512; i++)
                    {
                        if (GameInput.KeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i) && LastKey[i]==false)
                        {
                            Active.OnKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                        }
                        if(!GameInput.KeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i) && LastKey[i])
                        {
                            Active.OnKeyUp((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                        }

                        LastKey[i] = GameInput.KeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)i);
                    }
                    //int b = 5;
                    return;
                }
                {
                    directKeyStart = true;
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
        public IForm GetBeneath(List<IForm> list,IForm ignore)
        {
            foreach (var form in list)
            {
                if (form == ignore) continue;
                if (form.InBounds(MousePosition))
                {
                    return form;
                    break;
                }
            }
            return null;
        }

        IWindow CurrentDock;
        IWindow DockWindow;

        private int[] LastClick = new int[32]; 

        public void UpdateList(List<IForm> list)
        {

            if (CurrentDock != null)
            {
                if (Pressed[0] == null)
                {

                    var target = CurrentDock.DetermineDock(new Forms.Point(MousePosition.x, MousePosition.y), 128);
                    if (target != null)
                    {

                        if(target.Position != DockPosition.None)
                        {
                            CurrentDock.DockWindow(DockWindow, new Forms.Point(MousePosition.x, MousePosition.y), 128);
                        }

                    };

                        CurrentDock = null;
                    DockWindow = null;
                }
            }

            bool over_leave = false;
            if (Pressed[0] == null && Pressed[1]==null)
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

            if (Over != null && Pressed[0]==null && Pressed[1] ==null)
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
                if (Pressed[0] == Over)
                {

                    if (Over is IWindow)
                    {
                        var ww = Over as IWindow;

                        if (ww.Docked == false)
                        {
                            var be = GetBeneath(list, Over);

                            if (be != null)
                            {

                                if (be.Root is IWindow)
                                {
                                    // Environment.Exit(1);
                                    var win = be.Root as IWindow;
                                    if (win.WindowDock)
                                    {
                                        CurrentDock = be.Root as IWindow;
                                        DockWindow = Over as IWindow;
                                    }
                                }
                                if (be is IWindow)
                                {
                                    var win2 = be as IWindow;
                                    if (win2.WindowDock)
                                    {
                                        CurrentDock = be as IWindow;
                                        DockWindow = Over as IWindow;
                                    }
                                }
                                //be.DragOver(Over, MousePosition.x, MousePosition.y);


                            }
                        }

                    }

                    //Console.WriteLine("!!!!!!!!!")

                }

                if (GameInput.WheelDelta.Y != 0)
                {
                    Over.OnMouseWheelMove(GameInput.WheelDelta);
                }

                for (int i = 0; i < 18; i++)
                {
                 
                    if (Pressed[i] == null)
                    {

                        if (GameInput.MouseButtonDown((MouseID)i))
                        {
                            if (Over != ContextForm)
                            {
                                if (ContextForm != null)
                                {
                                    ContextForm = null;
                                }
                            }
                            Pressed[i] = Over;
                            Over.Active = true;
                            if (Active != null && Active != Over)
                            {
                                Active.OnDeactivate();
                                Active.Active = false;
                            }
                            Active = Over;
                            Active.OnActivate();
                            Pressed[i].OnMouseDown((MouseID)i);
                            if (Environment.TickCount < LastClick[i]+500)
                            {
                                Pressed[i].OnDoubleClick((MouseID)i);
                            }
                            else
                            {
                                LastClick[i] = Environment.TickCount;
                            }

                            if (Over is IWindow)
                            {
                                if (Windows.Contains(Over))
                                {
                                    Windows.Remove((IWindow)Over);
                                    Windows.Add((IWindow)Over);
                                }
                            }
                            else if (Over.Root is IWindow)
                            {
                                if (Windows.Contains(Over.Root))
                                {
                                    Windows.Remove((IWindow)Over.Root);
                                    Windows.Add((IWindow)Over.Root);
                                }
                            }
                        }
                        if (i == 1)
                        {
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
                    }
                    else
                    {
                        if (!GameInput.MouseButtonDown((MouseID)i))
                        {
                            Pressed[i].OnMouseUp((MouseID)i);
                            if (Over != Pressed[i])
                            {
                                Pressed[i].OnLeave();
                            }
                            Pressed[i] = null;
                        }
                    }
                }
            }
        }

        private void AddToList(List<IForm> list, IForm form)
        {
            list.Add(form);

            foreach (var f in form.Forms)
            {

                if (f.Override != null)
                {
                    AddToList(list, f.Override);
                }
                else
                {
                    AddToList(list, f);
                }
            }
        }
        public void DrawDockLines(IWindow win)
        {

            foreach(var space in win.dockingSpaces)
            {

                int rx, ry, rw, rh;

                rx = win.Position.x + space.X - 4;
                ry = win.Position.y+win.TitleHeight + space.Y;
                rw = space.Width;
                rh = space.Height;

                win.Draw(UI.Theme.Pure, rx, ry, rw, 5, new Maths.Color(2, 2, 2, 0.5f));
                win.Draw(UI.Theme.Pure, rx, ry, 5, rh, new Maths.Color(2, 2, 2, 0.5f));
                win.Draw(UI.Theme.Pure, rx + rw, ry, 5, rh, new Maths.Color(2,2,2, 0.5f));
                win.Draw(UI.Theme.Pure, rx, ry + rh, rw, 5, new Maths.Color(2,2,2, 0.5f));

                

            }

        }

        IForm WaitOver = null;
        int WaitUntil = 0;

        public void Render()
        {

            
           // Draw.Begin();
            Root.Render();

            foreach(var win in Windows)
            {
                win.Render();
            }

            foreach(var win in Windows)
            {
             //   win.DebugSpaces();
                if (win.WindowDock)
                {
                    DrawDockLines(win);
                }
            }

            ToolBar.Render();

            if (CurrentDock != null)
            {
                //CurrentDock.Draw(UI.Theme.Pure);
                //CurrentDock.DetermineDockPosition(new Forms.Point(MousePosition.x,MousePosition.y),)
                var target = CurrentDock.DetermineDock(new Forms.Point(MousePosition.x, MousePosition.y), 128);
                if (target != null)
                {
                    var th = 18;

                    //Console.WriteLine("Pos:" + target.Position);
                    Maths.Color col = new Maths.Color(0.7f, 0.7f, 0.7f, 0.65f);
                    int rx, ry, rw, rh;
                    switch (target.Position)
                    {
                        case DockPosition.Left:
                            rx = (int)target.Space.Area.X;
                            ry = (int)target.Space.Area.Y + th;
                            rw = (int)target.Space.Area.Width / 4;
                            rh = (int)target.Space.Area.Height;
                            rx = CurrentDock.RenderPosition.x + rx;
                            ry = CurrentDock.RenderPosition.y + ry;
                            ry = ry + CurrentDock.TitleHeight;
                            rh = rh - CurrentDock.TitleHeight;
                            CurrentDock.Draw(UI.Theme.Highlight, rx, ry, rw, rh, col);
                            break;
                        case DockPosition.Right:
                            rx = (int)target.Space.Area.X+(int)target.Space.Area.Width - (int)target.Space.Area.Width / 4;
                            ry = (int)target.Space.Area.Y + th;
                            rw = (int)target.Space.Area.Width / 4;
                            rh = (int)target.Space.Area.Height;
                            rx = CurrentDock.RenderPosition.x + rx;
                            ry = CurrentDock.RenderPosition.y + ry;
                            ry = ry + CurrentDock.TitleHeight;
                            rh = rh - CurrentDock.TitleHeight;
                            CurrentDock.Draw(UI.Theme.Highlight, rx, ry, rw, rh,col);
                            break;
                        case DockPosition.Top:
                            rx = (int)target.Space.Area.X;
                            ry = (int)target.Space.Area.Y + th;
                            rw = (int)target.Space.Area.Width;
                            rh = (int)target.Space.Area.Height / 4;
                            rx = CurrentDock.RenderPosition.x + rx;
                            ry = CurrentDock.RenderPosition.y + ry;
                            ry = ry + CurrentDock.TitleHeight;
                            rh = rh - CurrentDock.TitleHeight;
                            CurrentDock.Draw(UI.Theme.Highlight, rx, ry, rw, rh,col);
                            break;
                        case DockPosition.Bottom:
                            rx = (int)target.Space.Area.X;
                            ry = (int)+target.Space.Area.Y + (int)target.Space.Area.Height - (int)target.Space.Area.Height / 4 + th;
                            rw = (int)target.Space.Area.Width;
                            rh = (int)target.Space.Area.Height / 4;
                            rx = CurrentDock.RenderPosition.x + rx;
                            ry = CurrentDock.RenderPosition.y + ry;
                            ry = ry + CurrentDock.TitleHeight;
                            rh = rh - CurrentDock.TitleHeight;
                            CurrentDock.Draw(UI.Theme.Highlight, rx, ry, rw, rh,col);
                            break;
                        case DockPosition.Centre:

                            rx = (int)target.Space.Area.X;
                            ry = (int)target.Space.Area.Y + th;
                            rw = (int)target.Space.Area.Width;
                            rh = (int)target.Space.Area.Height;
                            rx = CurrentDock.RenderPosition.x + rx;
                            ry = CurrentDock.RenderPosition.y + ry;
                            ry = ry + CurrentDock.TitleHeight;
                            rh = rh - CurrentDock.TitleHeight;
                            CurrentDock.Draw(UI.Theme.Highlight, rx, ry, rw, rh,col);

                            break;
                    }

                 //   Environment.Exit(1);
                 
                    //CurrentDock.Draw(UI.Theme.Frame,)

                }

            }

            if (ContextForm != null)
            {
                ContextForm.Render();
            }
            Menu.Render();


            if(WaitOver == null)
            {
                if (Over != null)
                {
                    WaitOver = Over;
                    WaitUntil = Environment.TickCount + 1000;
                }
            }
            else
            {
                if(WaitOver != Over)
                {
                    WaitOver = null;

                }
            }


            if (WaitOver != null)
            {

                if (Environment.TickCount > WaitUntil)
                {
                    if (Over != null)
                    {
                        if (Over.ToolTip != "")
                        {

                            int w = UI.SystemFont.StringWidth(Over.ToolTip) + 10;
                            int h = UI.SystemFont.StringHeight() + 10;
                            Draw.Begin();

                            Draw.Draw(UI.Theme.Pure, new Rect(MousePosition.x + 20, MousePosition.y - 10, w, h), new Maths.Color(1, 1, 1, 1));
                            Draw.End();
                            UI.DrawString(Over.ToolTip, MousePosition.x + 25, MousePosition.y - 5, UI.Theme.TextColor);
                            //       Draw.End();
                        }
                    }
                }
            }

            // Draw.End();
            GL.Disable(EnableCap.ScissorTest);

            GL.Clear(ClearBufferMask.DepthBufferBit);// //..Disable(EnableCap.DepthTest);

            Draw.Blend = BlendMode.Alpha;
            Draw.Begin();
            Draw.Draw(UICursor, new Rect(MousePosition.x, MousePosition.y, 32, 32),new Maths.Color(1, 1, 1, 0.75f));
            Draw.End();


          

            GL.Enable(EnableCap.DepthTest);

        }
    }
}