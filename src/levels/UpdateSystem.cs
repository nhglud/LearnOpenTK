using OpenTK.Windowing.Common;

namespace LearnOpenTK.src.levels
{
    public class UpdateSystem : System
    {
        List<IUpdateable> updateables;

        public UpdateSystem(List<Entity> entities)
        {
            updateables = new List<IUpdateable>();
            foreach (var e in entities)
            {
                foreach (var component in e.components.Values)
                {
                    if (typeof(IUpdateable).IsAssignableFrom(component.GetType()))
                    {
                        updateables.Add((IUpdateable)component);
                    }

                }
            }
        }

        public void Update(FrameEventArgs e)
        {
            foreach (var u in updateables)
            {
                u.Update(e);
            }
        }


    }
}
