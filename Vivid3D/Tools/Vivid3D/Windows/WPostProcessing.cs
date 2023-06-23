using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.PostProcesses;
using Vivid.UI;
using Vivid.UI.Forms;

namespace Vivid3D.Windows
{
    public class WPostProcessing : IWindow
    {

        public static List<PostProcess> PostProcesses = new List<PostProcess>();
        public IList PPList;

        public WPostProcessing() : base("Post Processing")
        {
            Set(200, 200, 500, 500, "Post Processing");
            PPList = new IList().Set(4, 6, 160, 500-28) as IList;
            Content.AddForm(PPList);

            foreach(var pp in PostProcesses)
            {

                string name = pp.ToString();
                name = name.Substring(name.LastIndexOf(".") + 1);

                PPList.AddItem(name);
            }
        }

    }
}
