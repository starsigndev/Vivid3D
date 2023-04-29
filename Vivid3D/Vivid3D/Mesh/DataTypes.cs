using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Vivid.Meshes
{

    [StructLayout(LayoutKind.Sequential)]
    public struct int4
    {
        public int a, b, c, d;
        public int this[int id]
        {
            get
            {
                switch (id)
                {
                    case 0:
                        return a;
                    case 1:
                        return b;
                    case 2:
                        return c;
                    case 3:
                        return d;
                }
                return -1;
            }
            set
            {
                switch (id)
                {
                    case 0:
                        a = value;
                        break;
                    case 1:
                        b = value;
                        break;
                    case 2:
                        c = value;
                        break;
                    case 3:
                        d = value;
                        break;
                }

            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct dpos3
    {
        public float x, y, z;
        public dpos3(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct dpos4
    {
        public float x, y, z, w;
        public dpos4(float x,float y,float z,float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public dpos4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return x;
                        break;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                }
                return -1;
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                }
            }


        }

    }

    [StructLayout (LayoutKind.Sequential)]
    public struct LineVertex
    {
        public Vector3 Position;
        public Vector4 Color;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct Line
    {

        public int v0, v1;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {

        public Vector3 Position;
        public Vector4 Color;
        public Vector3 TexCoord;
        public Vector3 Normal;
        public Vector3 BiNormal;
        public Vector3 Tangent;
        public Vector4 BoneIDS;// = new dpos4();
        public Vector4 Weights;
        public Vertex()
        {
            //Position = new dpos3();
            //CallingConvention/

            Position = new Vector3(); //0
            Normal = new Vector3(); //3
            Color = new Vector4(1, 1, 1, 1); //6
            BiNormal = new Vector3(0, 0, 0); //10
            Tangent = new Vector3(0, 0, 0); //13
            TexCoord = new Vector3(0, 0, 0);//16
            BoneIDS = new Vector4(); //19
            Weights = new Vector4(); //23
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        public int V0;
        public int V1;
        public int V2;
        public Triangle(int v0,int v1,int v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }
    }

    internal class DataTypes
    {
    }
}
