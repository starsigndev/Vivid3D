namespace Vivid.Maths
{
    public static class MathHelp
    {
        public static float Rad2Degrees(float radians)
        {
            return radians * 180.0f / 3.14159265358979323846f * 2.0f;
        }

        public static float Degrees2Rad(float degrees)
        {
            return degrees / 360.0f * 3.14159265358979323846f * 2.0f;
        }

        public static float pi = 3.14159265358979323846f;
    }
}