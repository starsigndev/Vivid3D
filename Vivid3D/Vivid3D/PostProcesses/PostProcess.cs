using Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Draw;
using Vivid.RenderTarget;
using Vivid.Scene;
using Vivid.Shaders;
using Vivid.Texture;

namespace Vivid.PostProcesses
{
    public class PostProcess
    {
    
        public List<RenderTarget2D> Targets
        {
            get;
            set;
        }

        public Vivid.Scene.Scene Scene
        {
            get;
            set;
        }

        public Vivid.Scene.Entity Specific
        {
            get;
            set;
        }

        public SmartDraw Draw
        {
            get;
            set;
        }

        public PostProcess(Scene.Scene scene, int num_targets)
        {
            Scene = scene;
            Targets = new List<RenderTarget2D>();
            Specific = null;
            for(int i=0;i<num_targets; i++)
            {
                Targets.Add(new RenderTarget2D(Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight));
            }
            Draw = new SmartDraw();
        }
 
        public PostProcess(Scene.Entity entity,int num_targets)
        {
            Scene = null;
            Targets = new List<RenderTarget2D>();
            Specific = entity;
            for (int i = 0; i < num_targets; i++)
            {
                Targets.Add(new RenderTarget2D(Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight));
            }
            Draw = new SmartDraw();
        }

        public void BindTarget(int index)
        {

            Targets[index].Bind();

        }

        public void ReleaseTarget(int index)
        {
            Targets[index].Release();
        }
    
        public Texture2D GetTexture(int index)
        {
            return Targets[index].GetTexture();
        }

        public virtual void Process()
        {

        }

        public void RenderDepth()
        {
            if (Specific != null)
            {
                Specific.RenderDepth(Scene.MainCamera);
            }
            else
            {
                Scene.RenderDepth();
            }
        }

        public void RenderLit()
        {
            //Scene.RenderShadows();
            Scene.Render();
        }

        public void DrawTarget(int index,ShaderModule shader=null)
        {

            Draw.Begin();
            Draw.Draw(Targets[index].GetTexture(), new Maths.Rect(0, Vivid.App.VividApp.FrameHeight, VividApp.FrameWidth, -VividApp.FrameHeight), new Maths.Color(1, 1, 1, 1));
            Draw.End(shader);

        }

    }

}
