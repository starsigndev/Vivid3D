using Vivid.Physics;

namespace Vivid.Physx
{
    public class PXConvexHull : PXBody
    {
        public Vivid.Meshes.Mesh Mesh
        {
            get;
            set;
        }

        public PXConvexHull(Vivid.Meshes.Mesh mesh)
        {
            Mesh = mesh;
            InitBody();
        }

        public override void InitBody()
        {
            //base.InitBody();

            
        }
    }
}