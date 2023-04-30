using Vivid.App;
using Vivid.Draw;
using Vivid.PP;
using Vivid.RenderTarget;
using Vivid.Scene;

namespace Vivid.SceneComposer
{
    public class SceneRenderer
    {
        public const int MAX_LIGHTS = 8;
        public const int MAX_AUX = 8;
        public List<RenderTarget2D> LightTargets = new List<RenderTarget2D>();
        public List<RenderTarget2D> AuxTargets = new List<RenderTarget2D>();

        public float BloomColorLimit = 0.75f;
        public float BloomBlurAmount = 0.35f;
        public bool BloomOn = true;

        public Scene.Scene Scene
        {
            get;
            set;
        }

        public Scene.OctreeScene OCScene
        {
            get;
            set;
        }

        public List<Light> Lights
        {
            get;
            set;
        }

        private FXRenderer fx;
        private SmartDraw draw;

        public void CreateLightTargets(int number)
        {
            for (int i = 0; i < number; i++)
            {
                LightTargets.Add(new RenderTarget2D(Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight));
            }
        }

        public void CreateAuxTargets(int number)
        {
            for (int i = 0; i < number; i++)
            {
                AuxTargets.Add(new RenderTarget2D(VividApp.FrameWidth, VividApp.FrameHeight));
            }
        }

        public void GetLights(List<Light> lights)
        {
            foreach (var light in lights)
            {
                Lights.Add(light);
            }
        }

        public SceneRenderer(Scene.Scene scene)
        {
            Lights = new List<Light>();
            Scene = scene;
            GetLights(scene.Lights);
            Scene.Lights.Clear();
            CreateLightTargets(MAX_LIGHTS);
            CreateAuxTargets(MAX_AUX);
            draw = new SmartDraw();
            fx = new FXRenderer();
        }

        public SceneRenderer(Scene.OctreeScene scene)
        {
            Lights = new List<Light>();
            OCScene = scene;
            GetLights(scene.Scene.Lights);

            scene.Scene.Lights.Clear();
            CreateLightTargets(MAX_LIGHTS);
            CreateAuxTargets(MAX_AUX);
            draw = new SmartDraw();
            fx = new FXRenderer();
        }

        public void RenderLight(Light light, RenderTarget2D target)
        {
        }

        public void RenderFinal()
        {
            RenderShadows();

            int index = 0;
            foreach (var light in Lights)
            {
                BindLightTarget(index);
                RenderScene(light);
                ReleaseLightTarget(index);
                index++;
            }

            AuxTargets[0].Bind();
            Draw(LightTargets[0], Vivid.App.VividApp.FrameWidth, Vivid.App.VividApp.FrameHeight);
            for (int i = 1; i < Lights.Count; i++)
            {
                DrawAdd(LightTargets[i], VividApp.FrameWidth, VividApp.FrameHeight);
            }
            AuxTargets[0].Release();

            if (BloomOn)
            {
                var tex = GenerateBloom(AuxTargets[0]);

                Draw(tex, VividApp.FrameWidth, VividApp.FrameHeight);
            }
            else
            {
                Draw(AuxTargets[0], VividApp.FrameWidth, VividApp.FrameHeight);
            }
            //Draw(AuxTargets[0], GeminiApp.FrameWidth, GeminiApp.FrameHeight);
        }

        private Vivid.RenderTarget.RenderTarget2D GenerateBloom(RenderTarget2D rt)
        {
            AuxTargets[1].Bind();

            fx.RenderColorLimit(rt.GetTexture(), VividApp.FrameWidth, VividApp.FrameHeight, BloomColorLimit);
            AuxTargets[1].Release();

            Vivid.Texture.Texture2D tex = AuxTargets[1].GetTexture();

            AuxTargets[2].Bind();
            fx.RenderBlur(tex, VividApp.FrameWidth, VividApp.FrameHeight, BloomBlurAmount);
            AuxTargets[2].Release();

            tex = AuxTargets[2].GetTexture();

            AuxTargets[1].Bind();
            fx.RenderCombine(tex, rt.GetTexture(), VividApp.FrameWidth, VividApp.FrameHeight, 0.5f, 0.5f);
            AuxTargets[1].Release();

            return AuxTargets[1];
        }

        private void DrawLight(int i, int w, int h)
        {
            Draw(LightTargets[i], w, h);
        }

        private void Draw(RenderTarget2D target, int w, int h)
        {
            draw.SetMode(0);
            draw.Begin();
            //draw.DrawTexture(target.GetTexture(), 0, 0, w, h, 1, 1, 1, 1);
            draw.End();
        }

        private void DrawAdd(RenderTarget2D target, int w, int h)
        {
            draw.SetMode(1);
            draw.Begin();
            //draw.DrawTexture(target.GetTexture(), 0, 0, w, h, 1, 1, 1, 1);
            draw.End();
        }

        private void BindLightTarget(int i)
        {
            LightTargets[i].Bind();
        }

        private void ReleaseLightTarget(int i)
        {
            LightTargets[i].Release();
        }

        public void RenderShadows()
        {
            if (Scene != null)
            {
                AddLights();
                Scene.RenderShadows();
                RemoveLights();
            }
            else if (OCScene != null)
            {
                AddLights();
                OCScene.RenderShadows();
                RemoveLights();
            }
        }

        public void AddLights()
        {
            if (Scene != null)
            {
                Scene.Lights = Lights;
            }
            if (OCScene != null)
            {
                OCScene.Scene.Lights = Lights;
            }
        }

        public void RemoveLights()
        {
            if (Scene != null)
            {
                Scene.Lights = new List<Light>();
            }
            else if (OCScene != null)
            {
                OCScene.Scene.Lights = new List<Light>();
            }
        }

        public void RenderScene(Light light)
        {
            if (Scene != null)
            {
                Scene.Lights.Add(light);
                Scene.Render();
                Scene.Lights.Clear();
            }
            if (OCScene != null)
            {
                //OCScene.Scene.Lights.Add(light);
                OCScene.RenderLight(light);
                //OCScene.Scene.Lights.Clear();
            }
        }
    }
}