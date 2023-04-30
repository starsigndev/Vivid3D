using Vivid.Texture;

namespace Vivid.UI
{
    public class UITheme
    {
        public Texture2D Frame
        {
            get;
            set;
        }

        public Texture2D Button
        {
            get;
            set;
        }

        public UITheme(string name)
        {
            var frame = UI.UIBase.Find(name + ".frame.png");
            Frame = new Texture2D(frame.GetStream(), frame.Width, frame.Height);
            var button = UI.UIBase.Find(name + ".button.png");
            Button = new Texture2D(button.GetStream(), button.Width, button.Height);
        }
    }
}