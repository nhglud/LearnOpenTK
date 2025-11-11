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


void GenerateLine(int index)
{

	float normSize = 0.2; // adjust to taste

    vec3 start = fragPosition[index];
    vec3 end = start + normalize(normal[index]) * normSize;

    gl_Position = projection * view * vec4(start, 1.0);
    EmitVertex();

    gl_Position = projection * view * vec4(end, 1.0);
    EmitVertex();

    EndPrimitive();
}

void main()
{
	GenerateLine(0);
	GenerateLine(1);
	GenerateLine(2);
}


