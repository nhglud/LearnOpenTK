#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;
uniform vec2 resolution;



void main()
{
    
    vec2 uv = texCoord;
    

    float pixelsize = 6.0;

    vec2 fragCoord = resolution * uv;
    
    vec2 blocks = resolution / pixelsize;
    vec2 blockIndex = floor(fragCoord / pixelsize);
    vec2 samplePixel = (blockIndex + 0.5) * pixelsize;
    vec2 sampleUV = samplePixel / resolution;


    vec3 col = texture(screenTexture, sampleUV).rgb;

    outputColor = vec4(col, 1.0);

}