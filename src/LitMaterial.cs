
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using static System.Net.Mime.MediaTypeNames;



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


        private Texture decalTexture;


        //Matrix4 decalView;
        //Matrix4 decalProjection;

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

            decalTexture = AssetManager.GetTexture("blood");

        }


        public LitMaterial(Texture diffuseMap, Texture specularMap, Texture normalMap) : base()
        {
            this.color = Color4.Lavender;
            shader = AssetManager.GetShader("lit");

            this.diffuseMap = diffuseMap;
            this.specularMap = specularMap;
            this.normalMap = normalMap;

            decalTexture = AssetManager.GetTexture("blood");
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
            normalMap.Use(TextureUnit.Texture2);


            var decalCamPos = new Vector3(0.0f, 3.0f, 0.0f);
            var decalDir = new Vector3(0.0f, -1.0f, -1.0f);

            Matrix4 decalView = Matrix4.LookAt(decalCamPos, decalCamPos + decalDir, Vector3.UnitY);
            Matrix4 decalProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1.0f, 0.1f, 100.0f);


       

            //decalView = Camera.main.GetViewMatrix();
            //decalProjection = Camera.main.GetProjectionMatrix(1.0f);


            shader.SetMat4("decalView", decalView);
            shader.SetMat4("decalProjection", decalProjection);

            shader.SetInt("decalTexture", 3);

            decalTexture.Use(TextureUnit.Texture3);
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
