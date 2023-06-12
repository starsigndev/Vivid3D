namespace Vivid.Maths
{
    public class Delta
    {
        public float x
        {
            get;
            set;
        }

        public float y
        {
            get;
            set;
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y);
            }
        }

        public Delta(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}