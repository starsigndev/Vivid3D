using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;


namespace Vivid.Texture
{
    public class TextureCube
    {
        public byte[] px, py, pz, nx, ny, nz;
            
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

        public TextureHandle Handle
        {
            get;
            set;
        }

        public TextureCube(int w, int h)
        {
            Width = w;
            Height = h;

            GL.ActiveTexture(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            GenMap();
            for (int i = 0; i < 6; i++)
            {
                //TextureTarget.
                GL.TexImage2D((TextureTarget)((int)(TextureTarget.TextureCubeMapPositiveX) + i), 0, InternalFormat.Rgb32f, w, h, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            }
        }



        private void GenMap()
        {
            GL.Enable(EnableCap.TextureCubeMap);
            Handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, Handle);

            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
        }

        public void Bind(int unit)
        {

            uint t_unit = (uint)unit;

            OpenTK.Graphics.OpenGL.TextureUnit texu = OpenTK.Graphics.OpenGL.TextureUnit.Texture0;

            int oid = (int)texu + (int)unit;

            texu = (OpenTK.Graphics.OpenGL.TextureUnit)oid;

            GL.ActiveTexture(texu);
            GL.Enable(EnableCap.TextureCubeMap);
            GL.BindTexture(TextureTarget.TextureCubeMap, Handle);
        }

        public void Release(int unit)
        {
            uint t_unit = (uint)unit;

            OpenTK.Graphics.OpenGL.TextureUnit texu = OpenTK.Graphics.OpenGL.TextureUnit.Texture0;

            int oid = (int)texu + (int)unit;

            texu = (OpenTK.Graphics.OpenGL.TextureUnit)oid;

            GL.ActiveTexture(texu);
            GL.Disable(EnableCap.TextureCubeMap);
            GL.BindTexture(TextureTarget.TextureCubeMap, TextureHandle.Zero);
        }


    }
}
