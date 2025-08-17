using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class Camera
    {
        private Transform transform;

        private float fov;
        private float near;
        private float far;

        public Camera(Transform transform)
        {
            this.transform = transform;

            fov = 45.0f;
            near = 0.1f;
            far = 100.0f;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(
                new Vector3(0.0f, 0.0f, 3.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f));

        }

        public Matrix4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, near, far);
        }

    }
}
