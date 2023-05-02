#version 330 core

uniform vec3 mCol;
uniform vec3 g_CameraPosition;
uniform float g_CameraMinZ;
uniform float g_CameraMaxZ;




in vec3 out_Vert;

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