

#version 430

out vec4 outputColor;

in vec3 normal;
in vec2 texCoord;

in vec3 fragPosition;
in vec3 viewPosition;

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


vec3 CalculateDiffuseLight(vec3 lightPos, vec3 lightCol)
{
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPos - fragPosition);
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightCol;

	return diffuse;
}


vec3 CalculateSpecularLight(vec3 lightPos, vec3 lightCol)
{
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPos - fragPosition);

	float specularStrength = 0.5;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
	vec3 specular = specularStrength * spec * lightCol; 

	return specular;
}


void main()
{
	vec3 ambient = ambientStrength * ambientColor;


	vec3 diffuse = CalculateDiffuseLight(lightPosition, lightColor);
	vec3 specular = CalculateSpecularLight(lightPosition, lightColor);

	vec3 result = (ambient + diffuse + specular) * color;

	outputColor = vec4(result, 1.0);
}
