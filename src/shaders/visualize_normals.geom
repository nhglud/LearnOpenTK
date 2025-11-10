#version 430 core
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;
//uniform float normSize;

in vec3 normal[];
out vec3 geoNormal;

void GenerateLine(int i)
{
	float normSize = 1.0;
	gl_Position = gl_in[i].gl_Position;
	geoNormal = normal[i];
	EmitVertex();
	gl_Position = (gl_in[i].gl_Position + vec4(normal[i], 0.0) * normSize);
	geoNormal = normal[i];
	EmitVertex();
	EndPrimitive();
}

void main()
{
	GenerateLine(0);
	GenerateLine(1);
	GenerateLine(2);
}