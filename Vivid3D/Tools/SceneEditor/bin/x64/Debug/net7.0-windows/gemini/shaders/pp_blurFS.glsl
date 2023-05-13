#version 330 core

in vec2 out_TexCoord;
in vec4 out_Color;

out vec4 color;

uniform sampler2D g_TextureColor;
uniform float g_Blur;


void main(){



    //vec4 col = texture(g_TextureColor,out_TexCoord);

    vec3 fcol = vec3(0,0,0);

    float be = 0.1f;

    float samples=0;

    for(int y=-5;y<5;y++){

        for(int x=-5;x<5;x++){

            vec2 uv = out_TexCoord;
            uv.x = uv.x + float(x)*g_Blur*be;
            uv.y = uv.y + float(y)*g_Blur*be;

            if(uv.x>=0.0 && uv.x<=1.0 && uv.y>=0.0 && uv.y<=1.0f){

                fcol = fcol+texture(g_TextureColor,uv).rgb;
                samples = samples+1;

            }
            
        }

    }

    fcol = fcol / samples;

    color.rgb = fcol.rgb;
    color.a = 1.0;

}