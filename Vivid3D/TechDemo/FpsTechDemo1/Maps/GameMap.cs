using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Content;
using Vivid.Texture;

namespace FpsTechDemo1.Maps
{
    public class GameMap
    {

        public Texture2D MapPreviewImage
        {
            get;
            set;
        }

        public string MapName
        {
            get;
            set;
        }

        public string MapAuthor
        {
            get;
            set;
        }

        public string MapInfo
        {
            get;
            set;
        }

        public string MapPath
        {
            get;
            set;
        }

        public Content Content
        {
            get;
            set;
        }

        public GameMap()
        {

        }
        public GameMap(string path)
        {

            MapPath = path;
            Content = new Content(path);

            var info = Content.Find("info").GetStream();
            TextReader r = new StreamReader(info);
            MapInfo = r.ReadToEnd();

            int b = 5;


        }



    }
}
