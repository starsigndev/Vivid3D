using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.IO;
using Vivid.Scene;
using Vivid.UI.Forms;
using Vivid3D.Windows;

namespace Vivid3D.Forms
{
    public class FContentBrowser : IWindow
    {
        public IScrollArea Display
        {
            get;
            set;
        }

        public Stack<string> Paths
        {
            get;
            set;
        }

        public List<DirectoryInfo> Dirs
        {
            get;
            set;
        }

        public List<FileInfo> Files
        {
            get;
            set;
                
        }

        private int IconSize
        {
            get;
            set;
        }

        public IHorizontalSlider Zoomer
        {
            get;set;
        }

        public float ContentZoom
        {
            get;
            set;
        }
        public IToolBar Tools
        {
            get;
            set;
        }

        public IButton Back
        {
            get;
            set;
        }

        public FContentBrowser() : base("Content")
        {

            ContentZoom = 0.5f;
            IconSize = 64;
            Display = new IScrollArea();
            Content.AddForm(Display);
            Paths = new Stack<string>();
            Dirs = new List<DirectoryInfo>();
            Files = new List<FileInfo>();

            Zoomer = new IHorizontalSlider().Set(5, 10, 128, 8) as IHorizontalSlider;
            Back = new IButton().Set(0, 0, 0, 0) as IButton;
            Back.Icon = Vivid.UI.UI.Theme.ArrowUp;
            Tools = new IToolBar();
            Content.AddForm(Tools);

            //Content.AddForm(Zoomer);
            Tools.AddTool(Back);
            Back.Size = new Vivid.Maths.Size(24, 20);
            Tools.AddTool(Zoomer);
            Zoomer.Position = new Vivid.Maths.Position(Zoomer.Position.x, Zoomer.Position.y + 4);

            Back.OnClick += (form, data) =>
            {
                if (Paths.Count > 1)
                {
                    Paths.Pop();
                    ScanPath(Paths.Peek());
                }
            };


            Zoomer.OnSliderChanged += (val) =>
            {
                ContentZoom = 0.5f + Zoomer.Value;
                ScanPath(Paths.Peek());
            };

            Display.OnDrop += (form, data) =>
            {

                if (data.Object != null){
                    SceneIO io = new SceneIO();
                    Node obj = (Node)data.Object;
                    io.SaveNode(Paths.Peek() + "\\" + obj.Name + ".node", obj);
                }
            };
            Display.OnClick += (s, e) =>
            {
                if ((MouseID)e == MouseID.Back)
                {
                    //Environment.Exit(1);
                    if (Paths.Count > 1)
                    {
                        Paths.Pop();
                        ScanPath(Paths.Peek());
                    }
                }
            };

            IVerticalMenu c_menu = new IVerticalMenu();

            Display.ContextForm = c_menu;

            var cr = c_menu.AddItem("Create");

            var cr_script = cr.AddItem("C# Script");

            cr_script.Click += (item) =>
            {

                var new_file = new WNewFile();
                Vivid3DApp.MainUI.Top = new_file;
                new_file.OnFileNamePicked += (name) =>
                {

                    string class_name = "";
                    string ext = Path.GetExtension(name);
                    if(ext=="")
                    {
                        class_name = name;
                        name = name + ".cs";
                    }
                    else
                    {
                        class_name = Path.GetFileNameWithoutExtension(name);
                    }

                    string[] code = File.ReadAllLines("template/NewCSScript.cs");
                    string[] new_code = new string[code.Length];

                    int ln = 0;
                    foreach(var line in code.ToArray())
                    {
                        new_code[ln] = line.Replace("_SCRIPT_NAME_", class_name);
                        ln++;
                    }

                    File.WriteAllLines(Paths.Peek()+"\\"+name, new_code);

                    int b = 5;
                    //if (Path.GetExtension(name))
                   //{

                    

                };



            };


        }
        
        public void ScanPath(string path)
        {
            if (!Paths.Contains(path)){
                Paths.Push(path);
            }
            Dirs.Clear();
            Files.Clear();
            Display.ClearForms();

            int fx, fy;

            fx = 10;
            fy = 20;

            foreach(var dir in new DirectoryInfo(path).GetDirectories())
            {
                Dirs.Add(dir);
                var folder = new FContentItem(dir);
                folder.Set(fx, fy,(int)((float)IconSize*ContentZoom),(int)((float)IconSize*ContentZoom), dir.Name);
                Display.AddForm(folder);
                fx = fx + (int)((float)(IconSize * 2) * ContentZoom);
                if (fx >= Display.Size.w - IconSize)
                {
                    fx = 10;
                    fy = fy + (int)((float)(IconSize * 2) * ContentZoom);
                }
                folder.OnContentClicked += Folder_OnContentClicked;

            }
            foreach(var file in new DirectoryInfo(path).GetFiles())
            {
                Files.Add(file);
                var fileitem = new FContentItem(file);
                fileitem.Set(fx, fy, (int)((float)IconSize * ContentZoom), (int)((float)IconSize * ContentZoom), file.Name);
                Display.AddForm(fileitem);
                fx = fx + (int)((float)(IconSize * 2) * ContentZoom);
                if (fx >= Display.Size.w - IconSize)
                {
                    fx = 10;
                    fy = fy + (int)((float)(IconSize * 2) * ContentZoom);
                }
                fileitem.OnContentClicked += Fileitem_OnContentClicked;

            }
        }

        private void Fileitem_OnContentClicked(FContentItem item)
        {

            switch (item.FileInfo.Extension)
            {
                case ".fbx":
                case ".obj":
                case ".dae":
                    var node = Vivid.Importing.Importer.ImportEntity<Entity>(item.FileInfo.FullName);
                    Editor.CurrentScene.AddNode(node);
                    Editor.UpdateSceneGraph();
                    break;
                case ".scene":
                    Editor.Stop();
                    SceneIO io = new SceneIO();
                    var scene = io.LoadScene(item.FileInfo.FullName);
                    Editor.SetScene(scene);
                    FConsoleOutput.LogMessage("Loaded scene from:" + item.FileInfo.FullName);
                    break;
                case ".node":
                    Editor.Stop();
                    SceneIO io2 = new SceneIO();
                    var node2 = io2.LoadNode(item.FileInfo.FullName);
                    Editor.CurrentScene.AddNode(node2);
                    Editor.UpdateSceneGraph(); 

                    break;
            }
           

        }

        private void Folder_OnContentClicked(FContentItem item)
        {
            //throw new NotImplementedException();
            ScanPath(item.DirInfo.FullName);
        }

      

        public override void AfterSet()
        {
            base.AfterSet();
            Display.Set(0, 32, Content.Size.w, Content.Size.h-32, "");
            Tools.Set(0, 0, Content.Size.w, 32, "");
        }


    }
}
