
#version 430

out vec4 outputColor;

in vec2 texCoord;
in float height;
in vec3 norm;


uniform sampler2D noiseMap;

float rand(vec2 co){
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}

void main()
{
    vec3 lightDir = vec3(1, -1, 0);
    
    float noise = texture(noiseMap, 20 * texCoord).r;

    float intensity = max(dot(lightDir, norm + 0.1 * noise), 0) + 0.2;

    vec3 col = vec3(0.1, 0.4, 0.3);;

    

    if(height > 0.25 + 0.1 * noise || max(dot(norm, vec3(0, 1, 0)), 0.0) < 0.05) 
    {
        col = vec3(0.24, 0.25, 0.26);
    }
    

    if(height > 0.4 + 0.1 * noise)
    {
        col = vec3(1);
    }
    
    if(height < 0.01)
    {
        col = vec3(0.1, 0.12, 0.2);
    }

    
    
    outputColor = vec4(col, 1.0) * intensity;
}