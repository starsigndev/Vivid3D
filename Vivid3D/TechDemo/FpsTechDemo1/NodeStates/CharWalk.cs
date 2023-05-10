using FpsTechDemo1.Nodes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using Vivid.Scene;

namespace FpsTechDemo1.NodeStates
{
    public class CharWalk : NodeState
    {
        public Vector3 TargetPosition = Vector3.Zero;
        public override void Start()
        {
            var char_node = (CharacterNode)Node;
            char_node.PlayAnimation("Walk", false);
        }

        float pp = 0;
        public override void Update()
        {
            //base.Update();
            Vector3 diff = TargetPosition - Node.Position;

            float r_ang = (float)Math.Atan2(diff.Z, diff.X);

            r_ang = MathHelper.RadiansToDegrees(r_ang) - 180;

            pp = pp + 1f;


            Node.SetRotation(0,pp, 0);
         //   Console.WriteLine("PP:" + r_ang+" AR:"+pp);


            //Node.Move(0, 0, 0.05f);

            var cn = Node as CharacterNode;
            cn.debug_lines.Lines.Clear();
            cn.debug_lines.AddLine(cn.Position+new Vector3(0,3,0), TargetPosition+new Vector3(0,3,0), new Vector4(1, 1, 1, 1));
            cn.debug_lines.CreateBuffers();


            float vd = Vector3.Distance(TargetPosition, Node.Position);
            Console.WriteLine("DIST:" + vd);
            if (vd < 1.5f)
            {
                Node.PopState();
            }



        }


    }
}
