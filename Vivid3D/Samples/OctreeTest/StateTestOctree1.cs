using Vivid;
using Vivid.Acceleration.Octree;
using Vivid.App;
using Vivid.Mesh;
using Vivid.Scene;

namespace OctreeTest
{
    public class StateTestOctree1 : AppState
    {
        public StateTestOctree1() : base("Test Octree")
        {
        }

        private Scene s1;
        private Camera c1;
        private Vivid.Acceleration.Octree.ASOctree ot1;

        public override void Init()
        {
            //base.Init();
            s1 = new Vivid.Scene.Scene();
            s1.Load("test/sc1.scene");

            MeshLines lines = new MeshLines();
            //lines.AddBox(s1.Bounds.Min, s1.Bounds.Max, new OpenTK.Mathematics.Vector4(1, 1, 1, 1));// new OpenTK.Mathematics.Vector3(-5, -5, -5), new OpenTK.Mathematics.Vector3(5, 5, 5),new OpenTK.Mathematics.Vector4(1,1,1,1));
            //lines.CreateBuffers();
            s1.MeshLines.Add(lines);

            Vivid.Nodes.FreeLook fl = new Vivid.Nodes.FreeLook();
            s1.MainCamera = fl;
            ot1 = new Vivid.Acceleration.Octree.ASOctree(s1);
            Console.WriteLine("Leafs:" + ot1.LeafCount());
            ot1.Debug();
            Console.WriteLine("Scene Verts:" + s1.VertexCount + " Tris:" + s1.TriCount);
            Console.WriteLine("Octree Verts:" + ot1.VertexCount() + " Tris:" + ot1.TriangleCount());

            var list = ot1.GetLeafs();
            int aa = 5;
            leafs = list;
        }

        private List<OctreeNode> leafs;

        public override void Update()
        {
            //base.Update();
            s1.Update();
        }

        private int lc = 0;
        private bool none = false;

        public override void Render()
        {
            base.Render();
            s1.RenderShadows();
            //s1.Render();
            // ot1.ComputeVisibility();
            // ot1.Render();
            //ot1.Render();
            if (!none)
            {
                if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad1))
                {
                    lc--;
                    none = true;
                    //Environment.Exit(0);
                }
                if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad2))
                {
                    lc++;
                    none = true;
                }
            }
            else
            {
                if (!GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad1) && !GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad2))
                {
                    none = false;
                }
            }
            if (lc < 0) lc = 0;
            if (lc >= leafs.Count) lc = 0;
            //leafs[lc].Render();
            ot1.ComputeVisibility();
            ot1.Render();
            //s1.Render();

            s1.RenderLines();
        }
    }
}