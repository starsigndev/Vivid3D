using Vivid.Physics;
using OpenTK.Mathematics;
using Microsoft.VisualBasic;
using BepuUtilities.Memory;
using BepuPhysics.Collidables;
using BepuPhysics;
using System.Numerics;
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
           
            BepuUtilities.Memory.Buffer<BepuPhysics.Collidables.Triangle> tris = new BepuUtilities.Memory.Buffer<BepuPhysics.Collidables.Triangle>(Meshes[0].Triangles.Count,Physics.QPhysics.bufferPool); ;

            unsafe
            {
                int t = 0;
                foreach (var tri in Meshes[0].Triangles)
                {

                    var bt = tris.GetPointer(t);
                    OpenTK.Mathematics.Vector3 v0, v1, v2;
                    v0  = Meshes[0].Vertices[tri.V0].Position;
                    v1 = Meshes[0].Vertices[tri.V1].Position;
                    v2 = Meshes[0].Vertices[tri.V2].Position;
                  
                    bt->A = new System.Numerics.Vector3(v0.X, v0.Y, v0.Z);
                    bt->C = new System.Numerics.Vector3(v1.X, v1.Y, v1.Z);
                    bt->B = new System.Numerics.Vector3(v2.X, v2.Y, v2.Z);
                    bt->ComputeInertia(1.0f);
                    t++;
                    continue;

                    Triangle* p = bt;
                    t++;
                    bt = tris.GetPointer(t);
                    bt->A = p->A;
                    bt->B = p->C;
                    bt->C = p->B;




                    t++;

                }
            }

            BepuPhysics.Collidables.Mesh mesh = new BepuPhysics.Collidables.Mesh(tris, new System.Numerics.Vector3(1, 1, 1), Physics.QPhysics.bufferPool);
       
            RigidPose pose = new RigidPose();

            pose.Position = new System.Numerics.Vector3(0, 0, 0);
            pose.Orientation = System.Numerics.Quaternion.Identity;

            var sd = new StaticDescription(pose, Physics.QPhysics._Sim.Shapes.Add(mesh));
            
            //Body = BodyDescription.CreateDynamic(pose, inertia, Physics.QPhysics._Sim.Shapes.Add(box), 0.1f);
            var sh = Physics.QPhysics._Sim.Statics.Add(sd);


        }


    }
}