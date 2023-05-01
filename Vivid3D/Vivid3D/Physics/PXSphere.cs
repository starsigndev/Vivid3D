using BepuPhysics;
using Vivid.Physics;

namespace Vivid.Physx
{
    public class PXSphere : PXBody
    {
        public float Radius
        {
            get;
            set;
        }

        public PXSphere(float size)
        {
            Radius = size;
            InitBody();
        }

        public override void InitBody()
        {
            BepuPhysics.Collidables.Sphere sphere = new BepuPhysics.Collidables.Sphere(Radius);
            var inertia = sphere.ComputeInertia(1.0f);
            RigidPose pose = new RigidPose();


            pose.Position = new System.Numerics.Vector3(0, 0, 0);
            pose.Orientation = System.Numerics.Quaternion.Identity;
            Body = BodyDescription.CreateDynamic(pose, inertia, Physics.QPhysics._Sim.Shapes.Add(sphere), 0.1f);
            Handle = Physics.QPhysics._Sim.Bodies.Add(Body);
        }
    }
}