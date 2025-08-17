
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
            components.Add(typeof(T), component);   
        }

        public T GetComponent<T>()
        {
            return (T)components[typeof(T)];
        }

    }
}
