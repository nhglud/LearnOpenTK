using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace LearnOpenTK.src.shaders
{

    public class TerrainBillboardMaterial : Material
    {
        public float heightScale { get; set; } = 60.0f;
        private Texture heightMap;
        private Texture billboardTexture;
        private Texture noiseMap;
        //private Texture billboardTexture;



        public TerrainBillboardMaterial(Texture heightMap, Texture billboardTexture, float heightScale) : base()
        {
            shader = new Shader(
               AssetManager.path + "src/shaders/terrain_billboard.vert",
               AssetManager.path + "src/shaders/billboard.geom",
               AssetManager.path + "src/shaders/billboard.frag");

            this.heightMap = heightMap;
            this.billboardTexture = billboardTexture;
            this.heightScale = heightScale;

            noiseMap = new Texture(AssetManager.path + "assets/perlin.png");
            //this.billboardTexture = billboardTexture;
        }


        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetFloat("heightScale", heightScale);
            shader.SetInt("billboardTexture", 0);
            billboardTexture.Use();

            shader.SetInt("heightmap", 1);
            heightMap.Use(TextureUnit.Texture1);


            shader.SetInt("noisemap", 2);
            heightMap.Use(TextureUnit.Texture2);


            //shader.SetInt("billboardTexture", 2);
            //billboardTexture.Use(TextureUnit.Texture2);
        }


    }


}
