using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysX;
using Vivid.Meshes;
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

            var mesh = Mesh;

            int vc = mesh.Vertices.Count;
            int tc = mesh.Triangles.Count;

            //TriangleMeshDesc td = new TriangleMeshDesc();
            ConvexMeshDesc td = new ConvexMeshDesc();


            System.Numerics.Vector3[] points = new System.Numerics.Vector3[vc];
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {

                var pos = mesh.Vertices[i].Position;
                points[i] = new System.Numerics.Vector3(pos.X, pos.Y, pos.Z);


            }

            int[] tris = new int[mesh.Triangles.Count * 3];

            int ii = 0;
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {

                tris[ii++] = (int)mesh.Triangles[i].V0;
                tris[ii++] = (int)mesh.Triangles[i].V2;
                tris[ii++] = (int)mesh.Triangles[i].V1;

            }

            td.SetPositions(points);
            td.SetTriangles<int>(tris);
            //td.Points = points;
            //td.Triangles = tris
            //t;
            td.Flags = ConvexFlag.ComputeConvex;

            var stream = new MemoryStream();

            var result = Vivid.Physics.QPhysics._Cooking.CookConvexMesh(td, stream);

            stream.Position = 0;

            var tm = Vivid.Physics.QPhysics._Physics.CreateConvexMesh(stream);

            int bb = 0;



            Material = Vivid.Physics.QPhysics._Physics.CreateMaterial(0.4f, 0.4f, 0.4f);

            this.DynamicBody = Vivid.Physics.QPhysics._Physics.CreateRigidDynamic();


            Body = (RigidActor)DynamicBody;
            // var act = (RigidActor)StaticBody;

            Shape = RigidActorExt.CreateExclusiveShape(DynamicBody, new ConvexMeshGeometry(tm), Material);
            int a = 5;

            Vivid.Physics.QPhysics._Scene.AddActor(Body);


        }

    }
}
