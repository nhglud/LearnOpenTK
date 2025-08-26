#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;



void main()
{
    
    vec2 uv = texCoord;
    

    vec4 scene = texture(screenTexture, uv);


    outputColor = scene.rrra;

}