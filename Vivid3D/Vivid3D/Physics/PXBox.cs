using BepuPhysics;
using Vivid.Physics;

namespace Vivid.Physx
{
    public class PXBox : PXBody
    {
        public PXBox(float w, float h, float d, Vivid.Scene.Node node)
        {
            W = w * 2;
            H = h * 2;
            D = d * 2;
            Node = node;

            InitBody();
        }

        public override void InitBody()
        {

            BepuPhysics.Collidables.Box box = new BepuPhysics.Collidables.Box(W, H, D);
            var inertia = box.ComputeInertia(1.0f);
            RigidPose pose = new RigidPose();
            
            
            pose.Position = new System.Numerics.Vector3(0, 0, 0);
            pose.Orientation = System.Numerics.Quaternion.Identity;
            Body = BodyDescription.CreateDynamic(pose, inertia,Physics.QPhysics._Sim.Shapes.Add(box),  0.1f);
            Handle = Physics.QPhysics._Sim.Bodies.Add(Body);
        //    Body.Velocity = new BodyVelocity(new System.Numerics.Vector3(0, -5, 0));



            int b = 5;
        }
    }
}