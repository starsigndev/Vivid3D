namespace Vivid.Maths
{
    public class BoundingVolume
    {
        public float MinX, MinY, MinZ;
        public float MaxX, MaxY, MaxZ;

        public float Width
        {
            get
            {
                return MaxX - MinX;
            }
        }

        public float Height
        {
            get
            {
                return MaxY - MinY;
            }
        }

        public float Depth
        {
            get
            {
                return MaxZ - MinZ;
            }
        }
    }
}