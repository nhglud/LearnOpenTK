using OpenTK.Windowing.Common;

namespace LearnOpenTK
{
    public abstract class Component
    {
        public Entity entity;
        public Transform transform;

        protected static event Action<FrameEventArgs> OnUpdate;

        public Component()
        {

        }

        public Component(Entity entity)
        {
            this.entity = entity;
            transform = entity.transform;
        }

        public virtual void Init()
        {

        }

        public static void UpdateComponents(FrameEventArgs e)
        {
            OnUpdate?.Invoke(e);
        }

    }
}
