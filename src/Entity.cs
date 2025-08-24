
namespace LearnOpenTK
{
    public class Entity
    {
        public Transform transform; 

        private Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        public Entity()
        {

        }

        public void AddComponent<T>(T component) where T : Component
        {
            if (component == null) return;

            component.transform = transform;
            component.entity = this;
            component.Init();

            components.Add(typeof(T), component);   
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)components[typeof(T)];
        }


        public void RemoveComponent<T>() where T : Component
        {
            components.Remove(typeof(T));
        }

        public bool HasComponent(Type type)
        { 
            return components.ContainsKey(type); 
        }

        public void Clear()
        {
            components.Clear();
        }

    }
}
