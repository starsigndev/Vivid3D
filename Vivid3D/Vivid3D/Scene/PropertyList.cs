using Vivid.Audio;
using Vivid.Texture;

namespace Vivid.Scene
{
    public enum PropertyType
    {
        String, Float, Int, Vec3, Vec4, Matrix, File, Texture, Sound, Scene, Content
    }

    public class PropertyItem
    {
        public string Name { get; set; }
        public string StringValue;
        public int IntValue;
        public float FloatValue;
        public OpenTK.Mathematics.Vector3 Vec3Value = new OpenTK.Mathematics.Vector3();
        public OpenTK.Mathematics.Vector4 Vec4Value = new OpenTK.Mathematics.Vector4();
        public OpenTK.Mathematics.Matrix4 MatrixValue = new OpenTK.Mathematics.Matrix4();
        public Texture2D TextureValue;
        public Sound SoundValue;
        public Vivid.Scene.Scene SceneValue;
        public Content.Content ContentValue;

        public PropertyType Type
        {
            get;
            set;
        }

        public PropertyItem()
        {
            Name = "Property";
            Type = PropertyType.String;
        }

        public override string ToString()
        {
            return "Property:" + Name + " Type:" + Type.ToString();
            //return base.ToString();
        }
    }

    public class PropertyList
    {
        public List<PropertyItem> Items
        {
            get;
            set;
        }

        public PropertyList()
        {
            Items = new List<PropertyItem>();
        }
    }
}