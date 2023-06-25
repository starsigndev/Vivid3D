using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysX;
using Vivid.Meshes;
//using OpenTK.Mathematics;
using Vivid.Maths;
using System.Numerics;

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

        public PXTriMesh(List<Vivid.Meshes.Mesh> meshes,int index)
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
            for(int i = 0; i < mesh.Vertices.Count; i++)
            {

                var pos = mesh.Vertices[i].Position;
                //var np = new Vector3(pos.x, pos.y, pos.z) * mesh.Owner.WorldMatrix;
                var np = OpenTK.Mathematics.Vector3.TransformPosition(pos, mesh.Owner.WorldMatrix);

                 points[i] = new System.Numerics.Vector3(-np.X, np.Y, np.Z);

            }

            int[] tris = new int[mesh.Triangles.Count * 6];

            int ii = 0;
            for(int i = 0; i < mesh.Triangles.Count; i++)
            {

                tris[ii++] = (int)mesh.Triangles[i].V0;
                tris[ii++] = (int)mesh.Triangles[i].V2;
                tris[ii++] = (int)mesh.Triangles[i].V1;
                tris[ii++] = (int)mesh.Triangles[i].V0;
                tris[ii++] = (int)mesh.Triangles[i].V1;
                tris[ii++] = (int)mesh.Triangles[i].V2;

            }

            td.Points = points;
            td.Triangles = tris;

            var stream = new MemoryStream();

            var result = Vivid.Physics.QPhysics._Cooking.CookTriangleMesh(td, stream);

            stream.Position = 0;

            var tm = Vivid.Physics.QPhysics._Physics.CreateTriangleMesh(stream);


            int bb = 0;

            

            Material = Vivid.Physics.QPhysics._Physics.CreateMaterial(0.4f, 0.4f, 0.4f);

            this.StaticBody = Vivid.Physics.QPhysics._Physics.CreateRigidStatic();

            Body = (RigidActor)StaticBody;
            // var act = (RigidActor)StaticBody;

            Shape = RigidActorExt.CreateExclusiveShape(StaticBody, new TriangleMeshGeometry(tm), Material);
            int a = 5;

            //Vivid.Physx.QPhysics._Scene.AddActor(Body);
            Vivid.Physics.QPhysics.AddActor(Body,mesh.Owner);



        }


        public TriangleMesh triMesh = null;

    }
}
