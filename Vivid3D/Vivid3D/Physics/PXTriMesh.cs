using PhysX;

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
            //base.InitBody();

            var mesh = Meshes[Index];

            int vc = mesh.Vertices.Count;
            int tc = mesh.Triangles.Count;

            TriangleMeshDesc td = new TriangleMeshDesc();

            System.Numerics.Vector3[] points = new System.Numerics.Vector3[vc];
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                //var pos = mesh.Vertices[i].Position;
                //var np = new Vector3(pos.x, pos.y, pos.z) * mesh.Owner.WorldMatrix;

                // points[i] = new System.Numerics.Vector3(-np.x, np.y, np.z);
            }

            int[] tris = new int[mesh.Triangles.Count * 6];

            int ii = 0;
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                tris[ii++] = (int)mesh.Triangles[i].V0;
                tris[ii++] = (int)mesh.Triangles[i].V2;
                tris[ii++] = (int)mesh.Triangles[i].V1;
                //tris[ii++] = (int)mesh.Triangles[i].V0;
                // tris[ii++] = (int)mesh.Triangles[i].V1;
                // tris[ii++] = (int)mesh.Triangles[i].V2;
            }

            td.Points = points;
            td.Triangles = tris;

            var stream = new MemoryStream();

            var result = Vivid.Physx.QPhysics._Cooking.CookTriangleMesh(td, stream);

            stream.Position = 0;

            var tm = QPhysics._Physics.CreateTriangleMesh(stream);

            int bb = 0;

            Material = Vivid.Physx.QPhysics._Physics.CreateMaterial(0.4f, 0.4f, 0.4f);

            this.StaticBody = Vivid.Physx.QPhysics._Physics.CreateRigidStatic();

            Body = (RigidActor)StaticBody;
            // var act = (RigidActor)StaticBody;

            Shape = RigidActorExt.CreateExclusiveShape(StaticBody, new TriangleMeshGeometry(tm), Material);
            int a = 5;

            //Vivid.Physx.QPhysics._Scene.AddActor(Body);
            Vivid.Physx.QPhysics.AddActor(Body, mesh.Owner);
        }

        public TriangleMesh triMesh = null;
    }
}