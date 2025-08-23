
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
        private List<SpotLight> spotLights;


        private const int MAX_LIGHTS = 16;
        private const int MAX_DIRECTIONAL_LIGHTS = 8;
        private const int MAX_SPOT_LIGHTS = 8;


        public LightingSystem(List<PointLight> LightSources, List<DirectionalLight> directionalLights, List<SpotLight> spotLights)
        {
            litShader = AssetManager.GetShader("lit");

            this.pointLights = LightSources;
            this.directionalLights = directionalLights;
            this.spotLights = spotLights;
        }



        public LightingSystem(List<Entity> entities)
        {
            litShader = AssetManager.GetShader("lit");

            spotLights = new List<SpotLight>();
            directionalLights = new List<DirectionalLight>();
            pointLights = new List<PointLight>();


            foreach (Entity entity in entities)
            {
                if(entity.HasComponent(typeof(SpotLight)))
                {
                   spotLights.Add(entity.GetComponent<SpotLight>());
                }
                else if (entity.HasComponent(typeof(PointLight)))
                {
                    pointLights.Add(entity.GetComponent<PointLight>());

                }
                else if (entity.HasComponent(typeof(DirectionalLight)))
                {
                    directionalLights.Add(entity.GetComponent<DirectionalLight>());
                }

            }
        }


        public void Update()
        {
            litShader.Use();
            litShader.SetInt("numLights", pointLights.Count);
            litShader.SetInt("numDirectionalLights", directionalLights.Count);
            litShader.SetInt("numSpotLights", spotLights.Count);


            int count = Math.Min(MAX_LIGHTS, pointLights.Count);
            int countDirectional = Math.Min(MAX_DIRECTIONAL_LIGHTS, directionalLights.Count);
            int countSpot = Math.Min(MAX_SPOT_LIGHTS, spotLights.Count);


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

            for (int i = 0; i < countSpot; i++)
            {
                string nameDirection = $"spotLights[{i}].direction";
                string nameColor = $"spotLights[{i}].color";
                string namePosition = $"spotLights[{i}].position";
                string nameOuter= $"spotLights[{i}].outerRadius";
                string nameInner = $"spotLights[{i}].innerRadius";

                litShader.SetVector3(nameDirection, spotLights[i].direction);
                litShader.SetVector3(nameColor, spotLights[i].lightColor);
                litShader.SetVector3(namePosition, spotLights[i].transform.position);
                litShader.SetFloat(nameOuter, spotLights[i].outerRadius);
                litShader.SetFloat(nameInner, spotLights[i].innerRadius);
            }


        }

    }
}
