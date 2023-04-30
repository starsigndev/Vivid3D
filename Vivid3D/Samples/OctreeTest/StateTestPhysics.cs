using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.App;
using Vivid.Content;
using Vivid.Importing;
using Vivid.Nodes;
using Vivid.Scene;

namespace OctreeTest
{
    public class StateTestPhysics : AppState
    {
        public StateTestPhysics() : base("Physics Test")
        {

        }

        Scene s1;

        public override void Init()
        {
            base.Init();

            Content textures = new Content("c:/content/content/textures");
            Content scenes = new Content("c:/content/content/scenes");

            var sceneItem = scenes.Find("physics2");

            s1 = new Scene();

            s1.Load(sceneItem.GetStream());

            var fl = new FreeLook();
            s1.MainCamera = fl;
            fl.Position = new OpenTK.Mathematics.Vector3(0, 5, 5);


        }
        bool px = false;

        public override void Update()
        {
            //base.Update();
            s1.Update();
            if (!px)
            {
                if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Q))
                {
                    s1.BeginPhysics();
                }
            }
        }

        public override void Render()
        {
            //base.Render();
            s1.RenderShadows();
            s1.Render();
        }
    }

}