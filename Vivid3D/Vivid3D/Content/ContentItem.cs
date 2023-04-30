namespace Vivid.Content
{
    public enum ContentType
    {
        File, VirtualFolder, SubStream, Texture2D
    }

    public class ContentItem
    {
        public MemoryStream SaveStream
        {
            get;
            set;
        }

        public string LocalName
        {
            get;
            set;
        }

        public string DottedName
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public long IndexNumber
        {
            get;
            set;
        }

        public long ContentStart
        {
            get;
            set;
        }

        public long ContentLength
        {
            get;
            set;
        }

        public long OriginalLength
        {
            get;
            set;
        }

        public ContentType Type
        {
            get;
            set;
        }

        public ContentItem()
        {
            DataStream = null;
            InMemory = false;
            Compressed = false;
        }

        public string ContentFile
        {
            get;
            set;
        }

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

        public bool Compressed
        {
            get;
            set;
        }

        public void Load()
        {
            FileStream fs = new FileStream(ContentFile, FileMode.Open, FileAccess.Read);
            MemoryStream ms = new MemoryStream((int)ContentLength);

            fs.Position = ContentStart;

            byte[] data = new byte[ContentLength];

            fs.Read(data, 0, (int)ContentLength);

            /*
            byte[] rbuf = new byte[ContentLength];
            fs.Read(rbuf, 0, (int)ContentLength);
            ms.Write(rbuf, 0, (int)ContentLength);
            fs.Close();
            ms.Position = 0;
            DataStream = ms;
            InMemory = true;

            DataStream.Position = 0;
            */

            if (Compressed)
            {
                data = Ionic.Zlib.ZlibStream.UncompressBuffer(data);
            }

            DataStream = ms;
            DataStream.Write(data, 0, data.Length);
            InMemory = true;
        }

        public MemoryStream GetStream()
        {
            if (!InMemory)
            {
                Load();
            }
            DataStream.Position = 0;
            return DataStream;
        }

        public MemoryStream DataStream
        {
            get;
            set;
        }

        public bool InMemory
        {
            get;
            set;
        }

        public ContentItem(string longName, string shortName, string full)
        {
            LocalName = shortName;
            DottedName = full.Replace("C:\\", "");
            DottedName = DottedName.Replace("C:/", "");
            DottedName = DottedName.Replace("\\", ".");
            DottedName = DottedName.Replace("/", ".");
            FullName = full;
        }

        public override string ToString()
        {
            return Type + ":  " + DottedName + " Index:" + IndexNumber + "Pos:" + ContentStart + " Size:" + ContentLength;
            //return base.ToString();
        }
    }
}