using Vivid.Physics;
using OpenTK.Mathematics;
//using OpenTK.Mathematics;

namespace Vivid.Physx
{
    public class PXTriMesh : PXBody
    {
        public List<Vivid.Meshes.Mesh> Meshes
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public PXTriMesh(List<Vivid.Meshes.Mesh> meshes, int index)
        {
            Meshes = meshes;
            Index = index;
            InitBody();
        }

        public override void InitBody()
        {
            
        }


    }
}