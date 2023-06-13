#version 330 core

// Input vertex data, different for all executions of this shader.
layout(location = 0) in vec3 g_Position;
layout(location = 1) in vec2 g_TexCoord;
layout(location = 2) in vec4 g_Color;

uniform mat4 g_Projection;

// Output data ; will be interpolated for each fragment.
out vec2 out_TexCoord;
out vec4 out_Color;

void main(){

	out_TexCoord = g_TexCoord;
	out_Color = g_Color;
	gl_Position = g_Projection * vec4(g_Position,1);
	
}



