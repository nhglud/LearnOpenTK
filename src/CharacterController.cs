
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace LearnOpenTK
{
    public class CharacterController : Component
    {

        private KeyboardState keyboardState;
        private MouseState mouseState;

        private bool firstMove = true;
        private float moveSpeed = 6f;
        private float mouseSensitivity = 0.05f;

        private Keys forwardKey = Keys.W;
        private Keys backwardKey = Keys.S;
        private Keys rightKey = Keys.D;
        private Keys leftKey = Keys.A;


        public CharacterController(KeyboardState keyboardState, MouseState mouseState) : base()
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;

            OnUpdate += Update;
        }


        public void Update(FrameEventArgs e)
        {
            Vector3 moveDir = Vector3.Zero;

            if (keyboardState.IsKeyDown(forwardKey))
            {
                moveDir += Camera.main.front;
            }

            if (keyboardState.IsKeyDown(backwardKey))
            {
                moveDir -= Camera.main.front;
            }

            if (keyboardState.IsKeyDown(rightKey))
            {
                moveDir += Camera.main.right;
            }

            if (keyboardState.IsKeyDown(leftKey))
            {
                moveDir -= Camera.main.right;
            }


            if (moveDir != Vector3.Zero)
            {
                moveDir = Vector3.Normalize(moveDir);
                Camera.main.transform.position += moveDir * moveSpeed * (float)e.Time;
            }

            if (firstMove)
            {
                firstMove = false;
            }
            else
            {
                float dx = mouseState.Delta.X * mouseSensitivity;
                float dy = mouseState.Delta.Y * mouseSensitivity;

                Camera.main.yaw += dx;
                Camera.main.pitch -= dy;
                Camera.main.pitch = MathHelper.Clamp(Camera.main.pitch, -89.0f, 89.0f);

                Camera.main.UpdateOrientations();
            }
        }
    }
}
