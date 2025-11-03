using OpenTK.Mathematics;

namespace LearnOpenTK.src
{
    public class TerrainMaterial : Material
    {
        private Texture heightmap;

        public TerrainMaterial(Texture heightmap, Shader terrainShader) : base()
        {
            this.heightmap = heightmap;
            shader = terrainShader;
        }


        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetInt("heightmap", 0);
            heightmap.Use();

        }


    }
}
