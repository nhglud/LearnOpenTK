#version 430 core
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;

layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};

// Receive from vertex shader:
in vec3 fragPosition[];


void EmitEdge(vec3 a, vec3 b)
{
    gl_Position = projection * view * vec4(a, 1.0);
    EmitVertex();

    gl_Position = projection * view * vec4(b, 1.0);
    EmitVertex();

    EndPrimitive();
}

void main()
{
    vec3 p0 = fragPosition[0];
    vec3 p1 = fragPosition[1];
    vec3 p2 = fragPosition[2];

    vec3 edgeColor = vec3(0.0, 1.0, 0.0); // bright green wireframe

    EmitEdge(p0, p1);
    EmitEdge(p1, p2);
    EmitEdge(p2, p0);
}