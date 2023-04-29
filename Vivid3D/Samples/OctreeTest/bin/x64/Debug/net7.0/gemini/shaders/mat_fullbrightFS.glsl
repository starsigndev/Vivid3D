#version 330 core
#extension GL_NV_shadow_samplers_cube : enable

in vec3 out_TexCoord;
in vec4 out_Color;


uniform sampler2D g_ColorTexture;

out vec4 color;

void main(){
    
    color = texture(g_ColorTexture,out_TexCoord.xy);

}