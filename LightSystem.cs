
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace LearnOpenTK
{
    internal class LightingSystem
    {

        private Shader litShader;
        private List<LightSource> lightSources;

        public LightingSystem(List<LightSource> LightSources)
        {
            litShader = AssetManager.GetShader("lit");

            this.lightSources = LightSources;

            Console.WriteLine(lightSources.Count);
        }

        public void Update()
        {
            litShader.Use();
            litShader.SetInt("numLights", lightSources.Count);

            for (int i = 0; i < lightSources.Count; i++)
            {
                string namePosition = $"lights[{i}].position";
                string nameColor = $"lights[{i}].color";

                litShader.SetVector3(namePosition, lightSources[i].transform.position);
                litShader.SetVector3(nameColor, lightSources[i].lightColor);
            }

        }

    }
}
