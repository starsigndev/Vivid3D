using OpenTK.Compute.OpenCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

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

        public bool WindowDock
        {
            get;
            set;
        }

        private List<DockingSpace> dockingSpaces;
        bool Drag = false;
        bool HighlightArea = false;

        public IWindow(string title)
        {
            CastShadow = false;
            Text = title;
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
        public void DockWindow(IWindow win, Point pos, double edge)
        {
            var target = DetermineDock(pos, edge);
            target.Space.DockedWindow = win;
            int rx, ry, rw, rh;
            Content.AddForm(win);
            UI.This.Windows.Remove(win);
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
                    ry = (int)target.Space.Area.Height - (int)target.Space.Area.Height / 4;
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
                    ay = (int)space.Area.Y + (int)windowRect.Y;
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

            //Drag = true;

        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            Drag = false;
        }
        Position mp;
        public override void OnMouseMove(Position position, Delta delta)
        {
            mp = new Position(position.x, position.y);
            mp.x = mp.x-RenderPosition.x;
            mp.y = mp.y-RenderPosition.y;
            //base.OnMouseMove(position, delta);
            if (Drag)
            {

                // OnMove?.Invoke(this, (int)delta.x, (int)delta.y);
                //   Environment.Exit(1);
                if (!Static)
                {
                    Position.x = Position.x + (int)delta.x;
                    Position.y = Position.y +(int) delta.y;
                }
            }
        }

        public override void DragOver(IForm form, int mx, int my)
        {
            //base.DragOver(form, mx, my);
            HighlightArea = true;

        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.FrameH, RenderPosition.x, RenderPosition.y, Size.w, TitleHeight, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
         //   if (HighlightArea)
       //     {
                UI.DrawString(Text, RenderPosition.x + 8, RenderPosition.y, UI.Theme.TextColor);
       


            if (CastShadow)
            {
                Draw(UI.Theme.FrameShadow, RenderPosition.x + 16, RenderPosition.y + 32, Size.w + 16, Size.h + 16, new Maths.Color(1, 1, 1, 0.5f));
            }
        }

    }
}
