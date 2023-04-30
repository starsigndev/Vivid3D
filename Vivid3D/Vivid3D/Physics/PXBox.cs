using PhysX;

namespace Vivid.Physx
{
    public class PXBox : PXBody
    {
        public PXBox(float w, float h, float d, Vivid.Scene.Node node)
        {
            W = w / 2;
            H = h / 2;
            D = d / 2;
            Node = node;

            InitBody();
        }

        public override void InitBody()
        {
            //base.InitBody();
            Transform tm = new Transform(new System.Numerics.Vector3(0, 0, 0));

            Material = Vivid.Physx.QPhysics._Physics.CreateMaterial(0.206f, 0.26f, 0.4f);

            this.DynamicBody = Vivid.Physx.QPhysics._Physics.CreateRigidDynamic();
            Shape = RigidActorExt.CreateExclusiveShape(DynamicBody, new BoxGeometry(W, H, D), Material);
            int a = 5;
            Body = (RigidActor)DynamicBody;
            //DynamicBody.GlobalPose;
            //Vivid.Physx.QPhysics._Scene.AddActor(DynamicBody);                                                         e
            Vivid.Physx.QPhysics.AddActor(DynamicBody, Node);

            Console.WriteLine(DynamicBody.AngularDamping);
            //Console.WriteLine(DynamicBody.15);

            Console.WriteLine("LD:" + DynamicBody.LinearDamping);

            int b = 5;
        }
    }
}