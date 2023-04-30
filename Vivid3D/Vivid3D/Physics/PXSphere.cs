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

        }
    }
}