
using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public class LitMaterial : Material
    {
        private Vector3 color;
        private static Vector3 ambientColor;
        private static float ambientStrength;
        
        private static Vector3 lightPosition;
        private static Vector3 lightColor;



        public LitMaterial(Vector3 color) : base()  
        {
            this.color = color;
            shader = AssetManager.GetShader("lit");
        }

        public override void Use(Matrix4 model)
        { 
            base.Use(model);
            shader.SetFloat("ambientStrength", ambientStrength);
            shader.SetVector3("color", color);
            shader.SetVector3("ambientColor", ambientColor);

            shader.SetVector3("lightPosition", lightPosition);
            shader.SetVector3("lightColor", lightColor);

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
