using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Editor.SceneEditor;
namespace Editor.Logic
{
    public class KB
    {
        public static void Up(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift)
            {
                MoveFaster = false;
            }
            if (!e.Shift)
            {
                MoveFaster = false;
            }
            if (e.KeyCode == Keys.W)
            {
                MoveZ = false;
            }
            if(e.KeyCode == Keys.Q)
            {
                MoveDown = false;
            }
            if(e.KeyCode == Keys.E)
            {
                MoveUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                MoveZBack = false;
            }
            if (e.KeyCode == Keys.A)
            {
                MoveXLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                MoveXRight = false;
            }
            if (e.KeyCode == Keys.LShiftKey)
            {
                MoveFaster = false;
            }
        }
        public static void Down( KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Shift)
            {

                SceneEditor.MoveFaster = true;

            }
            if(e.KeyCode == Keys.F1)
            {
                SceneEditor.This.Click_SetTranslate(null, null) ;
            }
            if(e.KeyCode == Keys.F2)
            {
                SceneEditor.This.Click_SetRotate(null, null);
            }
            if(e.KeyCode == Keys.F3)
            {
                SceneEditor.This.Click_SetScale(null, null);
            }
            if(e.KeyCode == Keys.Q)
            {
                MoveDown = true;
            }
            if(e.KeyCode == Keys.E)
            {
                MoveUp = true;
            }
            if (e.KeyCode == Keys.W)
            {
                MoveZ = true;
            }
            if (e.KeyCode == Keys.S)
            {
                MoveZBack = true;
            }
            if (e.KeyCode == Keys.A)
            {
                MoveXLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                MoveXRight = true;
            }
            //throw new NotImplementedException();

        }
    }
}
