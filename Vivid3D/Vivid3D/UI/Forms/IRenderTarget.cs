using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public delegate void RenderAction(int w, int h);
    public class IRenderTarget : IForm
    {

        public RenderTarget.RenderTarget2D RenderTarget
        {
            get;
            set;
        }

        public RenderAction ActionRender
        {
            get;
            set;
        }

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
            RenderTarget = new RenderTarget.RenderTarget2D(Size.w, Size.h);

        }

       

        public override void OnRender()
        {
            //base.OnRender();
            RenderTarget.Bind();
            ActionRender?.Invoke(Size.w, Size.h);
            RenderTarget.Release();


         
            Draw(RenderTarget.GetTexture());
        }


    }
}
