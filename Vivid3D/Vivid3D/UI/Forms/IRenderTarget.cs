using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.RenderTarget;

namespace Vivid.UI.Forms
{
    public delegate void RenderAction(int w, int h);
    public delegate RenderTarget2D GetBufferAction();
    public class IRenderTarget : IForm
    {

        public RenderTarget.RenderTarget2D RenderTarget
        {
            get;
            set;
        }

        public event RenderAction ActionRender;
        public event RenderAction PreRender;
        public event GetBufferAction OnGetBuffer;

        public IRenderTarget()
        {
            ActionRender = null;
        }

        public override void AfterSet()
        {
            //base.AfterSet();
            if (Root != null)
            {
                Size = Root.Size;
            }
            if (RenderTarget != null)
            {
                RenderTarget.Delete();
            }
            RenderTarget = new RenderTarget.RenderTarget2D(Size.w, Size.h);
            Position = new Maths.Position(0, 0);
           

        }

     


        public override void OnRender()
        {
            //base.OnRender();
            PreRender?.Invoke(Size.w, Size.h);
            ActionRender?.Invoke(Size.w, Size.h);
            var rb = OnGetBuffer.Invoke();
            RenderTarget.Bind();
            Draw(rb.GetTexture(), 0, Size.h, Size.w, -Size.h);
            RenderTarget.Release();




            Draw(RenderTarget.GetTexture(), RenderPosition.x, RenderPosition.y + Size.h, Size.w, -Size.h, new Maths.Color(1, 1, 1, 1));
        }


    }
}
