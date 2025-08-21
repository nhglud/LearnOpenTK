using OpenTK.Windowing.Common;

namespace LearnOpenTK
{
    public abstract class Level
    {
        public List<Entity> entities;
        public Game game;

        public Level(Game game)
        {
            entities = new List<Entity>();
            this.game = game;
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


    }
}
