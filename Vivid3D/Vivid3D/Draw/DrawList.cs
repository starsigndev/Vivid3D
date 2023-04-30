using Vivid.Texture;

namespace Vivid.Draw
{
    public class DrawList
    {
        public List<DrawInfo> InfoList
        {
            get;
            set;
        }

        public Texture2D[] Texture
        {
            get;
            set;
        }

        public DrawList()
        {
            InfoList = new List<DrawInfo>();
            Texture = new Texture2D[2];
        }

        public void Add(DrawInfo info)
        {
            InfoList.Add(info);
        }

        public uint[] GenerateIndexData()
        {
            uint[] data = new uint[InfoList.Count * 6];
            int ii = 0;
            uint iv = 0;
            foreach (var info in InfoList)
            {
                data[ii++] = iv;
                data[ii++] = iv + 1;
                data[ii++] = iv + 2;
                data[ii++] = iv + 2;
                data[ii++] = iv + 3;
                data[ii++] = iv;

                iv = iv + 4;
            }

            return data;
        }

        public float[] GenerateVertexData()
        {
            float[] data = new float[InfoList.Count * 9 * 4];

            int loc = 0;
            foreach (var info in InfoList)
            {
                for (int i = 0; i < 4; i++)
                {
                    //pos
                    data[loc++] = info.X[i];
                    data[loc++] = info.Y[i];
                    data[loc++] = info.Z;

                    //uv
                    if (i == 0)
                    {
                        data[loc++] = 0;
                        if (info.FlipUV)
                        {
                            data[loc++] = 1;
                        }
                        else
                        {
                            data[loc++] = 0;
                        }
                        //if (info.FlipUV)
                        //{
                    }
                    else if (i == 1)
                    {
                        data[loc++] = 1;
                        if (info.FlipUV)
                        {
                            data[loc++] = 1;
                        }
                        else
                        {
                            data[loc++] = 0;
                        }
                    }
                    else if (i == 2)
                    {
                        data[loc++] = 1;
                        if (info.FlipUV)
                        {
                            data[loc++] = 0;
                        }
                        else
                        {
                            data[loc++] = 1;
                        }
                    }
                    else if (i == 3)
                    {
                        data[loc++] = 0;
                        if (info.FlipUV)
                        {
                            data[loc++] = 0;
                        }
                        else
                        {
                            data[loc++] = 1;
                        }
                    }
                    //col
                    data[loc++] = info.Color.r;
                    data[loc++] = info.Color.g;
                    data[loc++] = info.Color.b;
                    data[loc++] = info.Color.a;
                }
            }
            return data;
        }
    }
}