using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vivid.UI;

namespace Vivid.App
{
    public class AppState
    {
        public string Name
        {
            get;
            set;
        }

        public Vivid.Scene.Scene StateScene
        {
            get;
            set;
        }

        public Camera StateCamera
        {
            get;
            set;
        }

        public UI.UI StateUI
        {
         
            get;
            set;

        }

        public AppState(string name)
        {

            Name = name;

        }

        public void InitState()
        {
            return;
            StateScene = new Vivid.Scene.Scene();
            StateCamera = StateScene.MainCamera;
            this.StateUI = new UI.UI();
        }

        public virtual void Init()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public virtual void Pause()
        {

        }

        public virtual void Resume()
        {

        }

        public virtual void Stop()
        {

        }

    }
}
