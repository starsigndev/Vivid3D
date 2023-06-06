#version 330 core
#extension GL_NV_shadow_samplers_cube : enable

in vec3 out_TexCoord;
in vec4 out_Color;





out vec4 color;

void main(){

   vec4 fc = out_Color;

    color = fc;

}