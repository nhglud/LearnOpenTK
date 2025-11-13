#version 430 core
layout (triangles) in;
layout (triangle_strip) out;
layout (max_vertices = 8) out;

in vec2 uv[];


layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};

uniform mat4 model;

uniform sampler2D noisemap;
uniform float noiseScale;
uniform float noiseThreshold;

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


vec3 rotate(float angle, vec3 vector) {
    float sina = sin(angle);
    float cosa = cos(angle);

    mat3 R = mat3(
        cosa, 0.0, sina,
        0.0, 1.0, 0.0,
        -sina, 0, cosa);
    return R * vector;
}

float rand(vec2 co){
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}


void main()
{
    float noise = texture(noisemap, noiseScale * uv[0]).r;
//    float angle = 2 * 3.14 * texture(noisemap, 20 * uv[1]).r;

    float angle = 2 * 3.14 * rand(10.0 * uv[0]);



    if(noise > noiseThreshold) { 
        vec3 pos = 0.33 * ((model * gl_in[0].gl_Position).xyz + (model * gl_in[2].gl_Position).xyz + (model * gl_in[2].gl_Position).xyz);

        vec3 up = vec3(0.0, 1.0, 0.0);
        vec3 right = vec3(1.0, 0.0, 0.0);
        right = rotate(angle, right);
        CreateQuad(pos, right);

        pos += 0.5 * right;
        right = vec3(0.0, 0.0, 1.0);
        right = rotate(angle, right);
        pos -= 0.5 * right;
        CreateQuad(pos, right);
    
    }

}