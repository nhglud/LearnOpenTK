#version 430 core
layout (triangles) in;
layout (triangle_strip) out;
layout (max_vertices = 8) out;


layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};

uniform mat4 model;


in vec3 fragPosition[];

out vec2 TexCoord;

void CreateQuad(vec3 pos, vec3 right) {

    vec3 up = vec3(0.0, 1.0, 0.0);

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


void main()
{
    vec3 pos = 0.33 * (fragPosition[0].xyz + fragPosition[1].xyz + fragPosition[2]);

    vec3 up = vec3(0.0, 1.0, 0.0);
    vec3 right = vec3(1.0, 0.0, 0.0);

    CreateQuad(pos, right);

    pos += 0.5 * right;
    right = vec3(0.0, 0.0, 1.0);
    pos -= 0.5 * right;
    CreateQuad(pos, right);

}