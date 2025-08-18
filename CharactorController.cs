using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.Net.Mime.MediaTypeNames;



namespace LearnOpenTK
{
    public class CharactorController : Component
    {

        private KeyboardState keyboardState;
        private MouseState mouseState;

        private bool firstMove = true;
        private float moveSpeed = 6f;
        private float mouseSensitivity = 0.05f;

        public CharactorController(KeyboardState keyboardState, MouseState mouseState) : base()
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
        }


        public CharactorController(Entity entity, KeyboardState keyboardState, MouseState mouseState) : base(entity)
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
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


                Camera.main.front.X = (float)Math.Cos(MathHelper.DegreesToRadians(Camera.main.pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Camera.main.yaw));
                Camera.main.front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Camera.main.pitch));
                Camera.main.front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Camera.main.pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Camera.main.yaw));
                Camera.main.front = Vector3.Normalize(Camera.main.front);
            }
        }
    }
}
