using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IVector3 : IForm
    {

        private INumericBox bX, bY, bZ;

        public IVector3()
        {

            bX = new INumericBox();
            bY = new INumericBox();
            bZ = new INumericBox();
            AddForms(bX, bY, bZ);


        }

        public override void AfterSet()
        {
            //base.AfterSet();
            bX.Set(0, 0, 120, 28);
            bY.Set(160, 0, 120, 28);
            bZ.Set(320, 0, 120, 28);
        }

    }
}
