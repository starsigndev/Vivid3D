#version 330 core


layout(location = 0) in vec3 g_Position;
layout(location = 1) in vec4 g_Color; 
layout(location = 2) in vec3 g_TexCoord;
layout(location = 3) in vec3 g_Normal; 
layout(location = 4) in vec3 g_BiNormal;
layout(location = 5) in vec3 g_Tangent;
layout(location = 6) in vec4 g_BoneIds;
layout(location = 7) in vec4 g_Weights;

const int MAX_BONES = 100;
const int MAX_BONE_INFLUENCE = 4;
uniform mat4 g_finalBones[MAX_BONES];

uniform mat4 g_Projection;
uniform mat4 g_Model;
uniform mat4 g_View;
uniform vec3 g_CameraPosition;
uniform vec3 g_LightPosition;

out vec3 out_TexCoord;
out vec4 out_Color;
out vec3 out_FragPos;
out vec3 out_TLP;
out vec3 out_TVP;
out vec3 out_TFP;
out vec3 out_rPos;
out vec3 out_Norm;
out vec3 out_reflectVector;
out vec3 out_pass_normal;
out mat3 out_normMat;
out mat3 out_TBN;
out vec3 out_Vert;

void main(){

    out_TexCoord = g_TexCoord;
    out_Color = g_Color;

    //

  mat4 S = mat4(0.f);
    for (int i = 0; i < 4; ++i)
    {
        if (g_BoneIds[i] >= 0)
        {
            S += (g_finalBones[int(g_BoneIds[i])] * g_Weights[i]);
        }
    }
    mat3 S_ = transpose(inverse(mat3(S)));
    mat4 MVP = g_Projection * g_View * g_Model;
    gl_Position = MVP * S * vec4(g_Position, 1.f);


    //

    out_FragPos = vec3(g_Model * S * vec4(g_Position, 1.f));

    out_Vert = vec3(g_Model * S * vec4(g_Position, 1.f));

    mat3 normalMatrix = transpose(inverse(mat3(g_Model * S)));

    out_normMat = normalMatrix;

    vec3 T = normalize(normalMatrix * g_Tangent);
	vec3 N = normalize(normalMatrix * g_Normal);

	vec4 worldPos = g_Model * vec4(g_Position,1.0);

	out_pass_normal = N;


    //
    
    vec3 unitNormal = normalize(N);

	vec3 viewVector = normalize(worldPos.xyz - g_CameraPosition);

	out_reflectVector = reflect(viewVector, unitNormal);

	out_Norm = g_Position;
	
	T = normalize(T-dot(T,N) *N);
	
	vec3 B = cross(N,T);

	out_TBN = transpose(mat3(T,B,N));


	out_TLP = out_TBN * g_LightPosition;
	out_TVP = out_TBN * g_CameraPosition;
	out_TFP = out_TBN * out_FragPos;

  
   mat4 viewModel = g_View * g_Model;



}