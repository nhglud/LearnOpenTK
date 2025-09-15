#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;

vec3 Quantize(vec3 color, int levels)
{
    return floor(color * levels) / levels; 

}



void main()
{
    
    vec2 uv = texCoord;
    

    vec4 scene = texture(screenTexture, uv);
    vec3 col = Quantize(scene.rgb, 5);


    outputColor = vec4(col, 1.0);

}