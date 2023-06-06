using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Audio;
using Vivid.Content;
using Vivid.Draw;
using Vivid.Texture;

namespace ArcEpisode1.States
{
    public class StateLogos : AppState
    {
        public StateLogos() : base("Logos")
        {

        }

        public SmartDraw Draw = null;
        public Texture2D BG;
        public Texture2D Logo1;
        public Sound LogoSong;

        public float BGAlpha = 0.0f;

        public override void Init()
        {
            //base.Init();
            Draw = new SmartDraw();
            var cBG = Content.GlobalFindItem("starfield");
            var cLogo1 = Content.GlobalFindItem("starsignal");
            BG = new Texture2D(cBG.GetStream(), cBG.Width, cBG.Height);
            Logo1 = new Texture2D(cLogo1.GetStream(),cLogo1.Width, cLogo1.Height);
            int a = 5;
            var cSong = Content.GlobalFindItem("LogoSong1");
            LogoSong = new Sound("game/logosong1.wav");
            LogoSong.Play2D();

        }

        public override void Update()
        {
            // base.Update();
            if (BGAlpha < 1.0f)
            {
                BGAlpha = BGAlpha + 0.02f;
            }
        }

        public override void Render()
        {
            // base.Render();
            Draw.Blend = BlendMode.Alpha;
            Draw.Begin();
            Draw.Draw(BG, new Vivid.Maths.Rect(0, 0, VividApp.FrameWidth, VividApp.FrameHeight), new Vivid.Maths.Color(1, 1, 1, BGAlpha));
            Draw.End();

        }

    }
}
