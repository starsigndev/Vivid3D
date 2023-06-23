using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.IO;
using Vivid.Scene;
using Vivid.UI.Forms;
namespace Vivid3D.Forms
{
    public class FSceneGraph : IWindow
    {
        public ITreeView SceneTree
        {
            get;
            set;
        }
        public FSceneGraph() : base("Scene")
        {
            SceneTree = new ITreeView();
            AddForm(SceneTree);
            Editor.SceneTree = SceneTree;
            Editor.UpdateSceneGraph();
            SceneTree.OnDrop += (form, data) =>
            {
                if (Path.GetExtension(data.Path) == ".fbx")
                {

                    var node = Vivid.Importing.Importer.ImportEntity<Entity>(data.Path);
                    Editor.CurrentScene.AddNode(node);
                    Editor.UpdateSceneGraph();

                }else if(Path.GetExtension(data.Path)==".node")
                {
                    Editor.Stop();
                    SceneIO io2 = new SceneIO();
                    var node2 = io2.LoadNode(data.Path);
                    Editor.CurrentScene.AddNode(node2);
                    Editor.UpdateSceneGraph();
                }
            };
        }
        public override void AfterSet()
        {
            base.AfterSet();
            SceneTree.Set(0, TitleHeight, Size.w-4, Size.h-TitleHeight);
        }

    }
}
