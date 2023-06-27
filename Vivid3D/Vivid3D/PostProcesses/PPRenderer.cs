using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Texture;

namespace Vivid.PostProcesses
{
    public class PPRenderer
    {

        public List<PostProcess> PP
        {
            get;
            set;
        }

        public int W, H;

        public PPRenderer()
        {
            PP = new List<PostProcess>();
        }

        public void SetSize(int w,int h)
        {
            W = w;
            H = h;
            foreach(var pp in PP)
            {
                pp.SetTargetsSize(w, h);
            }
        }

        public Texture2D Render(Texture2D bb)
        {
            Texture2D final = null;
            foreach(var pp in PP)
            {

                final = pp.Process(bb);

            }

            return final;

        }

    }
}
