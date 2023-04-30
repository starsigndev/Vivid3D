using OpenTK.Mathematics;
using Vivid.App;

namespace Vivid.Scene
{
    public class Camera : Node
    {
        public override Matrix4 WorldMatrix
        {
            get
            {
                return base.WorldMatrix.Inverted();
            }
        }

        public float DepthStart
        {
            get;
            set;
        }

        public float DepthEnd
        {
            get;
            set;
        }

        public float FOV
        {
            get;
            set;
        }

        public Matrix4 Projection
        {
            get
            {
                //if (ProjectionMatrix == null)
                //{
                CreateProjectionMatrix();

                return ProjectionMatrix;
            }
        }

        private IntPtr fs = IntPtr.Zero;

        public void UpdateFS()
        {
            if (fs == IntPtr.Zero)
            {
                //   fs = GemBridge.gem_CreateFrustum(FOV, (float)Vivid.App.GeminiApp.FrameWidth / (float)GeminiApp.FrameHeight, DepthStart, DepthEnd);
            }
            // Vector3 p = Position;
            //  Vector3 l = new Vec3(0, 0, 1) * WorldMatrix.Inverse;
            //   Vector3 up = new Vec3(0, 1, 0) * WorldMatrix.Inverse;
            //    GemBridge.gem_UpdateFrustum(fs, p.handle, l.handle, up.handle);
        }

        public bool IsVisible(BoundingBox bb)
        {
            // int res =  GemBridge.gem_BoxInside(fs,bb.Min.handle, bb.Max.handle);
            // if (res == 1) return true;
            return false;
        }

        public Camera()
        {
            DepthStart = 0.2f;
            DepthEnd = 120.0f;
            FOV = 45.0f;
            //  CreateProjectionMatrix();
        }

        public void CreateProjectionMatrix()
        {
            //            ProjectionMatrix = Matrix4.Perspective(MathHelp.Degrees2Rad(FOV), (float)GeminiApp.FrameWidth / (float)GeminiApp.FrameHeight, DepthStart, DepthEnd);
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), (float)VividApp.FrameWidth / (float)VividApp.FrameHeight, DepthStart, DepthEnd);
        }

        private Matrix4 ProjectionMatrix = Matrix4.Identity;
    }
}