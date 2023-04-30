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
            

            int b = 5;
        }
    }
}