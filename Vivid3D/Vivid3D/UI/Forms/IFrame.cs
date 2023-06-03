namespace Vivid.UI.Forms
{
    public class IFrame : IForm
    {
        public IFrame(bool pure = false)
        {
            if (pure)
            {
                Image = UI.Theme.FramePure;
            }
            else
            {
                Image = UI.Theme.Frame;
            }
            Color = new Maths.Color(1, 1, 1, 0.75f);
        }

        public override void OnRender()
        {
            BlurBG();
            Draw(Image);
        }
    }
}