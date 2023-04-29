using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;
namespace Vivid.Acceleration.Octree
{

    public class ASOctree
    {

        public Scene.Scene Base
        {
            get;
            set;
        }

        public OctreeNode RootNode
        {
            get;
            set;
        }

        public static int VertexLeafLimit = 1024;

        public ASOctree(Scene.Scene scene)
        {
            Base = scene;

            ProcessScene();
            RootNode.CreateBuffers();
        }

        public void ProcessScene()
        {
            RootNode = new OctreeNode(Base.Bounds, Base);
        }

        public int LeafCount()
        {

            return RootNode.LeafCount;

        }

        public int VertexCount()
        {

            return RootNode.VertexCount;

        }

        public int TriangleCount()
        {
            return RootNode.TriangleCount;
        }

        public void Debug()
        {
            RootNode.Debug();
        }

        public void Render()
        {

            //OctreeNode.lc = 1;
            RootNode.Render();
        }
        public List<OctreeNode> GetLeafs()
        {
            return RootNode.Leafs;
        }

    }
}
