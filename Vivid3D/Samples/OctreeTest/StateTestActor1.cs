using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Importing;
using Vivid.App;
using Vivid.Scene;
using Vivid.Nodes;
using Vivid.Anim;

namespace OctreeTest
{
    public class StateTestActor1 : AppState
    {

        public StateTestActor1() : base("Test Animation")
        {



        }

        Vivid.Scene.Scene s1;

        public override void Init()
        {
            s1 = new Vivid.Scene.Scene();

            var n1 = Importer.ImportEntity<Entity>("test/floor1.fbx");
            s1.AddNode(n1);

            var l1 = new Light();
            l1.Range = 80;
            l1.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);
            s1.Lights.Add(l1);

            FreeLook fl = new FreeLook();
            s1.MainCamera = fl;

            fl.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);

            var act = Importer.ImportSkeletalEntity<SkeletalEntity>("test/a1.fbx");

            act.Scale = new OpenTK.Mathematics.Vector3(0.1f, 0.1f, 0.1f);


            ActorAnimation anim = new ActorAnimation("Dance", 0, 70, 0.05f, AnimType.Forward);
            act.AddAnimation(anim);
            act.PlayAnimation("Dance");
            s1.AddNode(act);

            //var act = Importer.ImportSkeletal<EntitySkel

        }

        public override void Update()
        {
            s1.Update();
        }

        public override void Render()
        {
            s1.RenderShadows();
            s1.Render();
        }


    }
}
