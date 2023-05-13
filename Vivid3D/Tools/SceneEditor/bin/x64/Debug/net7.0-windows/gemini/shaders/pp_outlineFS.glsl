#version 330 core

in vec2 out_TexCoord;
in vec4 out_Color;

out vec4 color;

uniform sampler2D g_TextureDepth;


void main(){



    //vec4 co = texture(g_TextureDepth,out_TexCoord);

    float c = texture(g_TextureDepth,out_TexCoord).r;
    float c1 = texture(g_TextureDepth,out_TexCoord+vec2(-0.003,0)).r;
    float c2 = texture(g_TextureDepth,out_TexCoord+vec2(0,-0.003)).r;
    


    vec4 co;

    if(abs(c1-c)>0.01 || abs(c2-c)>0.01)
    {
        co.rgb = vec3(0,1,0);
    }else{
        discard;
    }


    co.a = 0;


    
    color = co;

}