#version 330 core

in vec2 out_TexCoord;
in vec4 out_Color;

out vec4 color;

uniform sampler2D g_TextureColor;
uniform float g_ColorLimit;


void main(){



    vec4 col = texture(g_TextureColor,out_TexCoord);



    float cv = col.r + col.g + col.b;
    cv = cv / 3.0;

    if(cv<g_ColorLimit){

      col.rgb = vec3(0,0,0);
      //  return;

    }

    color = col;

}