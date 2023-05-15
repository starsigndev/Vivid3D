using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Acceleration.Octree;
using Vivid.PostProcesses;
using Vivid.State;

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

        public bool LightShaftsOn
        {
            get;
            set;
        }

        private PPBloom bloom;
        private PPLightShafts lightShafts;

        public Renderer(Vivid.Scene.Scene scene)
        {
            BloomOn = false;
            CurrentScene = scene;
            bloom = new PPBloom(scene);
            lightShafts = new PPLightShafts(scene);
            int a = 5;
          
        }

        public void Render()
        {

            if (BloomOn)
            {
                CurrentScene.RenderShadows();
                bloom.ProcessAndDraw();
            }
            else if (LightShaftsOn)
            {
                lightShafts.ProcessAndDraw();
            }
            else
            {

                    CurrentScene.RenderShadows();
                      CurrentScene.Render();
                //GLState.State = CurrentGLState.Draw;



            }

        }

    }
}
