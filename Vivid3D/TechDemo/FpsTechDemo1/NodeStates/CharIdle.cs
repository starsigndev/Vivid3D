using FpsTechDemo1.Nodes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;

namespace FpsTechDemo1.NodeStates
{
    public class CharIdle : NodeState
    {

        public override void Start()
        {

            var char_node = (CharacterNode)Node;
            char_node.PlayAnimation("Idle",false);

        }

        public override void Update()
        {
            //base.Update();
            Random rnd = new Random(Environment.TickCount);

      
                float x, z;
                x = -20+(float)rnd.NextDouble() * 40;
                z = -20 + (float)rnd.NextDouble() * 40;
            
                var ns = new CharWalk();
                ns.TargetPosition = new Vector3(10, 0, 10);
                Node.PushState(ns);


        }


    }
}
