namespace Vivid.UI.Forms
{
    public class IButton : IFrame
    {
        private Maths.Color Target;

        public IButton()
        {
            Target = new Maths.Color(0.5f, 0.5f, 0.5f, 0.8f);

            Image = UI.Theme.Button;
            Color = new Maths.Color(0.4f, 0.4f, 0.4f, 0.5f);
        }

        public override void OnEnter()
        {
            //base.OnEnter();
            Target = (new Maths.Color(1.4f, 1.4f, 1.4f, 0.7f));
            //   Console.WriteLine("Button!!!!!!!!");
            // Environment.Exit(0);
        }

        public override void OnLeave()
        {
            //base.OnLeave();
            Target = new Maths.Color(0.5f, 0.5f, 0.5f, 0.8f);
        }

        public override void OnUpdate()
        {
            Color = Color + (Target - Color) * new Maths.Color(0.03f, 0.03f, 0.03f, 0.75f);
            //base.OnUpdate();
        }

        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            Target = new Maths.Color(1.4f, 0.8f, 0.8f, 1.0f);
            OnClick?.Invoke(this, null);
        }

        public override void OnMouseUp(MouseID button)
        {
            //base.OnMouseUp(button);
            OnEnter();
            OnClicked?.Invoke(this, null);
        }

        public override void OnRender()
        {
            Draw(Image);
            UI.DrawString(Text, RenderPosition.x + Size.w / 2 - UI.SystemFont.StringWidth(Text) / 2, RenderPosition.y + Size.h / 2 - UI.SystemFont.StringHeight() / 2 + 2, new Maths.Color(1, 1, 1, 1));
        }
    }
}