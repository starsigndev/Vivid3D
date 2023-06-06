#version 330 core

in vec2 out_TexCoord;
in vec4 out_Color;

out vec4 color;

uniform sampler2D g_TextureColor;
uniform sampler2D g_TextureColor2;
uniform float g_I1;
uniform float g_I2;


void main(){

    vec3 col1 = texture(g_TextureColor,out_TexCoord).rgb * g_I1;
    vec3 col2 = texture(g_TextureColor2,out_TexCoord).rgb * g_I2;
  
    color.rgb = col1+col2;
    color.a = 1.0;

}