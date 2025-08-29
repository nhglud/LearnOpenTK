using OpenTK.Windowing.Common;
using System.Security.Principal;

namespace LearnOpenTK
{
    public abstract class Level
    {
        public List<Entity> entities;
        public Game game;
        public Framebuffer framebuffer;

        public Level(Game game)
        {
            entities = new List<Entity>();
            this.game = game;

            Entity.EntityCreated += AddEntity;
        }

        public virtual void LoadLevel()
        {

        }

        public virtual void UpdateLevel(FrameEventArgs e)
        {

        }

        public virtual void RenderLevel(FrameEventArgs e)
        {


        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }


        public void Clear()
        {
            Entity.EntityCreated -= AddEntity;
            entities.Clear();
        }


    }
}
