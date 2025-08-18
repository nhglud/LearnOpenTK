

#version 330

out vec4 outputColor;

in vec2 texCoord;


uniform sampler2D texture0;
uniform sampler2D texture1;


struct Material 
{
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};


uniform vec3 lightPos;
uniform Material material;


void main()
{

    outputColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), 0.2);
}
