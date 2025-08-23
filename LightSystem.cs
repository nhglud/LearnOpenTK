
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace LearnOpenTK
{
    internal class LightingSystem
    {

        private Shader litShader;
        private List<PointLight> pointLights;
        private List<DirectionalLight> directionalLights;

        //private DirectionalLight directionalLight;

        private const int MAX_LIGHTS = 16;
        private const int MAX_DIRECTIONAL_LIGHTS = 16;


        //public LightingSystem(List<PointLight> LightSources, DirectionalLight directionalLight)
        //{
        //    litShader = AssetManager.GetShader("lit");

        //    this.pointLights = LightSources;
        //    this.directionalLight = directionalLight;

        //}

        public LightingSystem(List<PointLight> LightSources, List<DirectionalLight> directionalLights)
        {
            litShader = AssetManager.GetShader("lit");

            this.pointLights = LightSources;
            this.directionalLights = directionalLights;

        }



        public void Update()
        {
            litShader.Use();
            litShader.SetInt("numLights", pointLights.Count);
            litShader.SetInt("numDirectionalLights", directionalLights.Count);

            int count = Math.Min(MAX_LIGHTS, pointLights.Count);
            int countDirectional = Math.Min(MAX_DIRECTIONAL_LIGHTS, directionalLights.Count);



            for (int i = 0; i < count; i++)
            {
                string namePosition = $"lights[{i}].position";
                string nameColor = $"lights[{i}].color";

                litShader.SetVector3(namePosition, pointLights[i].transform.position);
                litShader.SetVector3(nameColor, pointLights[i].lightColor);
            }

            for (int i = 0; i < countDirectional; i++)
            {
                string nameDirection = $"directionalLights[{i}].direction";
                string nameColor = $"directionalLights[{i}].color";

                litShader.SetVector3(nameDirection, directionalLights[i].direction);
                litShader.SetVector3(nameColor, directionalLights[i].lightColor);
            }




            //litShader.SetVector3("directionalLight.color", directionalLight.lightColor);
            //litShader.SetVector3("directionalLight.direction", directionalLight.direction);

        }

    }
}
