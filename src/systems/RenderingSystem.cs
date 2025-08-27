namespace LearnOpenTK
{
    public class RenderingSystem : System
    {
        private List<Renderer> renderers;

        public RenderingSystem(List<Entity> entities)
        {
            renderers = new List<Renderer>();
            foreach (var e in entities)
            {
                if (e.HasComponent(typeof(Renderer)))
                    renderers.Add(e.GetComponent<Renderer>());

            }

        }

        public void Render()
        {

            foreach (var r in renderers)
            {
                r.Render();
            }
        }
    }
}
