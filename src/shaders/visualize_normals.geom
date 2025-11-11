#version 430 core
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;

in vec3 normal[];

in vec3 fragPosition[];

layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};


out vec3 geoNormal;

void GenerateLine(int index)
{

	float normSize = 0.2; // adjust to taste

    vec3 start = fragPosition[index];
    vec3 end = start + normalize(normal[index]) * normSize;

    gl_Position = projection * view * vec4(start, 1.0);
    geoNormal = normal[index];
    EmitVertex();

    gl_Position = projection * view * vec4(end, 1.0);
    geoNormal = normal[index];
    EmitVertex();

    EndPrimitive();

//	float normSize = 1.0;
//	gl_Position = gl_in[index].gl_Position;
//	geoNormal = normal[index];
//	EmitVertex();
//	gl_Position = (gl_in[index].gl_Position + vec4(normal[index], 0.0) * normSize);
//	geoNormal = normal[index];
//	EmitVertex();
//	EndPrimitive();
}

void main()
{
	GenerateLine(0);
	GenerateLine(1);
	GenerateLine(2);
}