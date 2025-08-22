//using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

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

        private int cameraUBO;

        public Camera() : base()
        {
            main ??= this;

            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = new Vector3(0.0f, 1.0f, 0.0f);

            fov = 45.0f;
            near = 0.1f;
            far = 100.0f;
            InitUBO();
        }


        private void InitUBO()
        {
            cameraUBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.UniformBuffer, cameraUBO);
            GL.BufferData(BufferTarget.UniformBuffer, 2 * 16 * sizeof(float), IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);

            GL.BindBufferRange(BufferRangeTarget.UniformBuffer, 0, cameraUBO, IntPtr.Zero, 2 * 16 * sizeof(float));
        }


        public void UpdateUBO(int viewportWidth, int viewportHeight)
        {
            Matrix4 view = GetViewMatrix();
            Matrix4 proj = GetProjectionMatrix(viewportWidth / (float)viewportHeight);

            GL.BindBuffer(BufferTarget.UniformBuffer, cameraUBO);
            GL.BufferSubData(BufferTarget.UniformBuffer, IntPtr.Zero, 64, ref view);
            GL.BufferSubData(BufferTarget.UniformBuffer, (IntPtr)64, 64, ref proj);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);
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
