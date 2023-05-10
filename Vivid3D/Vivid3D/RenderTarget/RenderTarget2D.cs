using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Vivid.App;
using Vivid.Texture;

namespace Vivid.RenderTarget
{
    public class RenderTarget2D
    {
        private FramebufferHandle FB;
        public Texture2D BB;
        public TextureDepth DB;
        private RenderbufferHandle RB;

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public RenderTarget2D(int w, int h)
        {
            Width = w;
            Height = h;

            FB = GL.CreateFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FB);
            BB = new Texture2D(w, h);
            DB = new TextureDepth(w, h);
            RB = GL.CreateRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, RB);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.DepthComponent32, w, h);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, RB);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, BB.Handle, 0);
            DrawBufferMode db = DrawBufferMode.ColorAttachment0;
            GL.DrawBuffers(1, db);
            var fs = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fs != FramebufferStatus.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer failure.");
                throw (new Exception("Framebuffer failed:" + fs.ToString()));
            }
            Console.WriteLine("Framebuffer success.");
            //GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            //GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferHandle.Zero);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, RenderbufferHandle.Zero);
        }

        ~RenderTarget2D()
        {
            GL.DeleteFramebuffer(FB);

            GL.DeleteRenderbuffer(RB);
        }

        public void ClearZ()
        {
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FB);
            // SetVP.Set(0, 0, IW, IH);
            VividApp.BoundRT2D = this;

            GL.Viewport(0, 0, Width, Height);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //    GL.ClearColor(1, 0, 0, 1);
        }


        public void Release()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferHandle.Zero);
            // SetVP.Set(0, 0, AppInfo.W, AppInfo.H);
            VividApp.BoundRT2D = null;
            GL.Viewport(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
        }

        public Texture2D GetTexture()
        {
            return BB;
        }
    }
}