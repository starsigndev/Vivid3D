#version 330 core

// Input vertex data, different for all executions of this shader.
layout(location = 0) in vec3 g_Position;
layout(location = 1) in vec4 g_Color; 
layout(location = 2) in vec3 g_TexCoord;
layout(location = 3) in vec3 g_Normal; 
layout(location = 4) in vec3 g_BiNormal;
layout(location = 5) in vec3 g_Tangent;

uniform mat4 g_Projection;
uniform mat4 g_View;
uniform mat4 g_Model;
uniform vec3 g_CameraPosition;
uniform vec3 g_LightPosition;



out vec3 out_TexCoord;
out vec4 out_Color;
out vec3 out_FragPos;
out vec3 out_Pos;



void main(){

    out_TexCoord = g_TexCoord;
    out_Color = g_Color;
   
    out_FragPos = vec3(g_Model * vec4(g_Position,1.0));

    gl_Position = g_Projection * g_View * g_Model* vec4(g_Position,1.0);

}