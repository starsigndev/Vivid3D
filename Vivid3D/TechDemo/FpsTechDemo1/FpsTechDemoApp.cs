using FpsTechDemo1.Maps;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Content;

namespace FpsTechDemo1
{
    public class FpsTechDemoApp : VividApp
    {
        public static string MapsPath = "";
        public static List<string> Maps = new List<string>();
        public static string ContentPath = "";
        public static Content UIContent;
        public static Content ImagesContent;
        public static List<GameMap> GameMaps = new List<GameMap>();
        public FpsTechDemoApp(GameWindowSettings game_window,NativeWindowSettings native_window) : base(game_window, native_window)
        {
            ContentPath = "C:\\fpsContent\\content\\";
            MapsPath = "C:\\fpsContent\\content\\maps";
            ScanMaps();
            Console.WriteLine("Found " + Maps.Count + " maps.");
            foreach(var map in Maps)
            {
                Console.WriteLine("Map Name:" + map);
            }
            int b = 5;
        }
        
        public void ScanMaps()
        {
            DirectoryInfo map_Info = new DirectoryInfo(MapsPath);
            foreach(var file in map_Info.GetFiles())
            {

                var ext = Path.GetExtension(file.FullName);
                if (ext == ".content")
                {
                    Maps.Add(file.FullName);
                    GameMap map = new GameMap(file.FullName.Replace(".content",""));
                    GameMaps.Add(map);
                }

            }
        }

        public override void Init()
        {

            UIContent = new Content(ContentPath + "ui");
            ImagesContent = new Content(ContentPath + "images");
            int b = 5;
        }

        public override void Update()
        {
//            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

    }
}
