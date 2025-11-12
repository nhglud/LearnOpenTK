#version 430 core

in vec2 TexCoord;

out vec4 FragColor;


uniform sampler2D billboardTexture;

void main()
{
	vec4 col = texture(billboardTexture, TexCoord);

	if(col.a < 0.1) {
		discard;
	}

	FragColor = vec4(col.rgb, 1.0);
}