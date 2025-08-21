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
        private static event Action? OnRender;


        public Renderer(Shader shader) : base()
        {
            this.shader = shader;
            OnRender += Render;
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

        public static void UpdateRenderers()
        {
            OnRender?.Invoke();
        }

    }
}
