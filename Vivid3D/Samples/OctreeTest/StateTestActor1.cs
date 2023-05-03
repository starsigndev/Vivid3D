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

        SkeletalEntity a1;
        public override void Init()
        {
            s1 = new Vivid.Scene.Scene();

            var n1 = Importer.ImportEntity<Entity>("test/floor1.fbx");
            Texture2D tc, tn, ts;

            tc = n1.Meshes[0].Material.ColorMap;
            tn = n1.Meshes[0].Material.NormalMap;
            ts = n1.Meshes[0].Material.SpecularMap;
            n1.Meshes[0].Material = new Vivid.Materials.Materials.Entity.MaterialCelShaded();
            n1.Meshes[0].Material.ColorMap = tc;
            n1.Meshes[0].Material.NormalMap = tn;
            n1.Meshes[0].Material.SpecularMap = ts;
            s1.AddNode(n1);


        l1 = new Light();
            l1.Range = 80;
            l1.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);
            s1.Lights.Add(l1);

            var l2 = new Light();
            l2.Range = 70;
            l2.Position = new OpenTK.Mathematics.Vector3(-4, 6, -3);
           s1.Lights.Add(l2);
            l2.Diffuse = new OpenTK.Mathematics.Vector3(0, 2, 2);

            FreeLook fl = new FreeLook();
            s1.MainCamera = fl;

            fl.Position = new OpenTK.Mathematics.Vector3(0, 5, 3);


            //Comment the samples code.

            var act = Importer.ImportSkeletalEntity<SkeletalEntity>("test/c3.fbx");
            Importer.ImportAnimation(act, "test/anim_die1.fbx");
            act.Animator.LinkAnimation(0, "Walk");
            act.Animator.LinkAnimation(1, "Die1");

            act.Meshes[0].Material = new Vivid.Materials.Materials.Skeletal.MaterialSkeletalCelShaded();
            act.Meshes[1].Material = act.Meshes[0].Material;

            act.PlayAnimation("Walk");
            a1 = act;

            var v_diff = new Texture2D("test/vanguard_diffuse1.png");
            var v_norm = new Texture2D("test/vanguard_normal.png");
            var v_spec = new Texture2D("test/vanguard_specular.png");

            act.Meshes[1].Material.ColorMap = v_diff;
            act.Meshes[1].Material.SpecularMap = v_spec;
            act.Meshes[1].Material.NormalMap = v_norm;
            act.Meshes[0].Material.ColorMap = v_diff;
            act.Meshes[0].Material.SpecularMap = v_spec;
            act.Meshes[0].Material.NormalMap = v_norm;
            act.Scale = new OpenTK.Mathematics.Vector3(0.03f, 0.03f, 0.03f);


     
            s1.AddNode(act);
            

            //var act = Importer.ImportSkeletal<EntitySkel

        }

        public override void Update()
        {
            s1.Update();
        }
        Light l1;

        bool done = false;
        bool toggle = false;
        public override void Render()
        {
            s1.RenderShadows();
            s1.Render();
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
            {
                l1.Position = s1.MainCamera.Position;
            }
            if (!done)
            {
                if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Q))
                {
                    toggle = toggle ? false : true;
                    if (toggle)
                    {
                        a1.PlayAnimation("Walk");
                    }
                    else
                    {
                        a1.PlayAnimation("Die1");
                    }
                    done = true;
                }
            }
            else
            {
                if (!GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Q))
                {
                    done = false;
                }
            }

      //    if(GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.))
        }


    }
}
