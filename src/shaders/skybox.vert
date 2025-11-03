#version 430 core
layout (location = 0) in vec3 aPos;

out vec3 TexCoords;

layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};


void main()
{
    TexCoords = aPos;

    mat4 v = mat4(mat3(view));
    vec4 pos = projection * v * vec4(aPos, 1.0);
    gl_Position = pos.xyww;
}  