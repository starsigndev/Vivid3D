using Vivid.Texture;

namespace Vivid.UI.Forms
{
    public class IImage : IForm
    {
        public IImage(Texture2D image)
        {
            Image = image;
        }

        public override void OnRender()
        {
            Draw(Image);
        }
    }
}