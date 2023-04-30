using PhysX;

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
            //base.InitBody();
            Material = Vivid.Physx.QPhysics._Physics.CreateMaterial(0.4f, 0.4f, 0.4f);

            this.DynamicBody = Vivid.Physx.QPhysics._Physics.CreateRigidDynamic();
            Shape = RigidActorExt.CreateExclusiveShape(DynamicBody, new SphereGeometry(Radius), Material);
            int a = 5;
            Body = (RigidActor)DynamicBody;
            //DynamicBody.GlobalPose;
            Vivid.Physx.QPhysics._Scene.AddActor(DynamicBody);
        }
    }
}