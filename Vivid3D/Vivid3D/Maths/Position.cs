namespace Vivid.Maths
{
    public class Position
    {
        public int x
        { get; set; }

        public int y
        {
            get;
            set;
        }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Position operator-(Position a,Position b)
        {
            return new Position(a.x - b.x, a.y - b.y);
        }
        public static Position operator +(Position a, Position b)
        {
            return new Position(a.x + b.x, a.y + b.y);
        }
        public static Position operator+(Position a,Delta b)
        {
            return new Position(a.x + (int)b.x, a.y + (int)b.y);
        }

    }
}