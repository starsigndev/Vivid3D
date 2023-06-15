using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FMainWindow : IWindow
    {

        public FSceneGraph SceneGraph
        {
            get;
            set;
        }

        public FSceneView SceneView
        {
            get;
            set;
        }

        public FMainWindow() : base("Vivid3D",false,false)
        {

            Editor.NewScene();
            SceneGraph = new FSceneGraph();
            SceneView = new FSceneView();
            Vivid3DApp.MainUI.AddWindow(this);
            WindowDock = true;


        }

        public override void AfterSet()
        {
            base.AfterSet();

            SceneGraph.Set(80, 80, 300, 400, "Scene Graph");
            SceneView.Set(80, 80, 300, 400, "Scene Editor");
            Vivid3DApp.MainUI.AddWindow(SceneGraph);
            Vivid3D.Vivid3DApp.MainUI.AddWindow(SceneView);
            Dock(SceneGraph, DockPosition.Left);
            Dock(SceneView, DockPosition.Centre);
        }

        public void Dock(IWindow win,DockPosition position)
        {

            Point pp = new Point(0, 0);

            switch (position)
            {
                case DockPosition.Left:

                    pp = new Point(64, Size.h / 2);

                    break;
                case DockPosition.Bottom:

                    break;
                case DockPosition.Centre:

                    pp = new Point(Size.w / 2, Size.h / 2);
                    break;
            }

            DockWindow(win, pp, 320,320);

        }


    }
}
