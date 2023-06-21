using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI.Forms;
using Vivid.UI;
using Vivid.Texture;
using Vivid;
using Vivid.Maths;

namespace Vivid3D.Forms
{
    public delegate void ContentClicked(FContentItem item);
    public class FContentItem : IForm
    {

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

        public DirectoryInfo DirInfo
        {
            get;
            set;
        }

        public FileInfo FileInfo
        {
            get;
            set;
        }

        public bool Over
        {
            get;
            set;
        }

        public bool Drag
        {
            get;
            set;
        }

        public event ContentClicked OnContentClicked;

        public FContentItem(DirectoryInfo info)
        {
            DirInfo = info;
            if (FolderIcon == null)
            {
                FolderIcon = new Texture2D("ui/v3d/foldericon.png");
                FileIcon = new Texture2D("ui/v3d/fileicon.png");
            }
        }

        public FContentItem(FileInfo info)
        {
            FileInfo = info;
            if (FolderIcon == null)
            {
                FolderIcon = new Texture2D("ui/v3d/foldericon.png");
                FileIcon = new Texture2D("ui/v3d/fileicon.png");
            }
        }

        public override void OnEnter()
        {
            Over = true;
        }

        public override void OnLeave()
        {
            //base.OnLeave();
            Over = false;
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            if (Drag)
            {
                Position = Position + delta;
            }
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if(button == MouseID.Left)
            {
                if (FileInfo != null)
                {
                    DragObject drag = new DragObject();
                    drag.Image = FileIcon;
                    drag.Text = FileInfo.Name;
                    drag.Path = FileInfo.FullName;
                    UI.This.BeginDrag(drag);
                
                }
               // Drag = true;
               // Root.Forms.Remove(this);
                //Root.Forms.Add(this);

            }
        }

        public override void OnDoubleClick(MouseID button)
        {
            //base.OnDoubleClick(button);
            //int a = 5;
            OnContentClicked?.Invoke(this);
        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            if(button == MouseID.Left)
            {
       //         Drag = false;
            }
        }

        public override void OnRender()
        {
            //base.OnRender();

            var img = FolderIcon;
            if (FileInfo != null)
            {
                img = FileIcon;
            }

            if (Over)
            {
                Draw(img, RenderPosition.x - 4, RenderPosition.y - 4, Size.w + 8, Size.h + 8, new Vivid.Maths.Color(6, 6, 6, 1.0f));
            }
            Draw(img);

            UI.DrawString(Text,RenderPosition.x+ 4,RenderPosition.y+ Size.h +8,UI.Theme.TextColor);
             int a = 5;
        }

    }
}
