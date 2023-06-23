using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public delegate void ActionFileSelected(string path);
    public enum RequestorType
    {
        Save,Load,Folder
    }
    public class IFileRequestor : IWindow
    {
        public ITextBox DirBox;
        public ITextBox FileBox;
        public IButton OK;
        public IButton Cancel;
        public IButton Up;
        public IList Enteries;
        public DirectoryInfo CurrentInfo;

        public static Texture2D FolderIcon
        {
            get;
            set;
        }

        public static Texture2D FileIcon
        {
            get;
            set;
        }

        public FileInfo CurrentFile;

        public event ActionFileSelected OnFileSelected;

        public Stack<string> Paths = new Stack<string>();

        public IFileRequestor(string title,RequestorType type,string path) : base(title)
        {
            if (FolderIcon == null)
            {
                FolderIcon = new Texture2D("ui/v3d/foldericon.png");
                FileIcon = new Texture2D("ui/v3d/fileicon.png");
            }

            Set(0, 0, 400, 500, "title");

            OK = new IButton().Set(5, 450, 80, 25, "") as IButton;

            if(type == RequestorType.Save)
            {
                OK.Text = "Save";
            }
            else
            {
                OK.Text = "Load";
            }

            Cancel = new IButton().Set(315, 450, 80, 25, "Cancel") as IButton;

            Up = new IButton().Set(5, 11, 20, 20, "") as IButton;
            Up.Icon = UI.Theme.ArrowUp;
            DirBox = new ITextBox().Set(30, 9, 360, 25) as ITextBox;

       

            Enteries = new IList().Set(5, 40, 390, 368) as IList;

            FileBox = new ITextBox().Set(5, 416, 390, 25) as ITextBox;

            Content.AddForms(OK, Cancel,Up,DirBox,Enteries,FileBox);

            Scan(path);

            Cancel.OnClick += (form, data) =>
            {
                UI.This.Top = null;
            };

            OK.OnClick += (form, data) =>
            {
                if (FileBox.Text.Length > 0)
                {
                    string pf = Paths.Peek();

                    if (pf.Substring(pf.Length - 1, 1) != "\\")
                    {
                        pf = pf + "\\";
                    }
                    OnFileSelected?.Invoke(pf + FileBox.Text);
                    UI.This.Top = null;
                }
                else
                {
                    if (type == RequestorType.Save)
                    {
                        IMessageBox msg = new IMessageBox("File", "You have not given a filename");
                        //UI.This.Top = msg;

                    }
                }
            
            };

            Up.OnClick += (form, data) =>
            {
                if (Paths.Count > 1)
                {
                    Paths.Pop();
                    Scan(Paths.Peek());
                }
            };

        }

        public void Scan(string path)
        {
            if (Paths.Contains(path))
            {

            }
            else
            {
                Paths.Push(path);
            }
            DirBox.Text = path;
            CurrentInfo = new DirectoryInfo(path);
            Enteries.Items.Clear();
            foreach(var dir in CurrentInfo.GetDirectories())
            {

                var item = Enteries.AddItem(dir.Name);
                item.Icon = FolderIcon;
                item.Action += (item, index, data) =>
                {
                    Scan(dir.FullName);
                };


            }
            foreach(var file in CurrentInfo.GetFiles())
            {
                var item = Enteries.AddItem(file.Name);
                item.Icon = FileIcon;
                item.Action += (item, index, data) =>
                {
                    FileBox.Text = file.Name;
                    CurrentFile = file;
                };
                item.ActionDoubleClick += (form, index, data) =>
                {
                    OK.InvokeClick(null, null);
                };
            }
        }

    }
}
