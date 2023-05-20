#version 330 core
#extension GL_NV_shadow_samplers_cube : enable

in vec3 out_TexCoord;
in vec4 out_Color;
in vec3 out_FragPos;


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
uniform vec3 g_SunDirection;  // Changed from g_SunPosition
uniform float g_SunSize; // New uniform for the sun size

uniform vec3 g_TopColor;
uniform vec3 g_BotColor;
uniform vec3 g_MidColor;
uniform float g_TopY;
uniform vec3 g_Colors[6];  // Array of gradient colors
uniform float g_ColorPositions[6];  // Array of positions for each color in the gradient
uniform float g_SunGlow; // New uniform for the sun glow
out vec4 color;

void main(){
   float ry = out_FragPos.y;
    
    // Normalize the fragment's vertical position
    float normalizedY = (ry + g_TopY) / (2.0 * g_TopY);
    
    vec3 blendedColor = vec3(0.0);
    
    // Interpolate between the gradient colors
    for (int i = 0; i < 5; i++) {
        if (normalizedY >= g_ColorPositions[i] && normalizedY <= g_ColorPositions[i+1]) {
            float t = (normalizedY - g_ColorPositions[i]) / (g_ColorPositions[i+1] - g_ColorPositions[i]);
            blendedColor = mix(g_Colors[i], g_Colors[i+1], t);
            break;
        }
    }
    
    // Add smooth horizon blending
    float horizonBlend = smoothstep(g_ColorPositions[4], g_ColorPositions[5], normalizedY);
    vec3 horizonColor = mix(g_Colors[4], g_Colors[5], horizonBlend);
    blendedColor = mix(blendedColor, horizonColor, horizonBlend);

    // Add sun effect
    float sunFactor = dot(normalize(g_SunDirection), normalize(out_FragPos));
    float sunArea = 1.0 - smoothstep(g_SunSize, g_SunSize + 0.01, 1.0 - sunFactor);
    blendedColor += vec3(1.0, 0.7, 0.0) * sunArea; // Add orange-ish color

    // Add sun glow
    float sunGlowArea = 1.0 - smoothstep(g_SunSize, g_SunSize + g_SunGlow, 1.0 - sunFactor);
    blendedColor += vec3(1.0, 0.8, 0.6) * sunGlowArea; // Add light orange color
    
    color = vec4(blendedColor, 1.0);
}