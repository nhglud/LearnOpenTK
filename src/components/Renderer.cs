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
        public Material material;

        
        private static event Action? OnRender;

        public Renderer(Material material) : base()
        {
            this.material = material;
            OnRender += Render;
        }


        public override void Init()
        {
            mesh = entity.GetComponent<Mesh>();
        }


        public void Render()
        {
            material.Use(transform.GetTransformMatrix());
            mesh.Draw();
        }


        public static void UpdateRenderers()
        {
            OnRender?.Invoke();
        }

    }
}
