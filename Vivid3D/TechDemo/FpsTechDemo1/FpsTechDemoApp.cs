using FpsTechDemo1.Maps;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Content;
using Vivid.Importing;
using FpsTechDemo1.Nodes;
using Assimp;
using Assimp.Configs;
using Vivid.Scene;

namespace FpsTechDemo1
{
    public class FpsTechDemoApp : VividApp
    {
        public static string AnimsPath = "";
        public static string MapsPath = "";
        public static string CharPath = "";
        public static List<string> Maps = new List<string>();
        public static string ContentPath = "";
        public static Content UIContent;
        public static Content ImagesContent;
        public List<Animation> CharAnimations = new List<Animation>();
        public static List<GameMap> GameMaps = new List<GameMap>();
        public static List<CharacterNode> CharacterNodes = new List<CharacterNode>();
        public FpsTechDemoApp(GameWindowSettings game_window,NativeWindowSettings native_window) : base(game_window, native_window)
        {
            CharPath = "c:\\fpscontent\\characters\\";
            ContentPath = "C:\\fpsContent\\content\\";
            MapsPath = "C:\\fpsContent\\content\\maps";
            AnimsPath = "C:\\fpscontent\\animations\\";
            ScanMaps();
            Console.WriteLine("Found " + Maps.Count + " maps.");
            foreach(var map in Maps)
            {
                Console.WriteLine("Map Name:" + map);
            }
            int b = 5;
           // ScanAnimations();
            ScanChars();
            int a = 5;
        }
        
        public void ScanAnimations(CharacterNode node)
        {
            Console.WriteLine("Scanning animations.");
            foreach(var anim in new DirectoryInfo(AnimsPath).GetFiles())
            {

                Console.WriteLine("Found Animation:" + anim.Name);
                LoadAnim(node,anim.FullName);

            }

            int b = 5;
        }

        public void LoadAnim(CharacterNode node,string path)
        {

            Importer.ImportAnimation(node as SkeletalEntity,path);


        }

        public void ScanChars()
        {
            Console.WriteLine("Scanning characters.");
            foreach(var chr in new DirectoryInfo(CharPath).GetDirectories())
            {
                Console.WriteLine("Character Found:" + chr.Name);
                var node = LoadChar(chr.FullName);
                node.Scale = new OpenTK.Mathematics.Vector3(0.01f, 0.01f, 0.01f);
                ScanAnimations(node);
                CharacterNodes.Add(node);
                node.Animator.LinkAnimation(0, "Walk");
                node.Animator.LinkAnimation(1, "StrafeLeft");
                node.Animator.LinkAnimation(2, "StrafeRight");
                node.Animator.LinkAnimation(3, "Idle");
                node.PlayAnimation("StrafeRight");



            }
            int b = 5;
        }
        public CharacterNode LoadChar(string path)
        {
            path = path + "\\";
            var node = Importer.ImportSkeletalEntity<CharacterNode>(path + "char.fbx");
            return node;
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
