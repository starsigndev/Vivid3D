using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Vivid.App;
using Vivid.Texture;

namespace Vivid.RenderTarget
{
    public class RenderTargetCube
    {
        public FramebufferHandle FBO;
        public RenderbufferHandle FBD;
        public TextureCube Cube;
        public int W, H;

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

        public RenderTargetCube(int w, int h)
        {
            Width = w;
            Height = h;
            Cube = new TextureCube(w, h);
            FBO = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);
            FBD = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, FBD);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.DepthComponent32f, w, h);
            // GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            //G/             L.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, FBO);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.TextureCubeMapPositiveX, Cube.Handle, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, FBD);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.TextureCubeMapPositiveX, Cube.Handle, 0);

            CheckFBO();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferHandle.Zero);
            Cube.Release(0);
        }

        private static void CheckFBO()
        {
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer failure.");
                while (true)
                {
                }
            }
        }

        public TextureTarget SetFace(int face)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);
            //SetVP.Set(0, 0, W, H);
            //GL.Viewport(0, 0, W, H);

            VividApp.BoundRTC = this;
            GL.Viewport(0, 0, Width, Height);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, (TextureTarget)(((int)TextureTarget.TextureCubeMapPositiveX) + face), Cube.Handle, 0);
            CheckFBO();
            int af = (int)TextureTarget.TextureCubeMapPositiveX + face;

            TextureTarget at = (TextureTarget)(((int)TextureTarget.TextureCubeMapPositiveX) + face);
            GL.ClearColor(1,1,1,1);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            return at;
        }

        public void Release()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferHandle.Zero);

            VividApp.BoundRTC = null;
            GL.Viewport(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
        }
    }
}