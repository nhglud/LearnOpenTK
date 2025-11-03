
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK.src
{
    public class TerrainMaterial : Material
    {
        private Texture heightmap;
        private Texture noisemap;

        public TerrainMaterial(Texture heightmap, Shader terrainShader, Texture noisemap) : base()
        {
            this.heightmap = heightmap;
            shader = terrainShader;
            this.noisemap = noisemap;
        }


        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetFloat("heightScale", 1.0f);
            shader.SetInt("heightmap", 0);
            heightmap.Use();

            shader.SetInt("noiseMap", 1);
            noisemap.Use(TextureUnit.Texture1);


        }


    }
}
