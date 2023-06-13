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
            Color = new Maths.Color(0.8f, 0.8f, 0.8f, 0.95f);
        }
        public override void AfterSet()
        {
            //base.AfterSet();
            foreach(var form in Forms)
            {
                form.AfterSet();
            }
        }
        public override void OnRender()
        {
        //    BlurBG();
            Draw(UI.Theme.Pure);
        }
    }
}