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

        public Texture2D ButtonSelected
        {
            get;
            set;
        }

        public UITheme(string name)
        {
            var frame = Content.Content.GlobalFindItem(name + ".frame.png");
            Frame = new Texture2D(frame.GetStream(), frame.Width, frame.Height);
            var button = Content.Content.GlobalFindItem(name + ".button.png");
            Button = new Texture2D(button.GetStream(), button.Width, button.Height);
            var button_sel = Content.Content.GlobalFindItem(name + ".button_selected.png");
            ButtonSelected = new Texture2D(button_sel.GetStream(),button_sel.Width, button_sel.Height);
        }
    }
}