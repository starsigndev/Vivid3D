using Vivid.Physics;
using System.Numerics;
using Vivid.Maths;

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
         

        }

        public void MakeStatic()
        {
     
        }

        public virtual void InitBody()
        {
        }

        public OpenTK.Mathematics.Vector3 GetPos()
        {

            return new OpenTK.Mathematics.Vector3(0, 0, 0);
            //  return new OpenTK.Mathematics.Vector3(pose.X, pose.Y, pose.Z);
        }

       
        public OpenTK.Mathematics.Matrix4 GetRot()
        {
          
            return OpenTK.Mathematics.Matrix4.Identity;
        }

        public float W, H, D;
    }
}