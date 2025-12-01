
#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;
uniform float time;


void main()
{
    
    vec2 uv = texCoord;
    
    uv.x += sin(uv.y * 8.0 * 3.14159 + time) * 0.01;
    vec3 col = texture(screenTexture, uv).rgb;

    outputColor = vec4(col, 1.0);

}