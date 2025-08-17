
namespace LearnOpenTK
{
    public class Entity
    {
        public Transform transform; 

        private Dictionary<Type, object> components = new Dictionary<Type, object>();

        public Entity()
        {

        }

        public void AddComponent<T>(T component)
        {
            if (component == null) return;

            components.Add(typeof(T), component);   
        }

        public T GetComponent<T>()
        {
            return (T)components[typeof(T)];
        }


        public void RemoveComponent<T>()
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
