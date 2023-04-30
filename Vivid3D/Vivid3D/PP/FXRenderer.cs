using System.Runtime.InteropServices;
using Vivid.Texture;

namespace Vivid.PP
{
    public class FXRenderer
    {
        [DllImport("gembridge.dll")]
        public static extern IntPtr gem_CreatePPRenderer();

        [DllImport("gembridge.dll")]
        public static extern void gem_PPRendererLimit(IntPtr pr, IntPtr tex, int w, int h, float limit);

        [DllImport("gembridge.dll")]
        public static extern void gem_PPRendererBlur(IntPtr pr, IntPtr tex, int w, int h, float blur);

        [DllImport("gembridge.dll")]
        public static extern void gem_PPRendererCombine(IntPtr pr, IntPtr t1, IntPtr p2, int w, int h, float i1, float i2);

        [DllImport("gembridge.dll")]
        public static extern void gem_PPRendererGodRays(IntPtr pr, IntPtr t1, int w, int h, IntPtr sp, float exp, float decay, float den, float weight);

        private IntPtr handle;

        public FXRenderer()
        {
            handle = gem_CreatePPRenderer();
        }

        public void RenderGodRays(Texture2D tex, int w, int h, OpenTK.Mathematics.Vector3 sun_pos, float exp, float decay, float density, float weight)
        {
            //gem_PPRendererGodRays(handle, tex.CObj,w,h, sun_pos.handle, exp, decay, density, weight);
        }

        public void RenderColorLimit(Texture2D tex, int w, int h, float limit)
        {
            // gem_PPRendererLimit(handle, tex.CObj, w, h, limit);
        }

        public void RenderBlur(Texture2D tex, int w, int h, float blur)
        {
            // gem_PPRendererBlur(handle, tex.CObj, w, h, blur * 0.01f);
        }

        public void RenderCombine(Texture2D t1, Texture2D t2, int w, int h, float i1, float i2)
        {
            //gem_PPRendererCombine(handle, t1.CObj, t2.CObj, w, h, i1, i2);
        }
    }
}