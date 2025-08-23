
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;



namespace LearnOpenTK
{
    public class LitMaterial : Material
    {
        private Vector3 color;

        private static Vector3 ambientColor;
        private static float ambientStrength;
        
        private static Vector3 lightPosition;
        private static Vector3 lightColor;

        private Texture diffuseMap;
        private Texture specularMap;


        public LitMaterial(Vector3 color) : base()  
        {
            this.color = color;
            shader = AssetManager.GetShader("lit");
        }

        public LitMaterial(Texture diffuseMap, Texture specularMap) : base()
        {
            this.color = Vector3.One;
            shader = AssetManager.GetShader("lit");

            this.diffuseMap = diffuseMap;
            this.specularMap = specularMap;
        }


        public override void Use(Matrix4 model)
        { 
            base.Use(model);
            shader.SetVector3("color", color);

            shader.SetInt("material.diffuseMap", 0);
            shader.SetInt("material.specularMap", 1);
            diffuseMap.Use(TextureUnit.Texture0);
            specularMap.Use(TextureUnit.Texture1);

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


        public void SetColor(Vector3 color)
        {
            this.color = color;
        }

    }
}
