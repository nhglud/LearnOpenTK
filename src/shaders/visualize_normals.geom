//#version 430 core
//layout (triangles) in;
//layout (line_strip, max_vertices = 6) out;
//
//in vec3 normal[];
//
//in vec3 fragPosition[];
//
//layout(std140, binding = 0) uniform CameraData
//{
//    mat4 view;
//    mat4 projection;
//    vec4 cameraPosition;
//};
//
//
//void GenerateLine(int index)
//{
//
//	float normSize = 0.2; // adjust to taste
//
//    vec3 start = fragPosition[index];
//    vec3 end = start + normalize(normal[index]) * normSize;
//
//    gl_Position = projection * view * vec4(start, 1.0);
//    EmitVertex();
//
//    gl_Position = projection * view * vec4(end, 1.0);
//    EmitVertex();
//
//    EndPrimitive();
//}
//
//void main()
//{
//	GenerateLine(0);
//	GenerateLine(1);
//	GenerateLine(2);
//}


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

out vec3 geoColor;

void EmitEdge(vec3 a, vec3 b, vec3 color)
{
    gl_Position = projection * view * vec4(a, 1.0);
    geoColor = color;
    EmitVertex();

    gl_Position = projection * view * vec4(b, 1.0);
    geoColor = color;
    EmitVertex();

    EndPrimitive();
}

void main()
{
    vec3 p0 = fragPosition[0];
    vec3 p1 = fragPosition[1];
    vec3 p2 = fragPosition[2];

    vec3 edgeColor = vec3(0.0, 1.0, 0.0); // bright green wireframe

    EmitEdge(p0, p1, edgeColor);
    EmitEdge(p1, p2, edgeColor);
    EmitEdge(p2, p0, edgeColor);
}