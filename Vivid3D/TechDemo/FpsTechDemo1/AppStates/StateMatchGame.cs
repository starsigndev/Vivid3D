using FpsTechDemo1.Maps;
using FpsTechDemo1.Nodes;
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

            var cnode = FpsTechDemoApp.SpawnChar("exoMan");//   CharacterNodes[0].Spawn();// as CharacterNode;
            var cnode2 = FpsTechDemoApp.SpawnChar("exoMan");


            cnode.Position = spawn.Position;
   //         cnode2.Position = spawn.Position;
            StateScene.AddNode(cnode);


            //StateScene.AddNode(cnode2);
           // cnode2.PlayAnimation("Walk");


            //cnode.Position = spawn.Position;

            //Link nodes to bone transforms, such as camera to head.


            fl.Position = spawn.Position;
            VividApp.CurrentScene = StateScene;
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
            StateScene.RenderLines();
        }

    }
}
