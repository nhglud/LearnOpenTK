
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK.src
{
    public class TerrainMaterial : Material
    {
        private Texture heightmap;
        private Texture diffuseMap;
        private float heightScale = 60.0f;

        public TerrainMaterial(Texture heightmap, Shader terrainShader, Texture diffuseMap) : base()
        {
            this.heightmap = heightmap;
            shader = terrainShader;
            this.diffuseMap = diffuseMap;
        }


        public TerrainMaterial(Texture heightmap, Shader terrainShader, Texture diffuseMap, float heightScale) : base()
        {
            this.heightmap = heightmap;
            shader = terrainShader;
            this.diffuseMap = diffuseMap;
            this.heightScale = heightScale;
        }



        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetFloat("heightScale", heightScale);
            shader.SetInt("heightmap", 0);
            heightmap.Use();

            shader.SetInt("diffuseMap", 1);
            diffuseMap.Use(TextureUnit.Texture1);


        }


    }
}
