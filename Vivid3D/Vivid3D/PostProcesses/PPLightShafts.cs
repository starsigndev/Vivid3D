using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Shaders;
using Vivid.Texture;

namespace Vivid.PostProcesses
{
    public class PPLightShaftsFX : ShaderModule
    {

        public float Exposure
        {
            get;
            set;
        }

        public float Decay
        {
            get;
            set;
        }

        public float Density
        {
            get;
            set;
        }

        public float Weight
        {
            get;
            set;
        }

        public Vector3 LightPosition
        {
            get;
            set;
        }



        public PPLightShaftsFX() : base("gemini/shaders/pp_lightShaftsVS.glsl","gemini/shaders/pp_lightShaftsFS.glsl")
        {

            Exposure = 0.3f;
            Decay = 0.96815f;
            Density = 0.962f;
            Weight = 0.587f;
            LightPosition = new Vector3(0, 0, 0);
        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            SetUni("g_TextureColor", 0);
            SetUni("g_Exposure", Exposure);
            SetUni("g_Decay", Decay);
            SetUni("g_Density", Density);
            SetUni("g_Weight", Weight);
            SetUni("g_LightPosition", LightPosition);

            var Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f, 1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
        }

    }
    public class PPLightShafts : PostProcess
    {

        private PPLightShaftsFX lightShaftsFX;
        private PPCombineFX combineFX;

        public PPLightShafts(Vivid.Scene.Scene scene) : base(scene, 4)
        {
            lightShaftsFX = new PPLightShaftsFX();
            combineFX = new PPCombineFX();
            int a = 5;
        }

        public override Texture2D Process()
        {
            return base.Process();
        }

        public override void ProcessAndDraw()
        {

            Scene.RenderShadows();

            BindTarget(3);
            RenderLit();
            ReleaseTarget(3);

            //base.ProcessAndDraw();
            var shaftsTex = GenerateLightShafts();



            shaftsTex.Bind(1);
            DrawTarget(3,combineFX);
            shaftsTex.Unbind(1);

        }

        public Texture2D GenerateLightShafts()
        {

            BindTarget(0);
            RenderHalo(0);
            ReleaseTarget(0);

           // DrawTarget(0);

           
            // OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);
            //lightShaftsFX.Light = Scene.Lights[0];
            Matrix4 wvp = Scene.Lights[0].WorldMatrix * Scene.MainCamera.WorldMatrix * Scene.MainCamera.Projection;
            //wvp = wvp.Transpose;
            
           
           
            Vector3 pos = Vector3.TransformPerspective(new Vector3(0, 0, 0), wvp);
            pos.X = pos.X;// / pos.Z;
            pos.X = pos.X;
            pos.X = 0.5f + pos.X * 0.5f;
            pos.Y = 0.5f + pos.Y * 0.5f;
          //  pos.Y = 1.0f - pos.Y;

            lightShaftsFX.LightPosition = new Vector3(pos.X, pos.Y, 0);

            BindTarget(1);
            DrawTarget(0, lightShaftsFX);
            ReleaseTarget(1);

            return GetTexture(1);

            



        }



    }
}
