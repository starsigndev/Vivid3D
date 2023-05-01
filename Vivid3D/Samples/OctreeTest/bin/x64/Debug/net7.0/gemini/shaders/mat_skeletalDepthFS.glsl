#version 330 core
#extension GL_NV_shadow_samplers_cube : enable

in vec3 out_TexCoord;
in vec4 out_Color;
in vec3 out_FragPos;
in vec3 out_TLP;
in vec3 out_TVP;
in vec3 out_TFP;
in vec3 out_rPos;
in vec3 out_Norm;
in vec3 out_reflectVector;
in vec3 out_pass_normal;
in mat3 out_normMat;
in mat3 out_TBN;
in vec3 out_Vert;

uniform sampler2D g_TextureColor;
uniform sampler2D g_TextureNormal;
uniform samplerCube g_TextureShadow;
uniform sampler2D g_TextureSpecular;
uniform vec3 g_LightPosition;
uniform vec3 g_LightDiffuse;
uniform vec3 g_LightSpecular;
uniform float g_LightRange;
uniform vec3 g_CameraPosition;
uniform float g_LightDepth;
uniform vec3 g_CameraDir;
uniform float g_CameraMaxZ;


out vec4 color;

void main(){

    float dis = length(out_Vert-g_CameraPosition);

	dis = dis / g_CameraMaxZ;

	if(dis<0){
		dis=0;
	}
	if(dis>1){
		dis = 1;
	}

    vec4 fc;

    fc.rgb = vec3(dis,dis,dis);
    fc.a = 1.0;

    color = fc;

}