
#version 430

out vec4 outputColor;

in vec2 texCoord;
in float height;
in vec3 norm;
uniform vec3 color;

void main()
{
    vec3 lightDir = vec3(1, -1, 0);

    float intensity = max(dot(lightDir, norm), 0) + 0.2;

    vec3 col = vec3(height, 0, 0);
    outputColor = vec4(norm, 1.0) * intensity;
}