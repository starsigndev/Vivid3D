using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.App
{
    public class AppMetrics
    {

        public int WindowWidth
        {
            get;
            set;
        }

        public int WindowHeight
        {
            get;
            set;
        }

        public string WindowTitle
        {
            get;
            set;
        }

        public bool FullScreen
        {
            get;
            set;
        }

        public AppMetrics(int windowWidth, int windowHeight, string windowTitle, bool fullScreen)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            WindowTitle = windowTitle;
            FullScreen = fullScreen;
        }
        public AppMetrics()
        {

        }
    }
}
