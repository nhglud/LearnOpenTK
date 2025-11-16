#version 430 core

in vec2 TexCoord;
in vec2 UV;


out vec4 FragColor;


uniform sampler2D billboardTexture;


float rand(vec2 co){
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}


void main()
{

	vec4 col = texture(billboardTexture, TexCoord);


	if(col.a < 0.1) {
		discard;
	}

	FragColor = vec4(col.rgb, 1.0);
}