using OpenTK.Compute.OpenCL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using static System.Net.Mime.MediaTypeNames;

namespace Vivid.UI.Forms
{
    public static class DockMan
    {
        public static int Width, Height;
    }
    public class DockTarget
    {
        public DockingSpace Space;
        public DockPosition Position;
    }
    public class DockRect
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
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
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
        public Vivid.Maths.Color DebugCol;
        public int X
        {
            set
            {
                Area.X = value;
            }
            get
            {
                return (int)Area.X;
            }
        }

        public int Y
        {
            set
            {
                Area.Y = value;
            }
            get
            {
                return (int)Area.Y;
            }
        }

        public int Width
        {
            set
            {
                Area.Width = value;
            }
            get
            {
                return (int)Area.Width;
            }
        }

        public int Height
        {
            set
            {
                Area.Height = value;
            }
            get
            {
                return (int)Area.Height;
            }
        }


        public bool Hit(int x,int y)
        {
            if(x>=Area.X && x<=Area.X+Area.Width && y>=Area.Y && y <= Area.Y + Area.Height)
            {
                return true;
            }
            return false;
        }

        public DockRect Area { get; set; }
        public DockPosition Position { get; set; }
        public IWindow DockedWindow { get; set; }
        private float originalXRatio;
        private float originalYRatio;
        private float originalWidthRatio;
        private float originalHeightRatio;
        public DockingSpace(DockRect area, DockPosition position, IWindow dockedWindow = null)
        {
            Area = area;
            Position = position;
            DockedWindow = dockedWindow;
            Random rnd = new Random(Environment.TickCount);
            DebugCol = new Maths.Color(1,1,1,1);
            DebugCol.r = (float)rnd.NextDouble();
            DebugCol.g = (float)rnd.NextDouble();
            DebugCol.b = (float)rnd.NextDouble();
            DebugCol.a = 0.35f;
    
            int aa = 5;
        }

        public void UpdateProportions(IWindow dockedWindow)
        {
            originalXRatio = ((float)Area.X / (float)dockedWindow.Size.w);
            originalYRatio = ((float)Area.Y / (float)(dockedWindow.Size.h-18));
            originalWidthRatio = ((float)Area.Width / (float)dockedWindow.Size.w);
            originalHeightRatio = ((float)Area.Height / (float)(dockedWindow.Size.h-18));

            int bb = 5;
        }
         
