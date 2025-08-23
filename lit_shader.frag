

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

struct Material {
	sampler2D diffuseMap;
	sampler2D specularMap;
};


struct LightSource {
	vec3 position;
	vec3 color;
};

struct DirectionalLight
{
	vec3 color;
	vec3 direction;
};


uniform Material material;

#define MAX_LIGHTS 16


uniform int numLights;
uniform LightSource lights[MAX_LIGHTS];

uniform DirectionalLight directionalLight;


vec3 CalculateDiffuseLight(vec3 lightPos, vec3 lightCol, vec3 norm)
{
//	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPos - fragPosition);
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightCol;

	return diffuse;
}


vec3 CalculateSpecularLight(vec3 lightPos, vec3 lightCol, vec3 norm)
{
//	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPos - fragPosition);

	float specularStrength = 0.8;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 128);
	vec3 specular = specularStrength * spec * lightCol; 

	return specular;
}


vec3 CalculateDiffuseLightDirectional(vec3 lightCol, vec3 direction, vec3 norm)
{
//	vec3 norm = normalize(normal);
	vec3 lightDir = -direction;
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightCol;

	return diffuse;
}

vec3 CalculateSpecularLightDirectional(vec3 lightCol, vec3 direction, vec3 norm)
{

	vec3 lightDir = -direction;

	float specularStrength = 0.8;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 128);
	vec3 specular = specularStrength * spec * lightCol; 

	return specular;

}

void main()
{

	vec3 ambient = ambientStrength * ambientColor;

	vec3 diffColor = vec3(texture(material.diffuseMap, texCoord));
	vec3 specColor = vec3(texture(material.specularMap, texCoord));

	vec3 diffuse = vec3(0.0);
	vec3 specular = vec3(0.0);

	vec3 norm = normalize(normal);

	// point lights
	for (int i = 0; i < numLights; i++)
	{
		diffuse += CalculateDiffuseLight(lights[i].position, lights[i].color, norm);
		specular += CalculateSpecularLight(lights[i].position, lights[i].color, norm);
	}

	diffuse += CalculateDiffuseLightDirectional(directionalLight.color, directionalLight.direction, norm);
	specular += CalculateSpecularLightDirectional(directionalLight.color, directionalLight.direction, norm);





	vec3 result = (ambient + diffuse) * diffColor  + specular * specColor;

	outputColor = vec4(result, 1.0);
}
