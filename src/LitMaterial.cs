
using Assimp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;



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
        private Texture decalSpecular;
        private Texture decalNormal;



        public static List<Decal> decals;

        //Matrix4 decalView;
        //Matrix4 decalProjection;

        public LitMaterial(Color4 color) : base()  
        {
            this.color = color;
            shader = AssetManager.GetShader("lit");

            decalTexture = AssetManager.GetTexture("blood");
            decalSpecular = AssetManager.GetTexture("blood_specular");
            decalNormal = AssetManager.GetTexture("blood_normal");
        }

        public LitMaterial(Texture diffuseMap, Texture specularMap) : base()
        {
            this.color = Color4.Lavender;
            shader = AssetManager.GetShader("lit");

            this.diffuseMap = diffuseMap;
            this.specularMap = specularMap;

            decalTexture = AssetManager.GetTexture("blood");
            decalSpecular = AssetManager.GetTexture("blood_specular");
            decalNormal = AssetManager.GetTexture("blood_normal");
        }


        public LitMaterial(Texture diffuseMap, Texture specularMap, Texture normalMap) : base()
        {
            this.color = Color4.Lavender;
            shader = AssetManager.GetShader("lit");

            this.diffuseMap = diffuseMap;
            this.specularMap = specularMap;
            this.normalMap = normalMap;


            decals = new List<Decal>();
            decalTexture = AssetManager.GetTexture("blood");
            decalSpecular = AssetManager.GetTexture("blood_specular");
            decalNormal = AssetManager.GetTexture("blood_normal");
        }



        public override void Use(Matrix4 model)
        { 
            base.Use(model);

            SetMaterialProperties();
            SetDecals();
        }

        private void SetMaterialProperties()
        {
            shader.SetColor4("material.color", color);
            shader.SetBool("useTexture", useTexture);

            shader.SetInt("material.diffuseMap", 0);
            shader.SetInt("material.specularMap", 1);
            shader.SetInt("material.normalMap", 2);

            diffuseMap.Use(TextureUnit.Texture0);
            specularMap.Use(TextureUnit.Texture1);
            normalMap.Use(TextureUnit.Texture2);

        }


        private void SetDecals()
        {
            float decalSize = 5.0f;
            Matrix4 decalView = Matrix4.LookAt(Camera.main.transform.position, Camera.main.transform.position + Camera.main.front, Vector3.UnitY);
            Matrix4 decalProjection = Matrix4.CreateOrthographic(decalSize, decalSize, 0.1f, 100.0f);

            const int MAX_DECALS = 10;
            int countDecals = Math.Min(decals.Count, MAX_DECALS);

            shader.SetMat4("decalView", decalView);
            shader.SetMat4("decalProjection", decalProjection);

            shader.SetInt("decalTexture", 3);
            shader.SetInt("decalSpecular", 4);
            shader.SetInt("decalNormal", 5);

            shader.SetInt("countDecals", countDecals);

            decalTexture.Use(TextureUnit.Texture3);
            decalSpecular.Use(TextureUnit.Texture4);
            decalNormal.Use(TextureUnit.Texture5);


            for (int i = 0; i < countDecals; i++)
            {
                Matrix4 dview = Matrix4.LookAt(decals[i].position, decals[i].position + decals[i].direction, Vector3.UnitY); ;

                shader.SetMat4($"decals[{i}]", dview);

            }


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


public struct Decal
{
    public Vector3 position;
    public Vector3 direction;
    
}
