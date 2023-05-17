#version 330 core
#extension GL_NV_shadow_samplers_cube : enable

in vec3 out_TexCoord;
in vec4 out_Color;
in vec3 out_FragPos;


uniform sampler2D g_TextureColor;
uniform sampler2D g_TextureNormal;
uniform samplerCube g_TextureShadow;
uniform sampler2D g_TextureSpecular;
uniform vec3 g_LightPosition;
uniform vec3 g_LightDiffuse;
uniform vec3 g_LightSpecular;
uniform float g_LightRange;
uniform vec3 g_CameraPosition;
uniform float g_LightDepth;
uniform vec3 g_CameraDir;

uniform vec3 g_TopColor;
uniform vec3 g_BotColor;
uniform float g_TopY;



out vec4 color;

void main(){
 
    float ry = out_FragPos.y;

    float cv = ry / g_TopY;

    vec3 c1 = g_TopColor * cv;
    vec3 c2 = g_BotColor * (1.0-cv);

    vec3 fcol = (c1+c2);

    color = vec4(fcol,1);

}