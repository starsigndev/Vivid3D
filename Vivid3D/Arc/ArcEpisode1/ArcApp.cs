using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Content;

namespace ArcEpisode1
{
    public class ArcApp : VividApp
    {
        public static Content GameBase;
        public ArcApp(GameWindowSettings game_win, NativeWindowSettings native_win) : base(game_win, native_win)
        {

        }
        public override void Init()
        {
            //base.Init();
            GameBase = new Content("game/content/gamebase");
        }
    }
}
