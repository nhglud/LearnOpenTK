
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK.src
{
    public class TerrainMaterial : Material
    {
        private Texture heightmap;
        private Texture diffuseMap;

        public TerrainMaterial(Texture heightmap, Shader terrainShader, Texture diffuseMap) : base()
        {
            this.heightmap = heightmap;
            shader = terrainShader;
            this.diffuseMap = diffuseMap;
        }


        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetFloat("heightScale", 1.0f);
            shader.SetInt("heightmap", 0);
            heightmap.Use();

            shader.SetInt("diffuseMap", 1);
            diffuseMap.Use(TextureUnit.Texture1);


        }


    }
}
