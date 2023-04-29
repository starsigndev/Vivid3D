using Vivid.Maths;
using Vivid.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Draw
{
    public class DrawInfo
    {

        public float[] X
        {
            get;
            set;
        }

        public float[] Y
        {
            get;
            set;
        }

        public float Z
        {
            get;
            set;
        }

        public Texture2D[] Texture
        {
            get;
            set;
        }

        public Vivid.Maths.Color Color
        {
            get;
            set;
        }

        public bool FlipUV
        {
            get;
            set;
        }

        public DrawInfo()
        {
            FlipUV = false;
            X = new float[4];
            Y = new float[4];
            Texture = new Texture2D[2];
            Z = 0.0f;
            Color = new Maths.Color(1, 1, 1, 1);


        }

    }
}
