using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class ILabel : IForm
    {

        public override void OnRender()
        {
            //base.OnRender();
            UI.DrawString(Text, RenderPosition.x, RenderPosition.y, Color);
        }

    }
}
