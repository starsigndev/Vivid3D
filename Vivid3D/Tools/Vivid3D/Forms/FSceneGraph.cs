using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public override void AfterSet()
        {
            base.AfterSet();
            SceneTree.Set(0, TitleHeight, Size.w-4, Size.h-TitleHeight);
        }

    }
}
