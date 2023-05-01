using Vivid.Physics;
using System.Numerics;
using Vivid.Maths;
using BepuPhysics;

namespace Vivid.Physx
{
    public enum BodyType
    {
        Box, Sphere, TriMesh, ConvexHull, None, FPS
    }

    public class PXBody
    {
       
        public PXBody()
        {
            //  InitBody();
        }

     

        public Vivid.Scene.Node Node
        {
            get;
            set;
        }

        //private OpenTK.Mathematics.Matrix4
      
     

        public void SetPose(OpenTK.Mathematics.Matrix4 world)
        {
            //Body.GlobalPose = ConvertMatrix4ToMatrix4x4(world);
        }

        public void SetPose(OpenTK.Mathematics.Vector3 pos, OpenTK.Mathematics.Matrix4 rot)
        {

            var bb = Physics.QPhysics._Sim.Bodies[Handle];
            bb.Pose.Position = new Vector3(pos.X, pos.Y, pos.Z);
            var qr = rot.ExtractRotation();
            bb.Pose.Orientation = new Quaternion(qr.X, qr.Y, qr.Z, qr.W); //n//rot.ExtractRotation();



            //Body.Pose.Position = new Vector3(pos.X, pos.Y, pos.Z);
            //Body.Pose.Orientation = Qua

            //Body.Pose.Orientation = Quaternion.Identity;

        }

        public void MakeStatic()
        {
     
        }

        public virtual void InitBody()
        {
        }

        public OpenTK.Mathematics.Vector3 GetPos()
        {

            var bb = Physics.QPhysics._Sim.Bodies[Handle];

            return new OpenTK.Mathematics.Vector3(bb.Pose.Position.X, bb.Pose.Position.Y, bb.Pose.Position.Z);

            //return new OpenTK.Mathematics.Vector3(Handle.)

            return new OpenTK.Mathematics.Vector3(Body.Pose.Position.X,Body.Pose.Position.Y,Body.Pose.Position.Z);
            //  return new OpenTK.Mathematics.Vector3(pose.X, pose.Y, pose.Z);
        }

       
        public OpenTK.Mathematics.Matrix4 GetRot()
        {
            var bb = Physics.QPhysics._Sim.Bodies[Handle];
            var o = bb.Pose.Orientation;

            OpenTK.Mathematics.Quaternion qr = new OpenTK.Mathematics.Quaternion(o.X, o.Y, o.Z, o.W);

            return OpenTK.Mathematics.Matrix4.CreateFromQuaternion(qr);



            return OpenTK.Mathematics.Matrix4.Identity;
        }

        protected BodyHandle Handle;
        public BodyDescription Body;


        public float W, H, D;
    }
}