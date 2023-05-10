using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Mesh;
using Vivid.Scene;

namespace FpsTechDemo1.Nodes
{
    public class CharacterNode : SkeletalEntity
    {

        public override SkeletalEntity Spawn()
        {
            //base.Spawn();
            CharacterNode res = new CharacterNode();
            res.Meshes = Meshes;
            res.Position = Position;
            res.Rotation = Rotation;
            res.Name = Name;
            res.Animator = new Vivid.Anim.Animator().Copy();
            return res;


        }
        public CharacterNode()
        {

   
        }

        public override void Start()
        {
            //base.InitNode();
            PushState(new NodeStates.CharIdle());
            debug_lines = new MeshLines();
            VividApp.CurrentScene.MeshLines.Add(debug_lines);
        }

        public void UpdateCharacter()
        {

           // PlayAnimation("Idle",false);

        }
        public MeshLines debug_lines;
        public override void UpdateNode()
        {

            UpdateCharacter();
            //base.UpdateNode();
            Ray ray = new Ray();
            ray.Pos = Position;
            ray.Pos.Y = ray.Pos.Y + 4;
            ray.Dir = new OpenTK.Mathematics.Vector3(0, -8, 0);
            RaycastResult res = VividApp.CurrentScene.Raycast(ray, this);

           

            if (res != null)
            {
                if (res.Hit)
                {
                    Position = res.Point;
                }
            }

        }

    }
}
