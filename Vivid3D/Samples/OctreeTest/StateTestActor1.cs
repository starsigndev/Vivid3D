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
using Vivid;
using Vivid.Texture;

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

        l1 = new Light();
            l1.Range = 80;
            l1.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);
            s1.Lights.Add(l1);

            FreeLook fl = new FreeLook();
            s1.MainCamera = fl;

            fl.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);

            var act = Importer.ImportSkeletalEntity<SkeletalEntity>("test/a1.fbx");

            var v_diff = new Texture2D("test/vampire_diffuse.png");
            var v_norm = new Texture2D("test/spec_vampire_diffuse.png");

            act.Meshes[0].Material.ColorMap = v_diff;
            act.Meshes[0].Material.SpecularMap = v_norm;

            act.Scale = new OpenTK.Mathematics.Vector3(0.03f, 0.03f, 0.03f);


            ActorAnimation anim = new ActorAnimation("Dance", 0,75, 0.07f, AnimType.Forward);
            act.AddAnimation(anim);
            act.PlayAnimation("Dance");
            s1.AddNode(act);
            

            //var act = Importer.ImportSkeletal<EntitySkel

        }

        public override void Update()
        {
            s1.Update();
        }
        Light l1;
        public override void Render()
        {
            s1.RenderShadows();
            s1.Render();
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
            {
                l1.Position = s1.MainCamera.Position;
            }
        }


    }
}
