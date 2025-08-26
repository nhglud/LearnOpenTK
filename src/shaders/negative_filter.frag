#version 430 core

in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D screenTexture;



void main()
{
    
    vec2 uv = texCoord;
    
//    uv.y = sin(uv.y);
//    uv.x = sin(uv.x);
//
//

    vec4 scene = texture(screenTexture, uv);



    outputColor = 1.0 - scene;

//    outputColor = scene;

}