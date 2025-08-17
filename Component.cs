namespace LearnOpenTK
{
    public class Component
    {
        public Entity entity;
        public Transform transform;

        public Component(Entity entity)
        {
            this.entity = entity;
            transform = entity.transform;
            Init();
        }

        protected virtual void Init()
        {

        }

    }
}
