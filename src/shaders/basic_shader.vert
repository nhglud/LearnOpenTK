
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

uniform mat4 decalProjection;
uniform mat4 decalView;
out vec4 decalViewPos;


#define MAX_DECALS 10

uniform int countDecals;

uniform mat4 decals[MAX_DECALS];
out vec4 decalPositions[MAX_DECALS];


void main(void)
{
    texCoord = aTexCoord;

    viewPosition = cameraPosition.xyz;

    normal = normalize(mat3(transpose(inverse(model))) * aNormal);
    tangent = normalize(mat3(transpose(inverse(model))) * aTangent);
    binormal = normalize(mat3(transpose(inverse(model))) * aBinormal);

    tbn = mat3(tangent, binormal, normal);
    
    fragPosition = vec3(model * vec4(aPosition, 1.0));

    gl_Position =  projection * view * model * vec4(aPosition, 1.0);

    decalViewPos = decalProjection * decalView * model * vec4(aPosition, 1.0);



    for(int i = 0; i < countDecals; i++)
    {
        decalPositions[i] = decalProjection * decals[i] * model * vec4(aPosition, 1.0);
    }


}