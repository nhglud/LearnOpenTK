using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace LearnOpenTK.src.shaders
{
    //public class TerrainMaterial : Material
    //{
    //    private float heightScale = 60.0f;
    //    private Texture heightMap;
    //    private Texture diffuseMap;


    //    public TerrainMaterial(Texture heightMap, Texture diffuseMap, float heightScale) : base()
    //    {
    //        shader = AssetManager.GetShader("terrain");

    //        this.heightMap = heightMap;
    //        this.diffuseMap = diffuseMap;
    //        this.heightScale = heightScale;

    //    }


    //    public override void Use(Matrix4 model)
    //    {
    //        base.Use(model);

    //        shader.SetFloat("heighScale", heightScale);
    //        shader.SetInt("diffuseMap", 0);
    //        diffuseMap.Use();

    //        shader.SetInt("heightMap", 1);
    //        heightMap.Use(TextureUnit.Texture1);

    //    }


    //}

    public class TerrainBillboardMaterial : Material
    {
        private float heightScale = 60.0f;
        private Texture heightMap;
        private Texture billboardTexture;
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

            //shader.SetInt("billboardTexture", 2);
            //billboardTexture.Use(TextureUnit.Texture2);
        }


    }


}
