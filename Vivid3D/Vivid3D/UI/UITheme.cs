using Vivid.Maths;
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

        public Texture2D FramePure
        {
            get;
            set;
        }

        public Texture2D FrameH
        {
            get;
            set;
        }

        public Texture2D FramePureH
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

        public Texture2D FrameShadow
        {
            get;
            set;
        }

        public Vivid.Maths.Color TextColor
        {
            get;
            set;
        }

        public Texture2D ArrowUp
        {
            get;
            set;
        }

        public Texture2D ArrowDown
        {
            get;
            set;
        }

        public UITheme(string name)
        {
            //var frame = Content.Content.GlobalFindItem(name + ".frame.png");
            Frame = new Texture2D("ui/theme/" + name + "/frame.png");
            FrameH = new Texture2D("ui/theme/" + name + "/frameh.png");
            TextColor = new Maths.Color(0.1f, 0.1f, 0.1f, 1.0f);
            FramePure = new Texture2D("ui/theme/" + name + "/framepure.png");
            FramePureH = new Texture2D("ui/theme/" + name + "/framepureh.png");

            var button = Content.Content.GlobalFindItem(name + ".button.png");
            Button = new Texture2D("ui/theme/" + name + "/button.png");

            FrameShadow = new Texture2D("ui/theme/" + name + "/shadow.png");

            ArrowUp = new Texture2D("ui/theme/" + name + "/arrowup.png");
            ArrowDown = new Texture2D("ui/theme/" + name + "/arrowdown.png");
            //var button_sel = Content.Content.GlobalFindItem(name + ".button_selected.png");
            ButtonSelected = new Texture2D("ui/theme/"+name+"/buttonselected.png");// new Texture2D(button_sel.GetStream(),button_sel.Width, button_sel.Height);
        }
    }
}