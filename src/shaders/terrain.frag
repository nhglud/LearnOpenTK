
#version 430

out vec4 outputColor;

in vec2 texCoord;
in float height;
in vec3 norm;

uniform sampler2D diffuseMap;

void main()
{
    vec3 lightDir = vec3(0.5, -1, 0);
    

    float intensity = max(dot(lightDir, norm), 0) + 0.2;
    intensity = 1.0;

    vec3 col = texture(diffuseMap, texCoord).rgb;


    outputColor = vec4(col, 1.0) * intensity;
}