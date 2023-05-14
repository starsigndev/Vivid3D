#version 330 core

in vec2 out_TexCoord;
in vec4 out_Color;

out vec4 color;

uniform sampler2D g_TextureColor;
uniform float g_Exposure;
uniform float g_Decay;
uniform float g_Density;
uniform float g_Weight;
uniform vec3 g_LightPosition;


void main(){

     int num_samples=60;

    vec2 tc = out_TexCoord;


    vec2 deltaTC = (tc-g_LightPosition.xy);
    deltaTC *= 1.0/float(num_samples);
    float idecay = 1.0f;



    vec2 uv = out_TexCoord;

    vec3 fcol = vec3(0,0,0);


    vec4 godRayColor = texture(g_TextureColor,tc)*0.4;


    for(int i=0;i<num_samples;i++){

        tc-=deltaTC;
        vec4 samp = texture(g_TextureColor,tc)*0.4;
        samp*=idecay*g_Weight;
        godRayColor+=samp;
        idecay*=g_Decay;


    }

    fcol.rgb = godRayColor.rgb * g_Exposure;

    color = vec4(fcol.r,fcol.g,fcol.b,1.0);

 //   PSOut.Color = float4(fcol,1.0);

   // vec3 col1 = texture(g_TextureColor,out_TexCoord).rgb * g_I1;
  //  vec3 col2 = texture(g_TextureColor2,out_TexCoord).rgb * g_I2;
  

  //  color.rgb = col1+col2;
   // color.a = 1.0;

}