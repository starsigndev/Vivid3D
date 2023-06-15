using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Vivid.Texture
{
    public class TextureDepth
    {
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

        public byte[] Data
        {
            get;
            set;
        }

        public TextureHandle Handle
        {
            get;
            set;
        }

        public TextureDepth(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new byte[Width * Height];
            var Format = InternalFormat.Rgb;
            Handle = GL.CreateTexture(TextureTarget.Texture2d);
            GL.TextureStorage2D(Handle, 1, SizedInternalFormat.DepthComponent32, Width, Height);

            GL.TextureParameteri(Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }
        public void Delete()
        {
            GL.DeleteTexture(Handle);
        }
    }
}