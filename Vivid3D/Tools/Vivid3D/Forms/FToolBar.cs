using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Texture;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FToolBar : Vivid.UI.Forms.IToolBar
    {

        public IButton Move, Rotate, Scale;

        public FToolBar()
        {

            Set(0, 0, VividApp.FrameWidth, 45, "");
            AddSpace(32);
            var move = AddTool(new Texture2D("ui/v3d/moveicon2.png"));
            var rotate = AddTool(new Texture2D("ui/v3d/rotateIcon.png"));
            var scale = AddTool(new Texture2D("ui/v3d/scaleIcon.png"));

            var space_sel = new IEnumSelector(typeof(SpaceMode));

            space_sel.OnSelected += Space_sel_OnSelected;

            space_sel.Size = new Vivid.Maths.Size(140, 20);

            AddTool(space_sel);

            space_sel.Position.y = space_sel.Position.y + 6;

            AddSpace(356);

            var play = AddTool(new Texture2D("ui/v3d/playicon.png"));
            var pause = AddTool(new Texture2D("ui/v3d/pauseicon.png"));
            var stop = AddTool(new Texture2D("ui/v3d/stopicon.png"));
            play.OnClick += (form, data) =>
            {
                Editor.Play();
                play.Highlight = true;
                stop.Highlight = false;
                pause.Highlight = false;
               // Editor.PlayMode = PlayMode.Play;
            };
            stop.OnClick += (form, data) =>
            {
                Editor.Stop();
                play.Highlight = false;
                pause.Highlight = false;
                stop.Highlight = false;
            };

            pause.OnClick += (form, data) =>
            {
                if (Editor.PlayMode != PlayMode.Play)
                {
                    return;
                }
                Editor.Pause();
                play.Highlight = false;
                pause.Highlight = true;
                stop.Highlight = false;
            };

            move.ToolTip = "Set the editor to translate mode.";
            rotate.ToolTip = "Set the editor to rotate mode.";
            scale.ToolTip = "Set the editor to scale mode.";

            move.OnClick += Move_OnClick;
            rotate.OnClick += Rotate_OnClick;
            scale.OnClick += Scale_OnClick;

            Move = move as IButton;
            Rotate = rotate as IButton;
            Scale = scale as IButton;

            move.Highlight = true;

        }

        private void Space_sel_OnSelected(string value)
        {
            switch (value)
            {
                case "Local":
                    Editor.SpaceMode = SpaceMode.Local;
                    break;
                case "Global":
                    Editor.SpaceMode = SpaceMode.Global;
                   break;

            }
        }

        private void Scale_OnClick(Vivid.UI.IForm form, object data = null)
        {
            Move.Highlight = false;
            Rotate.Highlight = false;
            Scale.Highlight = true;
            Editor.EditMode = EditorMode.Scale;
        }

        private void Rotate_OnClick(Vivid.UI.IForm form, object data = null)
        {
            Move.Highlight = false;
            Rotate.Highlight = true;
            Scale.Highlight = false;
            Editor.EditMode = EditorMode.Rotate;
        }

        private void Move_OnClick(Vivid.UI.IForm form, object data = null)
        {
            Move.Highlight = true;
            Rotate.Highlight = false;
            Scale.Highlight = false;
            Editor.EditMode = EditorMode.Translate;
            //throw new NotImplementedException();
        }
    
        
    
    }



}
