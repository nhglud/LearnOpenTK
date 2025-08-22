
using OpenTK.Mathematics;
namespace LearnOpenTK
{
    public class UnlitMaterial : Material
    {
        private Vector3 color;

        public UnlitMaterial(Vector3 color) : base()
        {
            shader = AssetManager.GetShader("single_color");
            this.color = color;
        }

        public override void Use(Matrix4 model)
        {
            base.Use(model);
            shader.SetVector3("color", color);
        }

    }
}
