using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public enum DockArea
    {
        Left,Right,Top,Bottom,Centre
    }

    public class DockSize
    {
        public int W, H;
    }
    public class IWindowDock : IForm
    {



        public IWindowDock()
        {
          
        }

        public override void AfterSet()
        {
           

          

        }

        public override void OnRender()
        {
            Draw(UI.Theme.Pure, -1, -1, -1, -1, new Maths.Color(0.5f, 0.5f, 0.5f, 1.0f));
        }

        public void DockWindow(IWindow window, DockArea area)
        {
        }

    }
}
