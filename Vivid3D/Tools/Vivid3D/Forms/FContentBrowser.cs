using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.UI.Forms;

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
