using Vivid.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IImage : IForm
    {
       
        public IImage(Texture2D image)
        {

            Image = image;          

        }

        public override void OnRender()
        {

            Draw(Image);

        }

    }
}
