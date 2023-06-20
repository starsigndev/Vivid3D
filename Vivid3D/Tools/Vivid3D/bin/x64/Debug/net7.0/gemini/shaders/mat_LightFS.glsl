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
in vec3 out_LocalNormal;

uniform sampler2D g_TextureColor;
uniform sampler2D g_TextureNormal;
uniform samplerCube g_TextureShadow;
uniform sampler2D g_TextureSpecular;
uniform vec3 g_LightPosition;
uniform vec3 g_LightDiffuse;
uniform float g_LightCone;
uniform vec3 g_LightSpecular;
uniform float g_LightRange;
uniform vec3 g_LightDir;
uniform vec3 g_CameraPosition;
uniform float g_LightDepth;
uniform vec3 g_CameraDir;
uniform int g_LightType;



out vec4 color;


vec3 light_GetNormal(vec2 uv)
{

    vec3 tNormal = vec3(0.5, 0.0f, 1.0f);
                                                                                 
    tNormal = texture(g_TextureNormal, uv).rgb;


    tNormal = normalize(tNormal * 2.0 - 1.0);
    tNormal.y = -tNormal.y;

    return tNormal;

}

float light_GetDist(vec3 lightPos,vec3 fragPos,float lightRange)
{

    float xd = lightPos.x - fragPos.x;
    float yd = lightPos.y - fragPos.y;
    float zd = lightPos.z - fragPos.z;


    float dis = sqrt(xd * xd + yd * yd + zd * zd);

    if (dis < 0) {
        dis = -dis;
    }

    float dv = dis / lightRange;

    if (dv > 1.0) {
        dv = 1.0;
    }
    dv = 1.0 - dv;


    return dv;


}


float light_GetPointDiff(vec3 TLP,vec3 TFP,vec3 TVP,vec3 tNormal,vec3 lightPos,vec3 fragPos)
{


    vec3 lightDir = normalize(TLP - TFP);
    return max(dot(lightDir, tNormal),0.0);



}

float light_GetDirectional(vec3 lightDir,vec3 TLP,vec3 TFP,vec3 tNormal){

    vec3 lDir = normalize(lightDir);
    return max(dot(lDir, tNormal),0.0);

}


float light_GetPointSpec(vec3 TLP,vec3 TFP,vec3 TVP,vec3 tNormal)
{

    vec3 lightDir = normalize(TLP - TFP);
    vec3 viewDir = normalize(TVP-TFP);
    vec3 reflectDir = reflect(-lightDir,tNormal);
    vec3 halfwayDir = normalize(lightDir+viewDir);

    return pow(max(dot(tNormal,halfwayDir),0.0),32.0);
    

}

float light_GetSpotFalloff(vec3 lightPos,vec3 lightDir,vec3 fragPos,float lightCone)
{
      vec3 lightPixel = normalize(fragPos - lightPos);
      float factor = dot(lightPixel,-lightDir);
       factor *= pow(max(factor,0.0f),lightCone);
    return factor;
}


void main(){

    
    vec4 col = vec4(1, 0, 0, 1);
  
    float light = light_GetDist(g_LightPosition,out_FragPos,g_LightRange);

    vec3 tNormal = light_GetNormal(out_TexCoord.xy);
 
    float spec = 0.0;

    float shadowVal = 0;

    float diffuse = 0;
    float specular = 0;

    if(g_LightType==0){


        float diff = light_GetPointDiff(out_TLP,out_TFP,out_TVP,tNormal,g_LightPosition,out_FragPos);
        spec = light_GetPointSpec(out_TLP,out_TFP,out_TVP,tNormal);

        spec = spec * light;
        light = light * diff;

        diffuse = light;
        specular = spec;


    

    }

    if(g_LightType==1){

           float diff = light_GetPointDiff(out_TLP,out_TFP,out_TVP,tNormal,g_LightPosition,out_FragPos);
        spec = light_GetPointSpec(out_TLP,out_TFP,out_TVP,tNormal);
         float ff = light_GetSpotFalloff(g_LightPosition,g_LightDir,out_FragPos,g_LightCone);
       
        spec = spec * light * ff;

         light = light * diff * ff;

         diffuse = light;
         specular = spec;

    }

      if(g_LightType==2)
    {
        
        float diff = light_GetDirectional(g_LightDir,out_TLP,out_TFP,out_LocalNormal);
        spec = light_GetPointSpec(out_TLP,out_TFP,out_TVP,tNormal); 

        light = diff;
        spec = spec *  diff;

        diffuse = light;
        specular = spec;


    }


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

       vec4 specv= texture2D(g_TextureSpecular,out_TexCoord.xy);

    vec4 fc;

    vec4 tex_Col = texture2D(g_TextureColor,out_TexCoord.xy);

    fc.rgb = ((diffuse + (specular*specv.rgb*g_LightSpecular))) * vec3(shadow,shadow,shadow);
    fc.a = 1.0;

    fc.rgb *= out_Color.rgb * g_LightDiffuse;

    fc = fc * tex_Col;


    color = fc;

}