        public void WindowResize(IWindow win)
        {
            float X, Y, Width, Height;

            X = originalXRatio * win.Size.w;
            Y = originalYRatio * (win.Size.h-18);
            Width = originalWidthRatio * win.Size.w;
            Height = originalHeightRatio * (win.Size.h - 18);

            Area = new DockRect(X, Y, Width, Height);

            int aaa = 5;
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

        public IWindow(string title,bool resizable=false)
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
            if (resizable)
            {
                AddForms(Content, ResizeButton);// RightEdge, BottomEdge,ResizeButton);
            }
            else
            {
                AddForms(Content);
            }
            TitleHeight = 18;

            EdgeSize = 16;
            Content.Scissor = true;
            WindowDock = false;
            //    DrawOutline = true;
            Content.DrawOutline = true;
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
            ResizeButton.OnMove += (form, xm, ym) =>
            {
                if (!Static)
                {
                    PrevSize = Size;
                    Set(Position, new Maths.Size(Size.w + xm, Size.h + ym), Text);
                    RemoveAndMerge(null);


                    //UpdateDocks();
                }
            };
            MinimumSize = new Maths.Size(0, 0);
            VerticalScroller = new IVerticalScroller();
            RightEdge.AddForm(VerticalScroller);
            VerticalScroller.OnMove += (form, x, y) =>
            {
                Content.ScrollValue = new Maths.Position(0, y);
               
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
            ResizeButton.Set(Size.w - EdgeSize+3, Content.Size.h+2+3, EdgeSize-2, EdgeSize-2, "");
            VerticalScroller.Set(0, 12, EdgeSize, RightEdge.Size.h - 24, "");
            if (WindowDock && dockingSpaces == null)
            {
                dockingSpaces = new List<DockingSpace>();
                dockingSpaces.Add(new DockingSpace(new DockRect(0, 0, Size.w, Size.h - TitleHeight), DockPosition.None));
                dockingSpaces[0].UpdateProportions(this);
                DockMan.Width = Size.w;
                DockMan.Height = Size.h;
            }
            else
            {
                if (dockingSpaces != null)
                {
                    if (dockingSpaces.Count > 0)
                    {
                        foreach (var space in dockingSpaces)
                        {
                            space.WindowResize(this);
                            //space.UpdateProportions(this);

                        }
                    }
                }
            }
            //{

            //   new DockingSpace { area = new Rect(0,0,Size.w,Size.h), Position = DockPosition.None }
            //};

            // Content.AfterSetChildren();
        }
        public static List<DockingSpace> MergeAdjacentEmptySpaces(List<DockingSpace> spaces)
        {
            if (spaces.Count <= 1)
            {
                return spaces;
            }

            var mergedSpaces = new List<DockingSpace>();

            // Sort the spaces based on their top and left coordinates.
            spaces.Sort((a, b) =>
            {
                int topComparison = a.Area.Top.CompareTo(b.Area.Top);
                if (topComparison != 0)
                {
                    return topComparison;
                }
                return a.Area.Left.CompareTo(b.Area.Left);
            });

            var currentSpace = spaces[0];

            for (int i = 1; i < spaces.Count; i++)
            {
                var nextSpace = spaces[i];

                if (currentSpace.IsEmpty && nextSpace.IsEmpty && AreAdjacent(currentSpace, nextSpace))
                {
                    var mergedSpace = MergeSpaces(currentSpace, nextSpace);
                    if (mergedSpace != null)
                    {
                        currentSpace = mergedSpace;
                        continue;
                    }
                }

                mergedSpaces.Add(currentSpace);
                currentSpace = nextSpace;
            }

            mergedSpaces.Add(currentSpace);
            return mergedSpaces;
        }

        private static bool AreAdjacent(DockingSpace space1, DockingSpace space2)
        {
            return space1.Area.Right == space2.Area.Left ||
                   space1.Area.Left == space2.Area.Right ||
                   space1.Area.Bottom == space2.Area.Top ||
                   space1.Area.Top == space2.Area.Bottom;
        }

        private static DockingSpace MergeSpaces(DockingSpace space1, DockingSpace space2)
        {
            var mergedArea = CalculateMergedArea(space1.Area, space2.Area);

            if (IsAreaWithinUsedSpaces(mergedArea, space1, space2))
            {
                var mergedPosition = CalculateMergedPosition(space1.Position, space2.Position, mergedArea);
                return new DockingSpace(mergedArea, mergedPosition);
            }

            return null;
        }

        private static DockRect CalculateMergedArea(DockRect area1, DockRect area2)
        {
            int left = (int)Math.Min(area1.Left, area2.Left);
            int top = (int)Math.Min(area1.Top, area2.Top);
            int right = (int)Math.Max(area1.Right, area2.Right);
            int bottom = (int)Math.Max(area1.Bottom, area2.Bottom);

            return new DockRect(left, top, right, bottom);
        }

        private static DockPosition CalculateMergedPosition(DockPosition position1, DockPosition position2, DockRect mergedArea)
        {
            // Calculate the merged position based on the relative positions of the spaces.
            if (position1 == DockPosition.Left || position2 == DockPosition.Left)
            {
                return DockPosition.Left;
            }
            else if (position1 == DockPosition.Right || position2 == DockPosition.Right)
            {
                return DockPosition.Right;
            }
            else if (position1 == DockPosition.Top || position2 == DockPosition.Top)
            {
                return DockPosition.Top;
            }
            else if (position1 == DockPosition.Bottom || position2 == DockPosition.Bottom)
            {
                return DockPosition.Bottom;
            }
            else
            {
                // If neither space has a dock position, use a default position.
                return DockPosition.None;
            }
        }

        private static bool IsAreaWithinUsedSpaces(DockRect area, DockingSpace space1, DockingSpace space2)
        {
            // Check if the merged area is within the already used space.
            var usedArea = CalculateMergedArea(space1.Area, space2.Area);

            return area.Left >= usedArea.Left &&
                   area.Top >= usedArea.Top &&
                   area.Right <= usedArea.Right &&
                   area.Bottom <= usedArea.Bottom;
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
                if (target.Space.DockedWindow == win)
                {
                    return;
                }
                target.Space.DockedWindow.DockedWindows.Add(win);
                win.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, win.Text);
                foreach (var dw in win.DockedWindows)
                {
                    dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                }
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
                    foreach (var dw in win.DockedWindows)
                    {
                        dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                    }
                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                    //rx = CurrentDock.RenderPosition.x + rx;
                    //ry = CurrentDock.RenderPosition.y + ry;
                    // ry = ry + CurrentDock.TitleHeight;
                    //  rh = rh - CurrentDock.TitleHeight;
                    //CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Right:
                    rx = (int)target.Space.Area.X + (int)target.Space.Area.Width - (int)target.Space.Area.Width / 4;
                    ry = (int)target.Space.Area.Y;
                    rw = (int)target.Space.Area.Width / 4;
                    rh = (int)target.Space.Area.Height;
                    win.Set(rx, ry, rw, rh, win.Text);
                    foreach (var dw in win.DockedWindows)
                    {
                        dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                    }
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
                    foreach (var dw in win.DockedWindows)
                    {
                        dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                    }
                    SplitDockingSpace(target.Space, new DockRect(rx, ry, rw, rh), target.Position);
                    //  rx = CurrentDock.RenderPosition.x + rx;
                    //  ry = CurrentDock.RenderPosition.y + ry;
                    //  ry = ry + CurrentDock.TitleHeight;
                    //  rh = rh - CurrentDock.TitleHeight;
                    //   CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);
                    break;
                case DockPosition.Bottom:
                    rx = (int)target.Space.Area.X;
                    ry = (int)target.Space.Area.Y + (int)target.Space.Area.Height - (int)target.Space.Area.Height / 4;
                    rw = (int)target.Space.Area.Width;
                    rh = (int)target.Space.Area.Height / 4;
                    win.Set(rx, ry, rw, rh, win.Text);
                    foreach (var dw in win.DockedWindows)
                    {
                        dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                    }
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
                    foreach (var dw in win.DockedWindows)
                    {
                        dw.Set(Position.x, Position.y, target.Space.DockedWindow.Size.w, target.Space.DockedWindow.Size.h, dw.Text);
                    }
                    //rx = CurrentDock.RenderPosition.x + rx;
                    // ry = CurrentDock.RenderPosition.y + ry;
                    // ry = ry + CurrentDock.TitleHeight;
                    // rh = rh - CurrentDock.TitleHeight;
                    // CurrentDock.Draw(UI.Theme.Frame, rx, ry, rw, rh, col);

                    break;
            }
            win.Size.w = target.Space.Width;
            win.Size.h = target.Space.Height;
            //CheckOverwrite();

        }

        

        private async void SplitDockingSpace(DockingSpace space, DockRect windowRect, DockPosition position)
        {
            // Create a new docking space for the docked window
            var newSpace = new DockingSpace(new DockRect(space.Area.Location, space.Area.Size), position);
            //newSpace.Area = new DockRect(space.Area.Location, newSize);


            //{ Area = new Rect(space.Area.Location, newSize), Position = position };
            int ax, ay, aw, ah;
            // Adjust the existing docking space
            switch (position)
            {
                case DockPosition.Left:

                    ax = (int)space.Area.X + (int)windowRect.Width;
                    ay = (int)space.Area.Y;// + (int)windowRect.Y;
                    aw = (int)space.Area.Width - (int)windowRect.Width;
                    ah = (int)space.Area.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;
                    space.UpdateProportions(this);
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
                    space.UpdateProportions(this);

                    // space.Area.Width -= windowRect.Width;
                    break;

                case DockPosition.Top:
                    ax = (int)space.Area.X;
                    ay = (int)space.Area.Y + (int)windowRect.Height;
                    aw = (int)space.Area.Size.Width;
                    ah = (int)space.Area.Size.Height - (int)windowRect.Height;
                    newSpace.Area = new DockRect(ax, ay, aw, ah);
                    space.Area = windowRect;
                    space.UpdateProportions(this);
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
                    space.UpdateProportions(this);
                    //       space.Area.Height -= windowRect.Height;
                    break;
            }

            newSpace.UpdateProportions(this);
            dockingSpaces.Add(newSpace);
        }

        public DockTarget DetermineDock(Point pos, double edge)
        {
            pos.X = pos.X - RenderPosition.x;
            pos.Y = pos.Y - (RenderPosition.y + TitleHeight);
            Console.WriteLine("MP:" + pos.X + " Y:" + pos.Y);
            int sp = 0;
            foreach (var space in dockingSpaces)
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

        private bool InPos(Point mp, DockRect rect)
        {
            if (mp.X > rect.X && mp.X <= rect.X + rect.Width)
            {
                if (mp.Y >= rect.Y && mp.Y <= rect.Y + rect.Height)
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
                if (InPos(mousePos, new DockRect(space.Area.Left, space.Area.Top, edgeTolerance, space.Area.Height))) // mousePos.X <= space.Area.Left + edgeTolerance && space.Position != DockPosition.Right)
                {
                    return DockPosition.Left;
                }
                // Right edge.
                else if (InPos(mousePos, new DockRect(space.Area.Right - edgeTolerance, space.Area.Top, edgeTolerance, space.Area.Height)))//mousePos.X >= space.Area.Right - edgeTolerance && space.Position != DockPosition.Left)
                {
                    return DockPosition.Right;
                }

                // Top edge.
                if (InPos(mousePos, new DockRect(space.Area.Left, space.Area.Top, space.Area.Width, edgeTolerance)))// mousePos.Y <= space.Area.Top + edgeTolerance && space.Position != DockPosition.Bottom)
                {
                    return DockPosition.Top;
                }
                // Bottom edge.
                else if (InPos(mousePos, new DockRect(space.Area.Left, space.Area.Bottom - edgeTolerance, space.Area.Width, edgeTolerance)))//mousePos.Y >= space.Area.Bottom - edgeTolerance && space.Position != DockPosition.Top)
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
                if (OverTab == this)
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

        public void CheckTab(IWindow win, int dx, int dy, int mx, int my)
        {

            //            Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(text) + 15, TitleHeight, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
            if (mx >= RenderPosition.x + dx && mx <= RenderPosition.x + dx + UI.SystemFont.StringWidth(win.Text) + 15)
            {
                if (my >= RenderPosition.y + dy && my <= RenderPosition.y + dy + TitleHeight)
                {
                    OverTab = win;
                }
            }

        }

        public void MergeDown(DockingSpace space)
        {

            int dy = space.Y + space.Height;
            int sy = 2000;

            while (true)
            {
                int dx = space.X + 1;
                bool hit = false;
                for (int x = 0; x < space.Width - 2; x++)
                {

                    int cx = dx + x;

                    hit = false;

                    foreach (var check in dockingSpaces)
                    {
                        if (check == space) continue;

                        if (check.Hit(cx, dy))
                        {
                            int b = 5;
                            hit = true;
                            break;
                        }
                    }

                    if (hit)
                    {

                        if (dy < sy)
                        {
                            sy = dy;
                        }

                        break;
                    }
                }

                if (hit)
                {
                    //int change = dx - (space.X + space.Width);
                    //space.Width = space.Width + change;
                    //return;
                    //int bb = 5;

                }

                dy++;
                if (dy >= Size.h-TitleHeight)


                {
                    if (dy < sy)
                    {
                        sy = dy;
                    }
                    break;


                }
            }

            if (sy > Size.h-TitleHeight)
            {
                sy = Size.h - TitleHeight;
            }

            int change = sy - space.Y;

            space.Height = change;
            return;
        }

        public void MergeRight(DockingSpace space)
        {

            int dx = space.X + space.Width;
            int sx = 2000;

            while (true)
            {
                int dy = space.Y + 1;
                bool hit = false;
                for (int y = 0; y < space.Height-2; y++)
                {

                    int cy = dy + y;

                    hit = false;

                    foreach (var check in dockingSpaces)
                    {
                        if (check == space) continue;

                        if (check.Hit(dx, cy))
                        {
                            int b = 5;
                            hit = true;
                            break;
                        }
                    }

                    if (hit)
                    {

                        if (dx < sx)
                        {
                            sx = dx;
                        }

                        break;
                    }
                }

                if (hit)
                {
                    //int change = dx - (space.X + space.Width);
                    //space.Width = space.Width + change;
                    //return;
                    //int bb = 5;

                }

                dx++;
                if (dx >= Size.w)


                {
                    if (dx < sx)
                    {
                        sx = dx;
                    }
                    break;

                   
                }
            }
            if (sx > Size.w)
            {
                sx = Size.w;
            }
            int change = sx - space.X;

            space.Width = change;
            return;
        }
        public void MergeUp(DockingSpace space)
        {

            int dy = space.Y;

            int sy = -2000;
            while (true)
            {
                int dx = space.X + 1;
                bool hit = false;
                for (int x = 0; x < space.Width - 2; x++)
                {

                    int cx = dx + x;
                    hit = false;

                    foreach (var check in dockingSpaces)
                    {
                        if (check == space) continue;

                        if (check.Hit(cx, dy))
                        {
                            int b = 5;
                            hit = true;
                            break;
                        }
                    }

                    if (hit)
                    {

                        if (dy > sy)
                        {
                            sy = dy;
                        }

                        break;
                    }

                }

                if (hit)
                {
                    int bb = 5;
                    if (dx == space.X)
                    {
                        //   return;
                    }
                }

                dy--;
                if (dy < 1)
                {
                    if (dy > sy)
                    {
                        sy = dy;
                    }
                    break;
                    //return;
                }


            }

            if (sy < 0)
            {
                sy = 0;
            }
            int change = space.Y - sy;
            space.Y = space.Y - change;
            space.Height = space.Height + change;

        }
        public void MergeLeft(DockingSpace space)
        {

            int dx = space.X;

            int sx = -2000;
            while (true)
            {
                int dy = space.Y + 1;
                bool hit = false;
                for (int y = 0; y < space.Height-2; y++)
                {

                    int cy = dy + y;
                    hit = false;
                  
                    foreach (var check in dockingSpaces)
                    {
                        if (check == space) continue;

                        if (check.Hit(dx, cy))
                        {
                            int b = 5;
                            hit = true;
                            break;
                        }
                    }

                    if (hit)
                    {

                        if (dx > sx)
                        {
                            sx = dx;
                        }

                        break;
                    }

                }

                if (hit)
                {
                    int bb = 5;
                    if (dx == space.X)
                    {
                     //   return;
                    }
                }

                dx--;
                if (dx < 1)
                {
                    if (dx > sx)
                    {
                        sx = dx;
                    }
                    break;
                    //return;
                }


            }
            if (sx < 0)
            {
                sx = 0;
            }

            int change = space.X - sx;
            space.X = space.X - change;
            space.Width = space.Width + change;

        }
  
        public void RemoveAndMerge(DockingSpace toRemove)
        {
           
            if (dockingSpaces.Count == 1)
            {

                dockingSpaces[0].Area.X = 0;
                dockingSpaces[0].Area.Y = 0;
                dockingSpaces[0].Area.Width = Size.w;
                dockingSpaces[0].Area.Height = Size.h - TitleHeight;
                dockingSpaces[0].DockedWindow = null;
                dockingSpaces[0].UpdateProportions(this);
                return;
            }

            if (toRemove != null)
            {
                dockingSpaces.Remove(toRemove);

            }

            foreach (var space in dockingSpaces)
            {

             
                //for (int i = 0; i < 5; i++)
                //{
                    MergeLeft(space);
                    MergeRight(space);
                    MergeUp(space);
                    MergeDown(space);
             //   space.UpdateProportions(this);
              
                    if (space.X + space.Width > Size.w)
                {
                   // int change = space.X + space.Width;
                  //  change = change - Size.w;
                 //   space.Width -= change;
                }
                if (space.Y + space.Height > Size.h)
                {
                //    int change = space.Y + space.Height;
               //     change = change - Size.h;
               //     space.Height -= change;
                }
                //

                if (space.DockedWindow != null)
                {
                    space.DockedWindow.Set(space.X, space.Y, space.Width, space.Height, space.DockedWindow.Text);
                    foreach(var dw in space.DockedWindow.DockedWindows)
                    {
                        dw.Set(space.X, space.Y, space.Width, space.Height, dw.Text);
                    }
                }

            }

            int b = 5;

        }
        public void Undock(Position pos)
        {
            if (Docked)
            {
                Docked = false;
                DockedTo.Content.Forms.Remove(this);
                this.Root = null;
                UI.This.Windows.Add(this);
                //DockedTo = null;
                this.Position = pos + new Position(-5, -5);
                BoundSpace.DockedWindow = null;
                //DockedTo.dockingSpaces = MergeAdjacentEmptySpaces(DockedTo.dockingSpaces);
                DockedTo.RemoveAndMerge(BoundSpace);
                DockedTo = null;


                int a = 5;

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
                        if (delta.Length > 25)
                        {
                            if (ActiveDockedWindow == null)
                            {
                                Undock(position);
                            }
                            else
                            {


                                DockedWindows.Remove(ActiveDockedWindow);
                                ActiveDockedWindow.Position = position + new Position(-5, -5);
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
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight-3, new Maths.Color(1.35f, 1.35f, 1.35f, 0.85f));
                }
                else
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight-3, new Maths.Color(0.5f, 0.5f, 0.5f, 0.85f));
                }
            }
            else
            {
                if (ActiveDockedWindow == tab)
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight-3, new Maths.Color(1.35f, 1.35f, 1.35f, 0.85f));
                }
                else
                {
                    if (tab == OverTab)
                    {
                        Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight-3, new Maths.Color(0.7f, 0.7f, 0.7f, 0.85f));
                    }
                    else
                    {
                        Draw(UI.Theme.Frame, RenderPosition.x + x, RenderPosition.y + y, UI.SystemFont.StringWidth(tab.Text) + 15, TitleHeight-3, new Maths.Color(0.5f, 0.5f, 0.5f, 0.85f));
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

        public void DebugSpaces()
        {
            if (dockingSpaces != null)
            {
                foreach (var space in dockingSpaces)
                {

                    Draw(UI.Theme.Pure, RenderPosition.x + space.X, RenderPosition.y+TitleHeight + space.Y+3, space.Width, space.Height, space.DebugCol);

                }
            } 
        }
    }
}
