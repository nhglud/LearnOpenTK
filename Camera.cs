using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class Camera : Component 
    {
        public static Camera main;

        public Vector3 front;
        public Vector3 up;
        public Vector3 right;

        private float fov;
        private float near;
        private float far;

        public float pitch = 0.0f;
        public float yaw = -90.0f;

        public Camera() : base()
        {
            main ??= this;

            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = new Vector3(0.0f, 1.0f, 0.0f);

            fov = 45.0f;
            near = 0.1f;
            far = 100.0f;
        }


        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(transform.position, transform.position + front, up);
        }

        public Matrix4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, near, far);
        }

        public void UpdateOrientations()
        {
            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
            front = Vector3.Normalize(front);
            right = Vector3.Normalize(Vector3.Cross(front, up));
        }

    }
}
