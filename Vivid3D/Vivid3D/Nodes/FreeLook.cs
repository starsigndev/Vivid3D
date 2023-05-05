using Vivid.Scene;

namespace Vivid.Nodes
{
    public class FreeLook : Camera
    {
        private float cp = 0, cy = 0;

        public override void UpdateNode()
        {
            //base.UpdateNode();

            float spd = 1.5f;

            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift))
            {
                spd = 3.5f;
            }

            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
            {
                Move(0, 0, -0.07f*spd);
            }
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
            {
                Move(-0.05f*spd, 0, 0);
            }
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
            {
                Move(0.05f*spd, 0, 0);
            }
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
            {
                Move(0, 0, 0.07f * spd);
            }
            if (GameInput.KeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
            {
                //l1.LocalPosition = cam.LocalPosition;
                //l1.Rotation = cam.Rotation;
            }
            cp = cp - GameInput.MouseDelta.Y * 0.2f;
            cy = cy - GameInput.MouseDelta.X * 0.2f;
            //ang = ang + 0.2f;

            SetRotation(cp, cy, 0);
        }
    }
}