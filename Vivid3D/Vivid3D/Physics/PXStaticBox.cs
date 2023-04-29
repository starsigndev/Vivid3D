using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysX;
namespace Vivid.Physx
{
    public class PXStaticBox : PXBody
    {

        public PXStaticBox(float w, float h, float d)
        {
            W = w / 2;
            H = h / 2;
            D = d / 2;
            InitBody();
        }

        public override void InitBody()
        {
            //base.InitBody();
            Transform tm = new Transform(new System.Numerics.Vector3(0, 0, 0));

            Material = Vivid.Physx.QPhysics._Physics.CreateMaterial(0.4f, 0.4f, 0.4f);


           
            this.StaticBody = Vivid.Physx.QPhysics._Physics.CreateRigidStatic();

            Body = (RigidActor)StaticBody;
           // var act = (RigidActor)StaticBody;

            Shape = RigidActorExt.CreateExclusiveShape(StaticBody, new BoxGeometry(W, H, D), Material);
            int a = 5;
            //DynamicBody.GlobalPose;
            Vivid.Physx.QPhysics._Scene.AddActor(StaticBody);


        }

    }
}
