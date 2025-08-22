
using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public abstract class Material
    {
        protected Shader shader;

        public Material()
        {

        }

        public virtual void Use(Matrix4 model)
        {
            shader.Use();
            shader.SetMat4("model", model);
        }



    }
}
