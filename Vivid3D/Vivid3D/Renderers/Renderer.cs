using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Acceleration.Octree;
using Vivid.PostProcesses;

namespace Vivid.Renderers
{
    public class Renderer
    {

        public Scene.Scene CurrentScene
        {
            get;
            set;
        }

        public ASOctree CurrentOcScene
        {
            get;
            set;
        }

        //Specific features.
        public bool BloomOn
        {
            get;
            set;
        }

        private PPBloom bloom;

        public Renderer(Vivid.Scene.Scene scene)
        {
            BloomOn = false;
            CurrentScene = scene;
            bloom = new PPBloom(scene);
            int a = 5;
          
        }

        public void Render()
        {

            if (BloomOn)
            {
                CurrentScene.RenderShadows();
                bloom.Process();
            }
            else
            {
                CurrentScene.RenderShadows();
                CurrentScene.Render();
            }


        }

    }
}
