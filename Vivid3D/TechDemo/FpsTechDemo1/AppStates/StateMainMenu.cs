using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Content;
using Vivid.UI;
using Vivid.UI.Forms;
using Vivid.Texture;
using Vivid.Audio;

namespace FpsTechDemo1.AppStates
{
    public class StateMainMenu : AppState
    {

        public StateMainMenu() : base("Main Menu")
        {

        }

        Content GameContent = null;
        IImage background_image;
        public override void Init()
        {

            Sound music = new Sound("gemini/cpu.wav");
            music.Play2D();

            GameContent = new Content("c:/fpsContent/content/fpsGame");

            ContentItem bg_image_Content = GameContent.Find("titleBg1.png");

            var bg_image = new Texture2D(bg_image_Content.GetStream(),bg_image_Content.Width,bg_image_Content.Height);


            background_image = new IImage(bg_image).Set(new Vivid.Maths.Position(0, 0), new Vivid.Maths.Size(VividApp.FrameWidth, VividApp.FrameHeight),"BGImage") as IImage;

            StateUI.AddForm(background_image);
            background_image.Color.a = 0.0f;

        }

        public override void Update()
        {
            //base.Update();
            StateUI.Update();
            background_image.Color.a += (1.0f - background_image.Color.a) * 0.05f;

        }

        public override void Render()
        {
            //base.Render();
            StateUI.Render();

        }

    }
}
