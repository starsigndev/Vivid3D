using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Vivid.Texture
{
    public class Texture2D
    {
        public static bool SaveAndUseCachedData = true;

        public TextureHandle Handle
        {
            get;
            set;
        }

        public int Width
        { get; set; }

        public int Height
        {
            get; set;
        }

        public string Path
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }

        public int BPP
        {
            get;
            set;
        }

        

        public static Dictionary<string, Texture2D> Cache = new Dictionary<string, Texture2D>();

        public Texture2D(int width, int height)
        {
            Console.WriteLine("Creating tex W/H");
            Width = width;
            Height = height;
            // Raw = new byte[Width * Height * 4];
            var Format = InternalFormat.Rgba8;
            Handle = GL.CreateTexture(TextureTarget.Texture2d);
            GL.TextureStorage2D(Handle, 1, SizedInternalFormat.Rgba32f, Width, Height);

            GL.TextureParameteri(Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }

        public Texture2D(string path)
        {
            if (Cache.ContainsKey(path))
            {
                var tex = Cache[path];
                Handle = tex.Handle;
                Width = tex.Width;
                Height = tex.Height;
                BPP = tex.BPP;
                Path = tex.Path;
                Data = tex.Data;
                return;
            }

            if (File.Exists(path + ".data"))
            {
                FileStream fs = new FileStream(path + ".data", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                Width = r.ReadInt32();
                Height = r.ReadInt32();
                BPP = r.ReadInt32();
                Data = r.ReadBytes(Width * Height * BPP);
                Path = path;
            }
            else
            {
                Bitmap bit = new Bitmap(path);

                Data = new byte[bit.Width * bit.Height * 4];

                for (int y = 0; y < bit.Height; y++)
                {
                    for (int x = 0; x < bit.Width; x++)
                    {
                        var pixel = bit.GetPixel(x, y);
                        int loc = (y * bit.Width * 4) + x * 4;
                        Data[loc++] = pixel.R;
                        Data[loc++] = pixel.G;
                        Data[loc++] = pixel.B;
                        Data[loc++] = pixel.A;
                    }
                }

                Width = bit.Width;
                Height = bit.Height;

                BPP = 4;

                FileStream fs = new FileStream(path + ".data", FileMode.Create, FileAccess.Write);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(Width);
                w.Write(Height);
                w.Write(BPP);

                w.Write(Data);

                w.Flush();
                fs.Flush();
                w.Close();
                fs.Close();
            }

            Path = path;

            GL.Enable(EnableCap.Texture2d);
            Handle = GL.CreateTexture(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, Handle);

            CheckHandle();

            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Data);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear) ;
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, TextureHandle.Zero);
            Cache.Add(path, this);
        }

        private void CheckHandle()
        {
            if (Handle == TextureHandle.Zero)
            {
                Console.WriteLine("Invalid texture handle.");
                Environment.Exit(0);
            }
        }

        public void Delete()
        {
            GL.DeleteTexture(Handle);
        }

        public void Bind(int unit)
        {
            GL.ActiveTexture(((TextureUnit)((int)TextureUnit.Texture0 + unit)));
            GL.BindTexture(TextureTarget.Texture2d, Handle);
        }

        public void Unbind(int unit)
        {
            GL.ActiveTexture(((TextureUnit)((int)TextureUnit.Texture0 + unit)));
            GL.BindTexture(TextureTarget.Texture2d, TextureHandle.Zero);
        }

        public Texture2D(IntPtr handle)
        {
            // CObj = handle;
        }

        public Texture2D(MemoryStream stream, int w, int h, string path = "")
        {
            if (path == string.Empty)
            {
            }
            else
            {
                //Console.WriteLine("=================Path:" + path);
                if (Cache.ContainsKey(path))
                {
                    // CObj = Cache[path].CObj;
                    Width = Cache[path].Width;
                    Height = Cache[path].Height;
                    Path = path;
                    return;
                    //return Cache[path];
                }
            }
            byte[] buf = new byte[stream.Length];
            stream.Read(buf, 0, (int)stream.Length);
            //    CObj = GemBridge.gem_CreateTexture2D(buf, (int)stream.Length, w, h);
            Width = w;
            Height = h;
            Data = buf;
            Path = path;
            if (path == string.Empty)
            {
                int b = 5;
                Console.WriteLine("!!");
            }
            else
            {
                Cache.Add(path, this);
            }

            GL.Enable(EnableCap.Texture2d);
            Handle = GL.CreateTexture(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, Handle);

            CheckHandle();

            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Data);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

          //  GL.GenerateMipmap(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, TextureHandle.Zero);

        }

        //public static Dictionary<string, Texture2D> Cache = new Dictionary<string, Texture2D>();
        public void Copybuffer(int x,int y)
        {
            Bind(0);
            GL.CopyTextureSubImage2D(Handle, 0, 0, 0, x, y, Width, Height);
            Unbind(0);
        }
    }
}