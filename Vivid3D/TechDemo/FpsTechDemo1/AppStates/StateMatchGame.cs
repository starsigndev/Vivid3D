using FpsTechDemo1.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Nodes;
using Vivid.Scene;

namespace FpsTechDemo1.AppStates
{
    public class StateMatchGame : AppState
    {

        public static GameMap CurrentMap = null;
        public Entity MapEntity = null;

        public StateMatchGame() : base("Match Game")
        {

        }

        public override void Init()
        {
            //    base.Init();
            StateScene = new Scene();

            FreeLook fl = new FreeLook();
            StateScene.MainCamera = fl;

            var map_item = CurrentMap.Content.Find("map.scene");

            StateScene.Load(map_item.GetStream());

            var spawn = StateScene.FindSpawn("Spawn");

            var cnode = FpsTechDemoApp.CharacterNodes[0];

            cnode.Position = spawn.Position;

            StateScene.AddNode(cnode);

            fl.Position = spawn.Position;

        }

        public override void Update()
        {
            //base.Update();
            StateScene.Update();
        }

        public override void Render()
        {
            //base.Render();
            StateScene.RenderShadows();
            StateScene.Render();
        }

    }
}
