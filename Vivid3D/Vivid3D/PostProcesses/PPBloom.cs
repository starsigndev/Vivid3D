﻿using OpenTK.Mathematics;
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

    public class PPCombineFX : ShaderModule
    {
        public float I1
        {
            get;
            set;
        }

        public float I2
        {
            get;
            set;
        }

        public PPCombineFX() : base("gemini/shaders/pp_combineVS.glsl","gemini/shaders/pp_combineFS.glsl")
        {
            I1 = 0.95f;
            I2 = 1.0f;
        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            SetUni("g_TextureColor", 0);
            SetUni("g_TextureColor2", 1);
            SetUni("g_I1",I1);
            SetUni("g_I2", I2);

            var Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f, 1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
        }
    }

    public class PPBlurFX : ShaderModule
    {
        public float Blur
        {
            get;
            set;
        }

        public Vector2 ScreenSize
        {
            get;
            set;
        }

        public PPBlurFX() : base("gemini/shaders/pp_blurVS.glsl","gemini/shaders/pp_blurFS.glsl")
        {
            Blur = 0.1f;
        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            SetUni("g_TextureColor", 0);
            SetUni("g_Blur", Blur);
            SetUni("g_ScreenSize", ScreenSize);

            var Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f, 1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
        }

    }
    public class PPColorLimitFX : ShaderModule
    {

        public float ColorLimit
        {
            get;
            set;
        }
  
        public PPColorLimitFX() : base("gemini/shaders/pp_collimitVS.glsl","gemini/shaders/pp_collimitFS.glsl")
        {
            ColorLimit = 0.75f;
        }

        public override void SetUniforms()
        {
            //base.SetUniforms();
            //base.SetUniforms();
            SetUni("g_TextureColor", 0);
            SetUni("g_ColorLimit",ColorLimit);
            var Projection = Matrix4.CreateOrthographicOffCenter(0, VividApp.FrameWidth, VividApp.FrameHeight, 0, -1.0f, 1.0f);

            //base.SetUniforms();
            SetUni("g_Projection", Projection);
        }

    }
    public class PPBloom : PostProcess
    {

        PPColorLimitFX colorLimitFX;
        PPBlurFX blurFX;
        PPCombineFX combineFX;
        public float ColorLimit
        {
            get;
            set;
        }

        public float BlurAmount
        {
            get;
            set;
        }

        public PPBloom(Vivid.Scene.Scene scene) : base(scene, 3)
        {

            //FX1 = new OutlineFX();
            colorLimitFX = new PPColorLimitFX();
            colorLimitFX.ColorLimit = 0.45f;
            ColorLimit = 0.85f;
            BlurAmount = 0.05f;
            blurFX = new PPBlurFX();
            blurFX.Blur = 0.001f;
            combineFX = new PPCombineFX();
            
            int a = 5;
        }


        public override Vivid.Texture.Texture2D Process(Texture2D bb)
        {
            //base.Process();
            GenerateBloom(bb);

            return GetTexture(1);
            //DrawTarget(1);



        }

        public override void ProcessAndDraw()
        {


            //GenerateBloom();

           // DrawTarget(1);

        }

        private void GenerateBloom(Texture2D bb)
        {

            colorLimitFX.ColorLimit = ColorLimit;

            blurFX.Blur = BlurAmount;

            BindTarget(0);
            //RenderLit();
            DrawTex(bb, null);
            ReleaseTarget(0);


            BindTarget(1);
            DrawTarget(0, colorLimitFX);
            ReleaseTarget(1);

            BindTarget(2);
            blurFX.ScreenSize = new Vector2(Targets[2].Width, Targets[2].Height);
            DrawTarget(1, blurFX);
            ReleaseTarget(2);

            BindTarget(1);
            GetTexture(2).Bind(1);
            DrawTarget(0, combineFX);
            GetTexture(2).Unbind(1);
            ReleaseTarget(1);
        }
    }   
}
