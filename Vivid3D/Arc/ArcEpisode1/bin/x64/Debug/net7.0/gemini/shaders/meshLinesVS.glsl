#version 330 core

// Input vertex data, different for all executions of this shader.
layout(location = 0) in vec3 g_Position;
layout(location = 1) in vec4 g_Color; 


uniform mat4 g_Projection;
uniform mat4 g_Model;
uniform mat4 g_View;

out vec4 out_Color;


void main(){

    out_Color = g_Color;
  
    gl_Position = g_Projection * g_View * g_Model* vec4(g_Position,1.0);

}