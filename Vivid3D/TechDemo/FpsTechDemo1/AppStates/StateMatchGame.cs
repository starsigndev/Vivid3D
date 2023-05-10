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
using Vivid.UI.Forms;
using Vivid.Maths;
using Vivid.Renderers;

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


        //    cnode.Position = spawn.Position;
   //         cnode2.Position = spawn.Position;
            StateScene.AddNode(cnode);


            //StateScene.AddNode(cnode2);
            // cnode2.PlayAnimation("Walk");


            //cnode.Position = spawn.Position;

            //Link nodes to bone transforms, such as camera to head.
            StateUI = new Vivid.UI.UI();

            IFrame talkFrame = new IFrame();
            talkFrame.Set(new Position(VividApp.FrameWidth / 2 - 300, VividApp.FrameHeight / 2), new Size(600, 300), "");
            ILabel t1, t2, t3;
            t1 = new ILabel().Set(new Position(40, 40), new Size(5, 5), "In a series of systems, networks.") as ILabel;
            t2 = new ILabel().Set(new Position(40, 65), new Size(5, 5), "They say the game ends the truth.") as ILabel;

            talkFrame.AddForms(t1, t2);

            talk3D = new Vivid.UI.UI3DContainer();
            talk3D.SetForm(talkFrame);
            



            StateUI.AddForm(talkFrame);


            //fl.Position = spawn.Position;
            VividApp.CurrentScene = StateScene;
        }
        Vivid.UI.UI3DContainer talk3D;
        public override void Update()
        {
            //base.Update();
            StateScene.Update();
            StateUI.Update();
        }

        public override void Render()
        {
            //base.Render();
             StateScene.RenderShadows();
              StateScene.Render();
              StateScene.RenderLines();
            //StateUI.Render();
            RenderGlobals.CurrentCamera = StateScene.MainCamera;

           // OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);
            talk3D.Update();
            talk3D.Render();
            
        }

    }
}
