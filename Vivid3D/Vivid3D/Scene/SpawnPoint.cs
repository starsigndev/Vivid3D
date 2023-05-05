namespace Vivid.Scene
{
    public class SpawnPoint : Node
    {
        public int Index
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }
        public SpawnPoint()
        {
            Type = "Spawn";
            Index = 0;
        }
    }
}