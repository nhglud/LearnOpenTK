#version 430

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aTexCoord;


uniform mat4 model;
uniform sampler2D heightmap;
uniform float heightScale;

out vec2 uv;

void main()
{

//	float heightScale = 60.0;

	float height =  heightScale * texture(heightmap, aTexCoord).r;

	vec3 pos = aPosition;
	pos.y = pos.y + height ;

	uv = aTexCoord;

	gl_Position = vec4(pos, 1.0);
}