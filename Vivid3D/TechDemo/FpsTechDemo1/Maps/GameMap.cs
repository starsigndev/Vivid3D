using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Content;
using Vivid.Ini;
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

        public IniParser Ini
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
            var preview = Content.Find("preview");
            MapPreviewImage = new Texture2D(preview.GetStream(),preview.Width,preview.Height);
            var ini = Content.Find("properties");
            Ini = new IniParser(ini.GetStream());

            int b = 5;


        }



    }
}
