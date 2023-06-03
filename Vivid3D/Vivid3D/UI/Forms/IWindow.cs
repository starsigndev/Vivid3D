using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
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

        public IWindow(string title)
        {
            Text = title;
            Title = new IButton();
            Content = new IFrame();
            RightEdge = new IFrame(true);
            BottomEdge = new IFrame(true);
            ResizeButton = new IButton();
            AddForms(Title, Content, RightEdge, BottomEdge,ResizeButton);
            TitleHeight = 28;
            EdgeSize = 12;
            Title.OnMove = (form, xm, ym) =>
            {
                Position.x += xm;
                Position.y += ym;
            };
            ResizeButton.Image = UI.Theme.ButtonSelected;
            ResizeButton.OnMove = (form, xm, ym) =>
            {
                Set(Position, new Maths.Size(Size.w+xm, Size.h+ym), Text);
            }         ;
            MinimumSize = new Maths.Size(128,128);
            VerticalScroller = new IVerticalScroller();
            RightEdge.AddForm(VerticalScroller);
            VerticalScroller.OnMove = (form, x, y) =>
            {
                Content.ScrollValue = new Maths.Position(0, y);
                Console.WriteLine("SY:" + Content.ScrollValue.y);
            };
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            Title.Set(0, 0, Size.w, TitleHeight, Text);
            Content.Set(0, TitleHeight, Size.w-EdgeSize,(Size.h - TitleHeight + 1)-EdgeSize);
            RightEdge.Set(Size.w - EdgeSize,Content.Position.y, EdgeSize, Content.Size.h);
            BottomEdge.Set(0, Content.Size.h+TitleHeight, Content.Size.w, EdgeSize);
            ResizeButton.Set(Size.w - EdgeSize, Content.Size.h + TitleHeight, EdgeSize, EdgeSize, "-");
            VerticalScroller.Set(0, 0, EdgeSize, RightEdge.Size.h, "");
        }
        public override void OnUpdate()
        {
            //base.OnUpdate();
            int my = Content.ContentSize.h;
            VerticalScroller.MaxValue = my;
            Console.WriteLine("ContentY:" + my);
        }
        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.FrameShadow, RenderPosition.x + 16, RenderPosition.y + 32, Size.w + 16, Size.h + 16, new Maths.Color(1, 1, 1, 0.5f));

        }

    }
}
