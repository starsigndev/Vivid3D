using BepuPhysics.Collidables;
using BepuPhysics;
using OpenTK.Audio.OpenAL.Extensions.SOFT.LoopPoints;
using System.Numerics;
using Vivid.Meshes;
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

            System.Numerics.Vector3[] points = new System.Numerics.Vector3[Mesh.Vertices.Count];
            int p = 0;
            foreach(Vertex v in Mesh.Vertices)
            {

                points[p] = new System.Numerics.Vector3(v.Position.X, v.Position.Y, v.Position.Z);
                p++;
            }

            Span<System.Numerics.Vector3> span = new Span<System.Numerics.Vector3>(points, 0, points.Length);

            System.Numerics.Vector3 c = new Vector3(0, 0, 0);
            BepuPhysics.Collidables.ConvexHull hull = new BepuPhysics.Collidables.ConvexHull(points, Physics.QPhysics.bufferPool,out c);

            var inertia = hull.ComputeInertia(1.0f);

            RigidPose pose = new RigidPose();
            pose.Position = new System.Numerics.Vector3(0, 0, 0);
            pose.Orientation = System.Numerics.Quaternion.Identity;
            Body = BodyDescription.CreateDynamic(pose, inertia, Physics.QPhysics._Sim.Shapes.Add(hull), 0.1f);
            Handle = Physics.QPhysics._Sim.Bodies.Add(Body);


        }
    }
}