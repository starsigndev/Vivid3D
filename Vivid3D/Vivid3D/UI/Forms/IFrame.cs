namespace Vivid.UI.Forms
{
    public class IFrame : IForm
    {
        public IFrame()
        {
            Image = UI.Theme.Frame;
        }

        public override void OnRender()
        {
            Draw(Image);
        }
    }
}