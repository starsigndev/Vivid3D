using Vivid.Physics;

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
           
        }
    }
}