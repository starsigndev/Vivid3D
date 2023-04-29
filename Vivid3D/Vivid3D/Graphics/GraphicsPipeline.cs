using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Graphics
{
    public class GraphicsPipeline
    {
        [DllImport("gembridge.dll")]
        public static extern IntPtr gem_CreateGP(string name);
        [DllImport("gembridge.dll")]
        public static extern void gem_GPStartDef(IntPtr h);
        [DllImport("gembridge.dll")]
        public static extern void gem_GPSetShaders(IntPtr h, string v, string f, int const_size);
        [DllImport("gembridge.dll")]
        public static extern void gem_GPSetLayoutStandard(IntPtr h);
        [DllImport("gembridge.dll")]
        public static extern void gem_GPSetVarsStandard(IntPtr h);
        [DllImport("gembridge.dll")]
        public static extern void gem_GPEndDef(IntPtr h);

        public IntPtr handle;



        public GraphicsPipeline(string vertex,string frag)
        {

            handle = gem_CreateGP("Mesh");
            gem_GPStartDef(handle);
            gem_GPSetShaders(handle,vertex, frag,32);
            gem_GPSetLayoutStandard(handle);
            gem_GPSetVarsStandard(handle);
            gem_GPEndDef(handle);


        }

        public virtual void InitPipeline()
        {


          
            
        }

    }
}
