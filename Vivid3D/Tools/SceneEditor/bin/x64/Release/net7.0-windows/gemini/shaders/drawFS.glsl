#version 330 core

in vec2 UV;
in vec4 col;

out vec4 color;

uniform sampler2D tR;


void main(){

    vec4 co = texture(tR,UV) * col;

    if(co.a<0.1)
    {
        discard;
    }


    //co.rgb = vec3(1,1,1);

    color = co;

}