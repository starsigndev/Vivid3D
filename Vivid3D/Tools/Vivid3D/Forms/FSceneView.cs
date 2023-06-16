using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.State;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FSceneView : IWindow
    {


        public FSceneRT SceneRT;

        public FSceneView() : base("Scene Editor")
        {
            //Set()
            SceneRT = new FSceneRT();
            Content.AddForm(SceneRT);
            Content.Scissor = false;

            

        }

       

        public override void AfterSet()
        {
            base.AfterSet();
            SceneRT.Set(0,0,Content.Size.w, Content.Size.h);
        }


    }
}
