using SixLabors.ImageSharp.Formats.Tga;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;
using PhysX;
using System.Runtime.InteropServices;

namespace Vivid.Content
{
    public class Content
    {
        public static List<Content> ActiveContents = new List<Content>();
        public string Name
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public List<ContentItem> Items = new List<ContentItem>();

        public bool CompressImages
        {
            get;set;
        }
        public static ContentItem GlobalFindItem(string name)
        {

            foreach(var con in ActiveContents)
            {
                var res = con.Find(name);
                if (res != null) return res;

            }

            return null;
        }

        public Content(string path)
        {
            ReadIndex(path);
            ActiveContents.Add(this);
            CompressImages = true;
        }

        public Content()
        {

        }

        public void AddFile(FileInfo file)
        {

            Items.Add(new ContentItem(file.FullName, file.Name,file.FullName));

        }

        public void AddContentToStream(ContentItem file,MemoryStream stream)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            fs.CopyTo(stream);
            fs.Close();
        }
        public void AddImageToStream(MemoryStream img,MemoryStream stream)
        {
            img.CopyTo(stream);
        }
        public string GetFileExtension(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            if (!string.IsNullOrEmpty(extension))
            {
                return extension.Substring(1);
            }

            return string.Empty;
        }
        public static MemoryStream GetImageStream(string imagePath)
        {
            var img = new System.DrawingCore.Bitmap(imagePath);
            var data = img.LockBits(new System.DrawingCore.Rectangle(0, 0, img.Width, img.Height), System.DrawingCore.Imaging.ImageLockMode.ReadOnly, System.DrawingCore.Imaging.PixelFormat.Format32bppArgb);
            
            int w = img.Width;

            int h = img.Height;
            MemoryStream res = new MemoryStream(w * h * 4);
            
            res.Position = 0;
            for(int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    int i = y * data.Stride + x * 4;
                    byte r = Marshal.ReadByte(data.Scan0, i + 2);
                    byte g = Marshal.ReadByte(data.Scan0, i + 1);
                    byte b = Marshal.ReadByte(data.Scan0, i);
                    byte a = Marshal.ReadByte(data.Scan0, i + 3);
                    //var pix = img.GetPixel(x, y);
                    res.WriteByte(r);
                    res.WriteByte(g);
                    res.WriteByte(b);
                    res.WriteByte(a);


                }
            }
            img.UnlockBits(data);
            res.Position = 0;
            return res;
        }
        public static System.Drawing.Size GetImageDimensions(string imagePath)
        {
            var img = new System.DrawingCore.Bitmap(imagePath);
            return new System.Drawing.Size(img.Width,img.Height);
        }
        public void Build(string output)
        {

            
           

            long offset = 0;
            long size = 0;

            foreach (var file in Items)
            {

                MemoryStream stream = new MemoryStream();

                string ext = GetFileExtension(file.FullName);
                switch (ext)
                {
                    case "jpg":
                    case "png":
                    case "bmp":
                        {
                            var image_size = GetImageDimensions(file.FullName);
                            file.Width = image_size.Width;
                            file.Height = image_size.Height;
                            file.Type = ContentType.Texture2D;
                            file.ContentStart = offset;
                            var img_data = GetImageStream(file.FullName);

                            img_data.Position = 0;
                        

                            var compressed = ZlibStream.CompressBuffer(img_data.ToArray());


                            if (CompressImages)
                            {

                                file.Compressed = false;
                                //stream.CopyTo
                                img_data.CopyTo(stream);
                                offset += img_data.Length;
                                file.ContentLength = img_data.Length;
                            }
                            else
                            {
                                file.Compressed = true;
                                stream.Write(compressed);
                                offset += compressed.LongLength;
                                file.ContentLength = compressed.Length;
                            }


                        }
                        break;
                    default:
                        {

                            byte[] LoadData(ContentItem file)
                            {
                                return File.ReadAllBytes(file.FullName);
                            }

                            file.ContentStart = offset;

                            byte[] data = LoadData(file);


                            var compressed = ZlibStream.CompressBuffer(data);

                            if (compressed.Length == 0)
                            {

                                file.Compressed = false;
                                stream.Write(data);
                                offset += data.LongLength;
                                file.ContentLength = data.LongLength;
                            }
                            else
                            {
                                file.Compressed = true;
                                stream.Write(compressed);
                                offset += compressed.LongLength;
                                file.ContentLength = compressed.Length;
                            }

                            

                            int b = 5;
                            
                            
                            /*
                            file.ContentStart = offset;
                            AddContentToStream(file, stream);
                            long poffset = offset;
                            offset = stream.Position;
                            size = offset - poffset;
                            /file.ContentLength = size;
                            */
                        }
                            break;
                }
                int a = 5;
                stream.Position = 0;
                file.SaveStream = stream;


                Console.WriteLine("File:" + file.LocalName + " Pos:" + file.ContentStart + " Len:" + file.ContentLength);
            }


            WriteIndex(output+".index");
            WriteContent(output + ".content");


        }
        public void ReadIndex(string file)
        {
            FileStream fs = new FileStream(file + ".index", FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            long num = r.ReadInt64();

            for(long i = 0; i < num; i++)
            {

                string local = r.ReadString();
                string dotted = r.ReadString();
                string full = r.ReadString();
                long index_Num = r.ReadInt64();
                long data_start = r.ReadInt64();
                long data_len = r.ReadInt64();
                bool data_com = r.ReadBoolean();
                int data_type = r.ReadInt32();
                ContentItem item = new ContentItem();
                item.LocalName = local;
                item.DottedName = dotted;
                item.FullName = full;
                item.IndexNumber = index_Num;
                item.ContentStart = data_start;
                item.ContentLength = data_len;
                item.Type = (ContentType)data_type;
                item.Compressed = data_com;
                if(item.Type == ContentType.Texture2D)
                {
                    item.Width = r.ReadInt32();
                    item.Height = r.ReadInt32();
                }
                item.ContentFile = file + ".content";
                Items.Add(item);
            }

            fs.Close();
        }
        public void WriteIndex(string file)
        {
            FileStream fs = new FileStream(file,FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            w.Write((long)Items.Count);
            for(int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                w.Write(item.LocalName);
                w.Write(item.DottedName);
                w.Write(item.FullName);
                w.Write(item.IndexNumber);
                w.Write(item.ContentStart);
                w.Write(item.ContentLength);
                w.Write(item.Compressed);
                
                w.Write((int)item.Type);
                if(item.Type == ContentType.Texture2D)
                {
                    w.Write(item.Width);
                    w.Write(item.Height);
                }
            }
            
            fs.Flush();
            fs.Close();
        }
        public void WriteContent(string output)
        {

          
            FileStream fs = new FileStream(output, FileMode.Create, FileAccess.Write);
          //  BinaryWriter w = new BinaryWriter(fs);

            foreach(var file in Items)
            {

                file.SaveStream.CopyTo(fs);

                //byte[] data = file.SaveStream.

                //fs.Write(file.SaveStream.Read())

            }

            fs.Flush();
            fs.Close();
        }

        public ContentItem Find(string name)
        {

            foreach(var item in Items)
            {

                if (item.DottedName.ToLower().Contains(name.ToLower()))
                {
                    return item;
                }

            }
            return null;

        }

    }
}
