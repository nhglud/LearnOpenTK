
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace LearnOpenTK
{
    public class LightingSystem : System
    {
        private const int MAX_POINT_LIGHTS = 16;
        private const int MAX_DIRECTIONAL_LIGHTS = 8;
        private const int MAX_SPOT_LIGHTS = 8;

        private Shader litShader;
        private List<PointLight> pointLights;
        private List<DirectionalLight> directionalLights;
        private List<SpotLight> spotLights;



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


            int count = Math.Min(MAX_POINT_LIGHTS, pointLights.Count);
            int countDirectional = Math.Min(MAX_DIRECTIONAL_LIGHTS, directionalLights.Count);
            int countSpot = Math.Min(MAX_SPOT_LIGHTS, spotLights.Count);


            for (int i = 0; i < count; i++)
            {
                string namePosition = $"lights[{i}].position";
                string nameColor = $"lights[{i}].color";

                litShader.SetVector3(namePosition, pointLights[i].transform.position);
                litShader.SetColor4(nameColor, pointLights[i].lightColor);
                UnlitMaterial mat = (UnlitMaterial)pointLights[i].entity.GetComponent<Renderer>().material;
                mat.color = new Vector3(pointLights[i].lightColor.R, pointLights[i].lightColor.G, pointLights[i].lightColor.B);
            }

            for (int i = 0; i < countDirectional; i++)
            {
                string nameDirection = $"directionalLights[{i}].direction";
                string nameColor = $"directionalLights[{i}].color";

                litShader.SetVector3(nameDirection, directionalLights[i].direction);
                litShader.SetColor4(nameColor, directionalLights[i].lightColor);

            }

            for (int i = 0; i < countSpot; i++)
            {
                string nameDirection = $"spotLights[{i}].direction";
                string nameColor = $"spotLights[{i}].color";
                string namePosition = $"spotLights[{i}].position";
                string nameOuter= $"spotLights[{i}].outerRadius";
                string nameInner = $"spotLights[{i}].innerRadius";

                litShader.SetVector3(nameDirection, spotLights[i].direction);
                litShader.SetColor4(nameColor, spotLights[i].lightColor);
                litShader.SetVector3(namePosition, spotLights[i].transform.position);
                litShader.SetFloat(nameOuter, MathF.Cos(spotLights[i].outerRadius));
                litShader.SetFloat(nameInner, MathF.Cos(spotLights[i].innerRadius));

                UnlitMaterial mat = (UnlitMaterial)spotLights[i].entity.GetComponent<Renderer>().material;
                mat.color = new Vector3(spotLights[i].lightColor.R, spotLights[i].lightColor.G, spotLights[i].lightColor.B);
            }


        }

    }



}


