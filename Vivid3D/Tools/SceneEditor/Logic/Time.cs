using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static Editor.SceneEditor;
namespace Editor.Logic
{
    public class Time
    {

        public static void MainTick()
        {
           
            Vector3 move = new Vector3(0, 0, 0);

            float spd = 0.1f;
            if (MoveFaster)
            {
                spd = spd * 2.5f;
            }

            if (MoveDown)
            {
                move.Y = -spd;
            }
            if (MoveUp)
            {
                move.Y = spd;
            }
            if (MoveZ)
            {
                move.Z = spd;
            }
            if (MoveZBack)
            {
                move.Z = -spd;
            }
            if (MoveXLeft)
            {
                move.X = -spd;
            }
            if (MoveXRight)
            {
                move.X = spd;
            }

            EditCam.Move(move.X, move.Y, -move.Z);

            Output.Invalidate();

            //throw new NotImplemented
            //  Exception();

        }

    }
}
