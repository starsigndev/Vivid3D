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


        public FContentBrowser() : base("Content")
        {

            IconSize = 64;
            Display = new IScrollArea();
            Content.AddForm(Display);
            Paths = new Stack<string>();
            Dirs = new List<DirectoryInfo>();
            Files = new List<FileInfo>();
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
                folder.Set(fx, fy,IconSize,IconSize, dir.Name);
                Display.AddForm(folder);
                fx = fx + IconSize * 2;
                if (fx >= Display.Size.w - IconSize)
                {
                    fx = 10;
                    fy = fy + IconSize * 2;
                }
                folder.OnContentClicked += Folder_OnContentClicked;

            }
            foreach(var file in new DirectoryInfo(path).GetFiles())
            {
                Files.Add(file);
                var fileitem = new FContentItem(file);
                fileitem.Set(fx, fy, IconSize, IconSize, file.Name);
                Display.AddForm(fileitem);
                fx = fx + IconSize * 2;
                if (fx >= Display.Size.w - IconSize)
                {
                    fx = 10;
                    fy = fy + IconSize * 2;
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
                    SceneIO io = new SceneIO();
                    var scene = io.LoadScene(item.FileInfo.FullName);
                    Editor.SetScene(scene);
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
            Display.Set(0, 0, Content.Size.w, Content.Size.h, "");
        }


    }
}
