
#version 430 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aTexCoord;
layout(location = 3) in vec3 aTangent;
layout(location = 4) in vec3 aBinormal;

out vec3 normal;
out vec2 texCoord;
out vec3 tangent;
out vec3 binormal;
out mat3 tbn;

out vec3 fragPosition;
out vec3 viewPosition;

uniform mat4 model;


layout(std140, binding = 0) uniform CameraData
{
    mat4 view;
    mat4 projection;
    vec4 cameraPosition;
};



uniform sampler2D heightmap;

out float height;
out vec3 norm;

in float heightScale;

void main(void)
{
    texCoord = aTexCoord;

    float heightScale = 60.0;

    height =  heightScale * texture(heightmap, aTexCoord).r;

    vec3 pos = aPosition;
    pos.y = pos.y + height ;


    float delta = 1.0 / 512.0;

    float hL = heightScale * texture(heightmap, aTexCoord + vec2(-delta, 0.0)).r;
    float hR = heightScale * texture(heightmap, aTexCoord + vec2(delta, 0.0)).r;
    float hU = heightScale * texture(heightmap, aTexCoord + vec2(0.0, delta)).r;
    float hD = heightScale * texture(heightmap, aTexCoord + vec2(0.0, -delta)).r;

    vec3 dx = vec3(2.0*delta, (hR - hL), 0.0); // tangent along x
    vec3 dz = vec3(0.0, (hU - hD), 2.0 * delta); // tangent along z
    norm = normalize(cross(dz, dx));


    viewPosition = cameraPosition.xyz;

    normal = normalize(mat3(transpose(inverse(model))) * norm);
    tangent = normalize(mat3(transpose(inverse(model))) * aTangent);
    binormal = normalize(mat3(transpose(inverse(model))) * aBinormal);

    tbn = mat3(tangent, binormal, normal);
    
    fragPosition = vec3(model * vec4(pos, 1.0));

  

    gl_Position =  projection * view * model * vec4(pos, 1.0);



}