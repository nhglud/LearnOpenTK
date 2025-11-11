#version 430 core
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;

layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};

in vec3 fragPosition[];

void GenerateLine(vec3 posA, vec3 posB)
{
    gl_Position = projection * view * vec4(posA, 1.0);
    EmitVertex();

    gl_Position = projection * view * vec4(posB, 1.0);
    EmitVertex();

    EndPrimitive();
}

void main()
{
    vec3 p0 = fragPosition[0];
    vec3 p1 = fragPosition[1];
    vec3 p2 = fragPosition[2];

    GenerateLine(p0, p1);
    GenerateLine(p1, p2);
    GenerateLine(p2, p0);
}