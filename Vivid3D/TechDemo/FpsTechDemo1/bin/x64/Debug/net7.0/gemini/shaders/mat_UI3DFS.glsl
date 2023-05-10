#version 330 core

in vec3 out_TexCoord;
in vec4 out_Color;


uniform sampler2D g_ColorTexture;

out vec4 color;

void main(){
    
    vec4 col = texture(g_ColorTexture,out_TexCoord.xy);

    if(col.r<0.1)
    {
        discard;
    }

    color.rgb = col.rgb;
    color.a = 1.0;

}