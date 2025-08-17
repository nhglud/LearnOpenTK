using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class Camera
    {
        Transform transform;

        public Camera(Transform transform)
        {
            this.transform = transform;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.Identity;

        }

        public Matrix4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4.Identity;
        }

    }
}
