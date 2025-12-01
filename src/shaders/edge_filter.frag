
#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;
uniform float time;


void main()
{
    ivec2 texSize = textureSize(screenTexture, 0);
    vec2 stepSize = 1.0 / texSize;


     vec2 offsets[9] = vec2[](
        vec2(-stepSize.x,  -stepSize.y),
        vec2( 0.0f,         -stepSize.y),
        vec2( stepSize.x,  -stepSize.y),
        vec2(-stepSize.x,   0.0f),
        vec2( 0.0f,          0.0f),
        vec2( stepSize.x,   0.0f),
        vec2(-stepSize.x,  +stepSize.y),
        vec2( 0.0f,         +stepSize.y),
        vec2( stepSize.x,   stepSize.y)
    );

     float kernel[9] = float[](
        -1, -1, -1,
        -1,  9, -1,
        -1, -1, -1
    );

    vec4 sum = vec4(0.0);
    for(int i = 0; i < 9; i++) {
        sum += texture(screenTexture, texCoord + offsets[i]) * kernel[i];
    }

    sum.a = 1.0;

    outputColor = sum;

}