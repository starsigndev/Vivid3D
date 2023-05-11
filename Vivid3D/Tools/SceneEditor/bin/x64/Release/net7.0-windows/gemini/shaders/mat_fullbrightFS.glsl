#version 330 core

in vec3 out_TexCoord;
in vec4 out_Color;


uniform sampler2D g_ColorTexture;

out vec4 color;

void main(){
    
    color = texture(g_ColorTexture,out_TexCoord.xy);

}