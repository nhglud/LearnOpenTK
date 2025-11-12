#version 430 core
layout (triangles) in;
layout (triangle_strip) out;
layout (max_vertices = 4) out;


layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};

uniform mat4 model;


out vec2 TexCoord;

void main()
{

    vec3 pos = (model * gl_in[0].gl_Position).xyz;

    vec3 cameraDir = normalize(cameraPosition.xyz - pos);
    vec3 up = vec3(0.0, 1.0, 0.0);
    vec3 right = normalize(cross(cameraDir, up));

    gl_Position = projection * view * vec4(pos, 1.0);
    TexCoord = vec2(0.0, 0.0);
    EmitVertex();


    pos.y += 1.0;
    gl_Position = projection * view * vec4(pos, 1.0);
    TexCoord = vec2(0.0, 1.0);
    EmitVertex();

    pos.y -= 1.0;
    pos += right;
    gl_Position = projection * view * vec4(pos, 1.0);
    TexCoord = vec2(1.0, 0.0);
    EmitVertex();


    pos.y += 1.0;
    gl_Position = projection * view * vec4(pos, 1.0);
    TexCoord = vec2(1.0, 1.0);
    EmitVertex();
    EndPrimitive();

}