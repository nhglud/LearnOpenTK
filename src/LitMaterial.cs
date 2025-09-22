
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;



namespace LearnOpenTK
{
    public class LitMaterial : Material
    {
        private Color4 color;

        private static Vector3 ambientColor;
        private static float ambientStrength;
        
        private static Vector3 lightPosition;
        private static Vector3 lightColor;

        private Texture diffuseMap;
        private Texture specularMap;
        private Texture normalMap;


        public bool useTexture = true;

        public LitMaterial(Color4 color) : base()  
        {
            this.color = color;
            shader = AssetManager.GetShader("lit");
        }

        public LitMaterial(Texture diffuseMap, Texture specularMap) : base()
        {
            this.color = Color4.Lavender;
            shader = AssetManager.GetShader("lit");

            this.diffuseMap = diffuseMap;
            this.specularMap = specularMap;
        }


        public override void Use(Matrix4 model)
        { 
            base.Use(model);

            shader.SetColor4("material.color", color);
            shader.SetBool("useTexture", useTexture);

            shader.SetInt("material.diffuseMap", 0);
            shader.SetInt("material.specularMap", 1);
            shader.SetInt("material.normalMap", 2);

            diffuseMap.Use(TextureUnit.Texture0);
            specularMap.Use(TextureUnit.Texture1);
            //normalMap.Use(TextureUnit.Texture2);


        }

        public static void UpdateStaticProperties()
        {
            var litShader = AssetManager.GetShader("lit");

            litShader.SetFloat("ambientStrength", ambientStrength);
            litShader.SetVector3("ambientColor", ambientColor);
            litShader.SetVector3("lightPosition", lightPosition);
            litShader.SetVector3("lightColor", lightColor);
        }

        public static void SetAmbient(Vector3 color, float strength)
        {
            ambientColor = color;
            ambientStrength = strength;
        }


        public static void SetDiffuse(Vector3 position, Vector3 color)
        {
            lightPosition = position;
            lightColor = color;

        }


        //public void SetColor(Vector3 color)
        //{
        //    this.color = color;
        //}

    }
}
