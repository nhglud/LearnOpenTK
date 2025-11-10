
using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public class Material
    {
        protected Shader shader;

        public Material()
        {

        }

        public Material(Shader shader)
        {
            this.shader = shader;
        }

        public virtual void Use(Matrix4 model)
        {
            shader.Use();
            shader.SetMat4("model", model);
        }



    }
}
