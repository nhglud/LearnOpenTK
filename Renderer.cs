using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class Renderer : Component
    {
        
        private Mesh mesh;
        private Shader shader;


        public Renderer(Shader shader) : base()
        {

            this.shader = shader;

        }


        public Renderer(Entity entity, Shader shader) : base(entity)
        {

            this.shader = shader;
        
        }

        public override void Init()
        {
            mesh = entity.GetComponent<Mesh>();
        }

        public void Render()
        {
            shader.Use();
            shader.SetMat4("model", transform.GetTransformMatrix());

            mesh.Draw();
        }
    }
}
