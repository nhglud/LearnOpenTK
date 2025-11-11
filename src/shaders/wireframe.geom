#version 430 core
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;
//uniform float normSize;


in vec3 normal[];
in vec2 texCoord[];
in vec3 tangent[];
in vec3 binormal[];
in mat3 tbn[];
in vec3 fragPosition[];
in vec3 viewPosition[];

out vec3 geoNormal;



void main()
{
	vec4 p;





}