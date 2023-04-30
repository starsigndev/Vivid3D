namespace Vivid.Maths
{
    public class Color
    {
        public float r
        {
            get;
            set;
        }

        public float g
        {
            get;
            set;
        }

        public float b
        {
            get;
            set;
        }

        public float a
        {
            get;
            set;
        }

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static Color operator *(Color l, Color r)
        {
            return new Color(l.r * r.r, l.g * r.g, l.b * r.b, l.a * r.a);
        }

        public static Color operator *(Color l, float r)
        {
            return new Color(l.r * r, l.g * r, l.b * r, l.a * r);
        }

        public static Color operator -(Color l, Color r)
        {
            return new Color(l.r - r.r, l.g - r.g, l.b - r.b, l.a - r.a);
        }

        public static Color operator +(Color l, Color r)
        {
            return new Color(l.r + r.r, l.g + r.g, l.b + r.b, l.a + r.a);
        }
    }
}