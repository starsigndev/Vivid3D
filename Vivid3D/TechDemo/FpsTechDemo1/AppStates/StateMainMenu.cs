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

            //Possible cel-shading look.


            Sound music = new Sound("gemini/cpu.wav");
            Channel music_chan = music.Play2D();

           // GameContent = new Content("c:/fpsContent/content/fpsGame");

            StateUI = new UI();

            ContentItem bg_image_Content = FpsTechDemoApp.ImagesContent.Find("titleBg1.png");

            var bg_image = new Texture2D(bg_image_Content.GetStream(),bg_image_Content.Width,bg_image_Content.Height);


            background_image = new IImage(bg_image).Set(new Vivid.Maths.Position(0, 0), new Vivid.Maths.Size(VividApp.FrameWidth, VividApp.FrameHeight),"BGImage") as IImage;

            StateUI.AddForm(background_image);
            background_image.Color.a = 0.0f;

            IFrame menu_frame = new IFrame().Set(new Vivid.Maths.Position(VividApp.FrameWidth / 2 - 170, VividApp.FrameHeight - 350), new Vivid.Maths.Size(340, 300), "") as IFrame;

            StateUI.AddForm(menu_frame);

            IButton solo_game = new IButton().Set(new Vivid.Maths.Position(40, 20), new Vivid.Maths.Size(260, 30), "Solo Game") as IButton;
            IButton online_game = new IButton().Set(new Vivid.Maths.Position(40, 60), new Vivid.Maths.Size(260, 30), "Net Game") as IButton;

            menu_frame.AddForms(solo_game, online_game);

        

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
