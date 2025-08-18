using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;



namespace LearnOpenTK
{
    public class CharactorController : Component
    {

        private float moveSpeed = 6f;
        private KeyboardState keyboardState;


        public CharactorController(KeyboardState keyboardState) : base()
        {
            this.keyboardState = keyboardState;
        }


        public CharactorController(Entity entity, KeyboardState keyboardState) : base(entity)
        {
            this.keyboardState = keyboardState;
        }

        public void Update(FrameEventArgs e)
        {
            Vector3 moveDir = Vector3.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
            {
                moveDir += Camera.main.front;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                moveDir -= Camera.main.front;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                moveDir += Vector3.Normalize(Vector3.Cross(Camera.main.front, Camera.main.up));
            }

            if (keyboardState.IsKeyDown((Keys)Keys.A))
            {
                moveDir -= Vector3.Normalize(Vector3.Cross(Camera.main.front, Camera.main.up));
            }


            if (moveDir != Vector3.Zero)
            {
                moveDir = Vector3.Normalize(moveDir);

                Camera.main.transform.position += moveDir * moveSpeed * (float)e.Time;
            }


        }

    }
}
