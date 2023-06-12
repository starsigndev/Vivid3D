using OpenTK.Compute.OpenCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using static System.Net.Mime.MediaTypeNames;

namespace Vivid.UI.Forms
{
    public class DockTarget
    {
        public DockingSpace Space;
        public DockPosition Position;
    }
    public struct DockRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double Left => X;
        public double Top => Y;
        public double Right => X + Width;
        public double Bottom => Y + Height;

        public Point Location => new Point(X, Y);
        public Size Size
        {
            get;
            set;
        }// => new Size(Width, Height);

        public DockRect(Point loc,Size size)
        {
            X = loc.X;
            Y = loc.Y;
            Size = size;
            Width = size.Width;
            Height = size.Height; 
        }
        public DockRect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Size = new Size(width, height);
        }

        public bool Contains(double x, double y)
        {
            return (X <= x) && (x <= X + Width) && (Y <= y) && (y <= Y + Height);
        }

        public bool Contains(Point point)
        {
            return Contains(point.X, point.Y);
        }
    }
    public struct Size
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public enum DockPosition
    {
        None,
        Left,
        Right,
        Top,
        Bottom,
        Centre
    }


    public class DockingSpace
    {
        public DockRect Area { get; set; }
        public DockPosition Position { get; set; }
        public IWindow DockedWindow { get; set; }

        public DockingSpace(DockRect area, DockPosition position, IWindow dockedWindow = null)
        {
            Area = area;
            Position = position;
            DockedWindow = dockedWindow;
        }

        public bool IsEmpty => DockedWindow == null;

        public void DockWindow(IWindow window)
        {
            if (IsEmpty)
            {
                DockedWindow = window;
            }
            else
            {
                throw new InvalidOperationException("Cannot dock a window in a space that is already occupied.");
            }
        }

        public IWindow UndockWindow()
        {
            var window = DockedWindow;
            DockedWindow = null;
            return window;
        }
    }

    public class IWindow : IForm
    {
        public IButton Title
        {
            get;
            set;
        }

        public IFrame Content
        {
            get;
            set;
        }

        public int TitleHeight
        {
            get;
            set;
        }

        public int EdgeSize
        {
            get;
            set;
        }

        public IFrame RightEdge
        {
            get;
            set;
        }

        public IFrame BottomEdge
        {
            get;
            set;
        }

        public IButton ResizeButton
        {
            get;
            set;
        }

        public IVerticalScroller VerticalScroller
        {
            get;
            set;
        }

        public bool CastShadow
        {
            get;
            set;
        }

        public IWindow DockedTo
        {
            get;
            set;
        }

        public bool WindowDock
        {
            get;
            set;
        }

        public bool Docked
        {
            get;
            set;
        }

        public IWindow ActiveDockedWindow
        {
            get;
            set;
        }

        public IWindow OverTab
        {
            get;
            set;
        }

        public IFrame ThisContent
        {
            get;
            set;
        }

        public DockingSpace BoundSpace
        {
            get;
            set;

        }

        public List<IWindow> DockedWindows { get; set; }

        private List<DockingSpace> dockingSpaces;
        bool Drag = false;
        bool HighlightArea = false;

        public IWindow(string title)
        {
            CastShadow = false;
            Text = title;
            ActiveDockedWindow = null;
            Docked = false;
            DockedWindows = new List<IWindow>();
            //       Title = new IButton();
            Content = new IFrame();
            RightEdge = new IFrame(true);
            BottomEdge = new IFrame(true);
            ResizeButton = new IButton();
            AddForms(Content);// RightEdge, BottomEdge,ResizeButton);
            TitleHeight = 18;
            EdgeSize = 12;
            Content.Scissor = true;
            WindowDock = false;
            DrawOutline = true;
            ThisContent = Content;
            //Title.OnMove = (form, xm, ym) =>
            //{
            //    if (!Static)
            //   {
            //      Position.x += xm;
            //     Position.y += ym;
            //}
            //};
            ResizeButton.Image = UI.Theme.ButtonSelected;
            ResizeButton.OnMove = (form, xm, ym) =>
            {
                if (!Static)
                {
                    Set(Position, new Maths.Size(Size.w + xm, Size.h + ym), Text);
                }
            };
            MinimumSize = new Maths.Size(128, 128);
            VerticalScroller = new IVerticalScroller();
            RightEdge.AddForm(VerticalScroller);
            VerticalScroller.OnMove = (form, x, y) =>
            {
                Content.ScrollValue = new Maths.Position(0, y);
                Console.WriteLine("SY:" + Content.ScrollValue.y);
            };
            Static = false;
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            // Title.Set(0, 0, Size.w, TitleHeight, Text);
            Content.Set(0, TitleHeight, Size.w, (Size.h - TitleHeight + 1));
            RightEdge.Set(Size.w - EdgeSize, Content.Position.y, EdgeSize, Content.Size.h);
            BottomEdge.Set(0, Content.Size.h + TitleHeight, Content.Size.w, EdgeSize);
            ResizeButton.Set(Size.w - EdgeSize, Content.Size.h + TitleHeight, EdgeSize, EdgeSize, "-");
            VerticalScroller.Set(0, 12, EdgeSize, RightEdge.Size.h - 24, "");
            dockingSpaces = new List<DockingSpace>();
            dockingSpaces.Add(new DockingSpace(new DockRect(0, 0, Size.w, Size.h), DockPosition.None));
            //{
            //   new DockingSpace { area = new Rect(0,0,Size.w,Size.h), Position = DockPosition.None }
            //};

            // Content.AfterSetChildren();
        }


        public void CleanUpDockingSpaces(List<DockingSpace> dockingSpaces)
        {
            for (int i = dockingSpaces.Count - 1; i >= 0; i--)
            {
                var space = dockingSpaces[i];

                // If the space has no window, remove it and expand neighbouring spaces
                if (space.IsEmpty)
                {
                    dockingSpaces.RemoveAt(i);

                    foreach (var otherSpace in dockingSpaces)
                    {
                        // If the spaces are horizontally adjacent, expand horizontally
                        if (Math.Abs(space.Area.Top - otherSpace.Area.Top) < 1e-6 && Math.Abs(space.Area.Bottom - otherSpace.Area.Bottom) < 1e-6)
                        {
                            if (Math.Abs(space.Area.Right - otherSpace.Area.Left) < 1e-6)  // space is to the left of otherSpace
                            {
                                otherSpace.Area = new DockRect(otherSpace.Area.Left - space.Area.Width, otherSpace.Area.Top, otherSpace.Area.Width + space.Area.Width, otherSpace.Area.Height);
                            }
                            else if (Math.Abs(space.Area.Left - otherSpace.Area.Right) < 1e-6)  // space is to the right of otherSpace
                            {
                                otherSpace.Area = new DockRect(otherSpace.Area.Left, otherSpace.Area.Top, otherSpace.Area.Width + space.Area.Width, otherSpace.Area.Height);
                            }
                        }
                        // If the spaces are vertically adjacent, expand vertically
                        else if (Math.Abs(space.Area.Left - otherSpace.Area.Left) < 1e-6 && Math.Abs(space.Area.Right - otherSpace.Area.Right) < 1e-6)
                        {
                            if (Math.Abs(space.Area.Bottom - otherSpace.Area.Top) < 1e-6)  // space is above otherSpace
                            {
                                otherSpace.Area = new DockRect(otherSpace.Area.Left, otherSpace.Area.Top - space.Area.Height, otherSpace.Area.Width, otherSpace.Area.Height + space.Area.Height);
                            }
                            else if (Math.Abs(space.Area.Top - otherSpace.Area.Bottom) < 1e-6)  // space is below otherSpace
                            {
                                otherSpace.Area = new DockRect(otherSpace.Area.Left, otherSpace.Area.Top, otherSpace.Area.Width, otherSpace.Area.Height + space.Area.Height);
                            }
                        }
                    }
                }
            }
        }
        public void DockWindow(IWindow win, Point pos, double edge)
        {
            var target = DetermineDock(pos, edge);
            if (target.Space.DockedWindow != null)
            {
                if (target.Space.DockedWindow.DockedWindows.Contains(win))
                {
                    return;
                }
                if(target.Space.DockedWindow == win)
                {
                    return;
                }
                target.Space.DockedWindow.DockedWindows.Add(win);
                win.Set(Position.x,Position.y,target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h,win.Text);
                UI.This.Windows.Remove(win);
                //win.Root.Forms.Remove(win);
                //win.Root = null;

                return;
                //Environment.Exit(0);

            }
            else
            {
                target.Space.DockedWindow = win;
                win.BoundSpace = target.Space;
            }
            int rx, ry, rw, rh;
            Content.AddForm(win);
            UI.This.Windows.Remove(win);
            win.Docked = true;
            win.DockedTo = this;
            switch (target.Position)
            {
                case DockPosition.Left:
                    rx = (int)target.Space.Area.X;
                    ry = (int)target.Space.Area.Y;
                    rw = (int)target.Space.Area.Width / 4;
                    rh = (int)target.Space.Area.Height;
                    win.Set(rx, ry, rw, rh, win.Text);

                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                    //rx = CurrentDock.RenderPosition.x + rx;
                    //ry = CurrentDock.RenderPosition.y + ry;
                    // ry = ry + CurrentDock.TitleHeight;
                    //  rh = rh - CurrentDock.TitleHeight;
                    //CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Right:
                    rx = (int)target.Space.Area.X+(int)target.Space.Area.Width - (int)target.Space.Area.Width / 4;
                    ry = (int)target.Space.Area.Y;
                    rw = (int)target.Space.Area.Width / 4;
                    rh = (int)target.Space.Area.Height;
                    win.Set(rx, ry, rw, rh, win.Text);
                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                  
                    // rx = CurrentDock.RenderPosition.x + rx;
                    //  ry = CurrentDock.RenderPosition.y + ry;
                    //  ry = ry + CurrentDock.TitleHeight;
                    //   rh = rh - CurrentDock.TitleHeight;
                    //     CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Top:
                    rx = (int)target.Space.Area.X;
                    ry = (int)target.Space.Area.Y;
                    rw = (int)target.Space.Area.Width;
                    rh = (int)target.Space.Area.Height / 4;
                    win.Set(rx, ry, rw, rh, win.Text);
                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                    //  rx = CurrentDock.RenderPosition.x + rx;
                    //  ry = CurrentDock.RenderPosition.y + ry;
                    //  ry = ry + CurrentDock.TitleHeight;
                    //  rh = rh - CurrentDock.TitleHeight;
                    //   CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Bottom:
                    rx = (int)target.Space.Area.X;
                    ry = (int)target.Space.Area.Y+(int)target.Space.Area.Height - (int)target.Space.Area.Height / 4;
                    rw = (int)target.Space.Area.Width;
                    rh = (int)target.Space.Area.Height / 4;
                    win.Set(rx, ry, rw, rh, win.Text);
                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                    //  rx = CurrentDock.RenderPosition.x + rx;
                    // ry = CurrentDock.RenderPosition.y + ry;
                    //ry = ry + CurrentDock.TitleHeight;
                    //rh = rh - CurrentDock.TitleHeight;
                    //CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Centre:

                    rx = (int)target.Space.Area.X;
                    ry = (int)target.Space.Area.Y;
                    rw = (int)target.Space.Area.Width;
                    rh = (int)target.Space.Area.Height;
                    win.Set(rx, ry, rw, rh, win.Text);
                    //rx = CurrentDock.RenderPosition.x + rx;
                    // ry = CurrentDock.RenderPosition.y + ry;
                    // ry = ry + CurrentDock.TitleHeight;
                    // rh = rh - CurrentDock.TitleHeight;
                    // CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);

                    break;
            }

        }
        private async void SplitDockingSpace(DockingSpace space, DockRect windowRect, DockPosition position)
        {
            // Create a new docking space for the docked window
            var newSpace = new DockingSpace(new DockRect(space.Area.Location,space.Area.Size), position);
            //newSpace.Area = new DockRect(space.Area.Location, newSize);


            //{ Area = new Rect(space.Area.Location, newSize), Position = position };
            int ax, ay, aw, ah;
            // Adjust the existing docking space
            switch (position)
            {
                case DockPosition.Left:
                  
                    ax = (int)space.Area.X + (int)windowRect.Width;
                    ay = (int)space.Area.Y;// + (int)windowRect.Y;
                    aw = (int)space.Area.Size.Width - (int)windowRect.Width;
                    ah = (int)space.Area.Size.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;
                    //newSpace.Area.X = newSpace.Area.X + windowRect.Width;
                    //space.Area.X += windowRect.Width;
                    //space.Area.Width -= windowRect.Width;
                    break;

                case DockPosition.Right:

                    ax = (int)space.Area.X;
                    ay = (int)space.Area.Y;
                    aw = (int)space.Area.Size.Width - (int)windowRect.Width;
                    ah = (int)space.Area.Size.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;

                    // space.Area.Width -= windowRect.Width;
                    break;

                case DockPosition.Top:
                    ax = (int)space.Area.X;
                    ay = (int)space.Area.Y +(int)windowRect.Height; 
                    aw = (int)space.Area.Size.Width;
                    ah = (int)space.Area.Size.Height - (int)windowRect.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;
                    //   space.Area.Y += windowRect.Height;
                    //     space.Area.Height -= windowRect.Height;
                    break;

                case DockPosition.Bottom:
                    ax = (int)space.Area.X;
                    ay = (int)space.Area.Y;
                    aw = (int)space.Area.Size.Width;
                    ah = (int)space.Area.Size.Height - (int)windowRect.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;
                    //       space.Area.Height -= windowRect.Height;
                    break;
            }

            dockingSpaces.Add(newSpace);
        }
    
    public DockTarget DetermineDock(Point pos,double edge)
        {
            pos.X = pos.X - RenderPosition.x;
            pos.Y = pos.Y - (RenderPosition.y+TitleHeight);
            Console.WriteLine("MP:" + pos.X + " Y:" + pos.Y);
            int sp = 0;
            foreach(var space in dockingSpaces)
            {

                Console.WriteLine("Space:" + sp + " X:" + space.Area.X + " Y:" + space.Area.Y);
                sp++;
                if (pos.X >= space.Area.X && pos.Y >= space.Area.Y && pos.X <= space.Area.X + space.Area.Width && pos.Y <= space.Area.Y + space.Area.Height)
                {
                    var dp = DetermineDockPosition(pos, space.Area, edge);
                    if (dp != DockPosition.None)
                    {
                        DockTarget target = new DockTarget();
                        target.Space = space;
                        target.Position = dp;
                        return target;
                    }
                }
            }
            return null;
        }

        private bool InPos(Point mp,DockRect rect)
        {
            if(mp.X>rect.X && mp.X<=rect.X+rect.Width)
            {
                if(mp.Y>=rect.Y && mp.Y<=rect.Y+rect.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public DockPosition DetermineDockPosition(Point mousePos, DockRect windowRect, double edgeTolerance)
        {
            foreach (var space in dockingSpaces)
            {
                if (space.DockedWindow != null)
                {
                    //return DockPosition.Centre;
                    if (InPos(mousePos, space.Area)) { 
                        return DockPosition.Centre;
                    }
                }
                // Left edge.
                if (InPos(mousePos,new DockRect(space.Area.Left,space.Area.Top,edgeTolerance,space.Area.Height))) // mousePos.X <= space.Area.Left + edgeTolerance && space.Position != DockPosition.Right)
                {
                    return DockPosition.Left;
                }
                // Right edge.
                else if (InPos(mousePos, new DockRect(space.Area.Right-edgeTolerance, space.Area.Top, edgeTolerance, space.Area.Height)))//mousePos.X >= space.Area.Right - edgeTolerance && space.Position != DockPosition.Left)
                {
                    return DockPosition.Right;
                }

                // Top edge.
                if (InPos(mousePos, new DockRect(space.Area.Left, space.Area.Top, space.Area.Width,edgeTolerance)))// mousePos.Y <= space.Area.Top + edgeTolerance && space.Position != DockPosition.Bottom)
                {
                    return DockPosition.Top;
                }
                // Bottom edge.
                else if (InPos(mousePos, new DockRect(space.Area.Left, space.Area.Bottom-edgeTolerance,space.Area.Width, edgeTolerance)))//mousePos.Y >= space.Area.Bottom - edgeTolerance && space.Position != DockPosition.Top)
                {
                    return DockPosition.Bottom;
                }

                // Centre.
                var centre = new Point(space.Area.Left + space.Area.Width / 2, space.Area.Top + space.Area.Height / 2);
                if (Math.Abs(mousePos.X - centre.X) <= edgeTolerance && Math.Abs(mousePos.Y - centre.Y) <= edgeTolerance)
                {
                    return DockPosition.Centre;
                }
            }

            return DockPosition.None;
        }


        public override void OnUpdate()
        {
            //base.OnUpdate();
            int my = Content.ContentSize.h;
            VerticalScroller.MaxValue = my;
         //   Console.WriteLine("ContentY:" + my);
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (mp.y > 0)
            {
                if (mp.y <= TitleHeight)
                {
                    Drag = true;
                }
            }
            if (OverTab != null)
            {
                ActiveDockedWindow = OverTab;
                if(OverTab == this)
                {
                    ActiveDockedWindow = null;
                    Content.Override = null;
                   // Forms.Remove(Content);
                   // Content = ThisContent;
                   // Forms.Add(Content);
      
                }
                else
                {
                    ActiveDockedWindow = OverTab;
                    Content.Override = OverTab.Content;
                    Content.Override.Root = Content.Root;
                 //   OverTab.Content.Size = Content.Size;
                //    OverTab.Content.Position = Content.Position;
                   // Forms.Remove(Content);
                   // Content = OverTab.Content;
                   // Forms.Add(Content);
                    //Content.Root = this;


                }
            }
            //Drag = true;

        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            Drag = false;
        }
        Position mp;

        public void CheckTab(IWindow win,int dx,int dy,int mx,int my)
        {

            //            Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(text) + 15, TitleHeight, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
            if (mx >= RenderPosition.x + dx && mx <= RenderPosition.x +dx+ UI.SystemFont.StringWidth(win.Text) + 15)
            {
                if(my>=RenderPosition.y+dy && my <= RenderPosition.y+dy + TitleHeight)
                {
                    OverTab = win;
                }
            }

        }

        public void Undock()
        {
            if (Docked)
            {
                Docked = false;
                DockedTo.Content.Forms.Remove(this);
                UI.This.Windows.Add(this);
                DockedTo = null;
                BoundSpace.DockedWindow = null;
            

            }
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            mp = new Position(position.x, position.y);
            mp.x = mp.x - RenderPosition.x;
            mp.y = mp.y - RenderPosition.y;
            //base.OnMouseMove(position, delta);
            OverTab = null;
            CheckTab(this, 0, 0, position.x, position.y);
            int dx, dy;

            dx = UI.SystemFont.StringWidth(Text) + 20;

            foreach (var tab in DockedWindows)
            {
                CheckTab(tab, dx, 0, position.x, position.y);
                dx = dx + UI.SystemFont.StringWidth(tab.Text) + 12;

            }
            if (OverTab != null)
            {
                //Environment.Exit(1);

            }


            if (Drag)
            {

                // OnMove?.Invoke(this, (int)delta.x, (int)delta.y);
                //   Environment.Exit(1);
                if (!Static)
                {

                    if (Docked)
                    {

                        //Console.WriteLine("Del:" + delta.Length);
                        if (delta.Length > 30)
                        {
                            if (ActiveDockedWindow == null)
                            {
                                Undock();
                            }
                            else
                            {


                                DockedWindows.Remove(ActiveDockedWindow);
                                ActiveDockedWindow.Position = RenderPosition + mp;
                                UI.This.Windows.Add(ActiveDockedWindow);
                                UI.This.Pressed[0] = null;
                                if (Content.Override == ActiveDockedWindow.Content)
                                {
                                    ActiveDockedWindow.Content.Root = ActiveDockedWindow;
                                    ActiveDockedWindow.Drag = true;
                                    UI.This.Pressed[0] = ActiveDockedWindow;
                                    UI.This.Over = ActiveDockedWindow;
                                    Content.Override = null;
                                    ActiveDockedWindow = null;
                                    Drag = false;


                                }
                                // UI.This.Over = null;




                            }
                            //Docked = false;


                        }
                    }
                    else
                    {
                        if (ActiveDockedWindow != null)
                        {

                            if (delta.Length > 30)
                            {

                                DockedWindows.Remove(ActiveDockedWindow);
                                ActiveDockedWindow.Position = RenderPosition + mp;
                                UI.This.Windows.Add(ActiveDockedWindow);
                                UI.This.Pressed[0] = null;
                                if (Content.Override == ActiveDockedWindow.Content)
                                {
                                    ActiveDockedWindow.Content.Root = ActiveDockedWindow;
                                    ActiveDockedWindow.Drag = true;
                                    UI.This.Pressed[0] = ActiveDockedWindow;
                                    UI.This.Over = ActiveDockedWindow;
                                    Content.Override = null;
                                    ActiveDockedWindow = null;
                                    Drag = false;


                                }
                                // UI.This.Over = null;




                            }
                            else
                            {

                                Position.x = Position.x + (int)delta.x;
                                Position.y = Position.y + (int)delta.y;
                            }
                            //Docked = false;


                        }
                        else
                        {
                            Position.x = Position.x + (int)delta.x;
                                Position.y = Position.y + (int)delta.y;
                        }
                        //int b = 5;

                    }
                }
            }
        }

        public override void DragOver(IForm form, int mx, int my)
        {
            //base.DragOver(form, mx, my);
            HighlightArea = true;

        }

        private void DrawTab(IWindow tab, int x, int y)
        {

            if (ActiveDockedWindow==null)
            {
                if(tab == this)
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight, new Maths.Color(1.35f, 1.35f, 1.35f, 0.85f));
                }
                else
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight, new Maths.Color(0.5f, 0.5f, 0.5f, 0.85f));
                }
            }
            else
            {
                if (ActiveDockedWindow == tab)
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight, new Maths.Color(1.35f, 1.35f, 1.35f, 0.85f));
                }
                else
                {
                    if (tab == OverTab)
                    {
                        Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
                    }
                    else
                    {
                        Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight, new Maths.Color(0.5f, 0.5f, 0.5f, 0.85f));
                    }
                }
            }
          //   if (HighlightArea)
            //     {

            UI.DrawString(tab.Text, RenderPosition.x +x+4, RenderPosition.y, UI.Theme.TextColor);
        }

        public override void OnRender()
        {
            //base.OnRender();

            DrawTab(this, 0, 0);
            int dx, dy;

            dx = UI.SystemFont.StringWidth(Text) + 20;

            foreach(var win in DockedWindows)
            {
                DrawTab(win, dx, 0);
                dx = dx + UI.SystemFont.StringWidth(win.Text) + 12;
            }
            //Draw(UI.Theme.FrameH, RenderPosition.x, RenderPosition.y, Size.w, TitleHeight, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
         //   if (HighlightArea)
       //     {
            //    UI.DrawString(Text, RenderPosition.x + 8, RenderPosition.y, UI.Theme.TextColor);
       


            if (CastShadow)
            {
                Draw(UI.Theme.FrameShadow, RenderPosition.x + 16, RenderPosition.y + 32, Size.w + 16, Size.h + 16, new Maths.Color(1, 1, 1, 0.5f));
            }

     

        }

    }
}
