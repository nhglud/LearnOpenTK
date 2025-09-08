

#version 430


struct Material {
	vec4 color;
	sampler2D diffuseMap;
	sampler2D specularMap;
};


struct LightSource {
	vec3 position;
	vec4 color;
};

struct DirectionalLight
{
	vec4 color;
	vec3 direction;
};

struct SpotLight
{
	vec4 color;
	vec3 direction;
	vec3 position;
	float innerRadius;
	float outerRadius;
};


out vec4 outputColor;

in vec3 normal;
in vec2 texCoord;

in vec3 fragPosition;
in vec3 viewPosition;

//uniform vec3 color;
uniform vec3 ambientColor;
uniform float ambientStrength;

//uniform vec3 lightPosition;
//uniform vec3 lightColor;

uniform Material material;

#define MAX_LIGHTS 16

uniform int numLights;
uniform LightSource lights[MAX_LIGHTS];


#define MAX_DIRECTIONAL_LIGHTS 8

uniform int numDirectionalLights;
uniform DirectionalLight directionalLights[MAX_DIRECTIONAL_LIGHTS];


#define MAX_SPOT_LIGHTS 8
uniform int numSpotLights;
uniform SpotLight spotLights[MAX_SPOT_LIGHTS];

uniform bool useTexture;

vec3 CalculateDiffuseLight(vec3 lightPos, vec4 lightCol, vec3 norm);
vec3 CalculateSpecularLight(vec3 lightPos, vec4 lightCol, vec3 norm);
vec3 CalculateDiffuseLightDirectional(vec4 lightCol, vec3 direction, vec3 norm);
vec3 CalculateSpecularLightDirectional(vec4 lightCol, vec3 direction, vec3 norm);
vec3 CalculateDiffuseSpotLight(SpotLight spotlight, vec3 norm);
vec3 CalculateSpecularSpotLight(SpotLight spotlight, vec3 norm);


void main()
{

	vec3 ambient = ambientStrength * ambientColor;

	

	vec3 diffColor = vec3(texture(material.diffuseMap, texCoord));
	vec3 specColor = vec3(texture(material.specularMap, texCoord));

	if(!useTexture)
	{
		diffColor = material.color.rgb;
		specColor = vec3(1.0);
	}


	vec3 diffuse = vec3(0.0);
	vec3 specular = vec3(0.0);

	vec3 norm = normalize(normal);

	// point lights
	for (int i = 0; i < numLights; i++)
	{
		diffuse += CalculateDiffuseLight(lights[i].position, lights[i].color, norm);
		specular += CalculateSpecularLight(lights[i].position, lights[i].color, norm);
	}

	for (int i = 0; i < numDirectionalLights; i++)
	{
		diffuse += CalculateDiffuseLightDirectional(directionalLights[i].color, directionalLights[i].direction, norm);
		specular += CalculateSpecularLightDirectional(directionalLights[i].color, directionalLights[i].direction, norm);
	}

	for (int i = 0; i < numSpotLights; i++)
	{
		diffuse += CalculateDiffuseSpotLight(spotLights[i], norm);
		specular += CalculateSpecularSpotLight(spotLights[i], norm);
	}

	vec3 result = (ambient + diffuse) * diffColor  + diffuse * specular * specColor;

	outputColor = vec4(result, 1.0);
}


vec3 CalculateDiffuseLight(vec3 lightPos, vec4 lightCol, vec3 norm)
{
	vec3 lightDir = normalize(lightPos - fragPosition);
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightCol.rgb;

	return diffuse;
}


vec3 CalculateSpecularLight(vec3 lightPos, vec4 lightCol, vec3 norm)
{
	vec3 lightDir = normalize(lightPos - fragPosition);

	float specularStrength = 0.8;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  

	vec3 halfwayDir = normalize(viewDir + lightDir);

	float spec = pow(max(dot(norm, halfwayDir), 0.0), 128);
	vec3 specular = specularStrength * spec * lightCol.rgb; 

	return specular;
}


vec3 CalculateDiffuseLightDirectional(vec4 lightCol, vec3 direction, vec3 norm)
{
	vec3 lightDir = -direction;
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * lightCol.rgb;

	return diffuse;
}

vec3 CalculateSpecularLightDirectional(vec4 lightCol, vec3 direction, vec3 norm)
{
	vec3 lightDir = -direction;

	float specularStrength = 0.8;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  

	vec3 halfwayDir = normalize(viewDir + lightDir);

	float spec = pow(max(dot(norm, halfwayDir), 0.0), 128);
	vec3 specular = specularStrength * spec * lightCol.rgb; 

	return specular;
}


vec3 CalculateDiffuseSpotLight(SpotLight spotlight, vec3 norm)
{
	vec3 lightDir = normalize(spotlight.position - fragPosition);
	vec3 diffuse = max(dot(norm, lightDir), 0.0) * spotlight.color.rgb;

	float theta     = dot(lightDir, normalize(spotlight.direction));
	float epsilon   = spotlight.innerRadius - spotlight.outerRadius;
	float intensity = clamp((theta -  spotlight.outerRadius) / epsilon, 0.0, 1.0);   

	return diffuse * intensity;
}


vec3 CalculateSpecularSpotLight(SpotLight spotlight, vec3 norm)
{
	vec3 lightDir = normalize(spotlight.position - fragPosition);

	float specularStrength = 0.8;
	vec3 viewDir = normalize(viewPosition - fragPosition);
	vec3 reflectDir = reflect(-lightDir, norm);  

	vec3 halfwayDir = normalize(viewDir + lightDir);

	float spec = pow(max(dot(norm, halfwayDir), 0.0), 128);
	vec3 specular = specularStrength * spec * spotlight.color.rgb; 

	float theta     = dot(lightDir, normalize(spotlight.direction));
	float epsilon   = spotlight.innerRadius - spotlight.outerRadius;
	float intensity = clamp((theta -  spotlight.outerRadius) / epsilon, 0.0, 1.0);   

	return specular * intensity;
}
