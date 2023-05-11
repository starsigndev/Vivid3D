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



out vec4 color;

void main(){

     float dis = abs(length(out_FragPos-g_LightPosition));

    float dv = dis/g_LightRange;

    if(dv>1.0) dv = 1.0;
    dv = 1.0-dv;

    vec3 df = vec3(dv,dv,dv);

    //
    vec2 uv;

    uv.x = out_TexCoord.x;
    uv.y = 1.0-out_TexCoord.y;

    vec3 normal = vec3(0.5,0.5,1);

      normal = texture2D(g_TextureNormal,uv).rgb;

    normal = normalize(normal * 2.0 - 1.0);

    vec3 ref_Norm = out_reflectVector;




    vec3 cTex = texture(g_TextureColor,uv).rgb;

    //

     vec3 lightDir = normalize(out_TLP - out_TFP);

    float diff = max(dot(lightDir,normal),0.0);

    vec3 diffuse = vec3(diff,diff,diff) * g_LightDiffuse;

    diffuse = diffuse * cTex;

    vec3 viewDir = normalize(out_TVP-out_TFP);
    vec3 reflectDir = reflect(-lightDir,normal);
    vec3 halfwayDir = normalize(lightDir+viewDir);

    float spec = pow(max(dot(normal,halfwayDir),0.0),32.0);

    spec = spec;

    vec3 specular = ((g_LightSpecular) * spec); 

   
    vec3 fragToLight = out_FragPos - g_LightPosition;
    float currentDepth = length(fragToLight);
    fragToLight = normalize(fragToLight);

    currentDepth = currentDepth / g_LightRange;
    
    float shadow = 0.0;

    float closestDepth = textureCube(g_TextureShadow,fragToLight).r;
     
    if( (currentDepth - 0.0005) > closestDepth){
        shadow = 0.0;
    }else{
        shadow = 1.0;
    }

    vec4 specv= texture2D(g_TextureSpecular,uv);

    vec4 fc;


    fc.rgb = ((diffuse + (specular*specv.rgb))*df) * vec3(shadow,shadow,shadow);
    fc.a = 1.0;

    fc.rgb *= out_Color.rgb;
    

    color = fc;

}