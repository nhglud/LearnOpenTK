
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace LearnOpenTK
{
    internal class LightingSystem
    {

        private Shader litShader;
        private List<PointLight> pointLights;
        private DirectionalLight directionalLight;

        private const int MAX_LIGHTS = 16;

        public LightingSystem(List<PointLight> LightSources, DirectionalLight directionalLight)
        {
            litShader = AssetManager.GetShader("lit");

            this.pointLights = LightSources;
            this.directionalLight = directionalLight;

        }

        public void Update()
        {
            litShader.Use();
            litShader.SetInt("numLights", pointLights.Count);

            int count = Math.Min(MAX_LIGHTS, pointLights.Count);


            for (int i = 0; i < count; i++)
            {
                string namePosition = $"lights[{i}].position";
                string nameColor = $"lights[{i}].color";

                litShader.SetVector3(namePosition, pointLights[i].transform.position);
                litShader.SetVector3(nameColor, pointLights[i].lightColor);
            }


            litShader.SetVector3("directionalLight.color", directionalLight.lightColor);
            litShader.SetVector3("directionalLight.direction", directionalLight.direction);

        }

    }
}
