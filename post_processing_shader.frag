#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;

void main()
{
    outputColor = vec4(1.0) - texture(screenTexture, texCoord);
}