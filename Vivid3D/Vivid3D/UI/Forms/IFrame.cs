using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IFrame : IForm
    {

        public IFrame()
        {

            Image = UI.Theme.Frame;

        }

        public override void OnRender()
        {
            
            Draw(Image);

        }

     

    }
}
