
namespace LearnOpenTK
{
    public class Renderer : Component
    {

        private Mesh mesh;
        public Material material;
        public List<Material> materials = new List<Material>();

        private static event Action? OnRender;

        public Renderer(Material material) : base()
        {
            this.material = material;
            materials.Add(material);

            OnRender += Render;
        }


        public Renderer(List<Material> materials) : base()
        {
            this.materials = materials;
            OnRender += Render;

        }


        public override void Init()
        {
            mesh = entity.GetComponent<Mesh>();
        }


        public void Render()
        {
            foreach (var mat in materials)
            {
                mat.Use(transform.GetTransformMatrix());
                mesh.Draw();
            }

            //material.Use(transform.GetTransformMatrix());
            //mesh.Draw();
        }


        public void AddMaterial(Material mat)
        {
            materials.Add(mat);
        }

        public void RemoveMaterial(Material mat)
        {
            materials.Remove(mat);
        }




        public static void UpdateRenderers()
        {
            OnRender?.Invoke();
        }




    }
}
