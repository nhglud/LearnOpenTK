

#version 430

out vec4 outputColor;

in vec3 normal;
in vec2 texCoord;

in vec3 fragPosition;

uniform vec3 color;
uniform vec3 ambientColor;
uniform float ambientStrength;

uniform vec3 lightPosition;
uniform vec3 lightColor;

//
//uniform sampler2D texture0;
//uniform sampler2D texture1;
//
//
//struct Material 
//{
//    sampler2D diffuse;
//    sampler2D specular;
//    float     shininess;
//};
//
//
//uniform vec3 lightPos;
//uniform Material material;


void main()
{

	vec3 ambient = ambientStrength * ambientColor;


	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPosition - fragPosition);
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightColor;
	
	vec3 result = (ambient + diffuse) * color;

	outputColor = vec4(result, 1.0);
//    outputColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), 0.2);
}
