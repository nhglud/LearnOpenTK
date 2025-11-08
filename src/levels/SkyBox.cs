using OpenTK.Graphics.OpenGL4;

namespace LearnOpenTK.src
{
    public class SkyBox
    {
        private CubeMap cubeMap;
        private Shader shader;
        private int vao;
        private int vbo;
        private float[] skyboxVertices = {
             -1.0f,  1.0f, -1.0f,
             -1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,

             -1.0f, -1.0f,  1.0f,
             -1.0f, -1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f,  1.0f,
             -1.0f, -1.0f,  1.0f,

              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,

             -1.0f, -1.0f,  1.0f,
             -1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f, -1.0f,  1.0f,
             -1.0f, -1.0f,  1.0f,

             -1.0f,  1.0f, -1.0f,
              1.0f,  1.0f, -1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
             -1.0f,  1.0f,  1.0f,
             -1.0f,  1.0f, -1.0f,

             -1.0f, -1.0f, -1.0f,
             -1.0f, -1.0f,  1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
             -1.0f, -1.0f,  1.0f,
              1.0f, -1.0f,  1.0f
        };

        public SkyBox(List<string> faces)
        {
            shader = new Shader(
               AssetManager.path + "src/shaders/skybox.vert",
               AssetManager.path + "src/shaders/skybox.frag");

            cubeMap = new CubeMap(faces);
            shader.Use();
            shader.SetInt("skybox", 0);
            BindBuffer();

        }

        private void BindBuffer()
        {
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindVertexArray(vao);


            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, skyboxVertices.Length * sizeof(float), skyboxVertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void draw()
        {
            GL.DepthFunc(DepthFunction.Lequal);
            GL.DepthMask(false);
            shader.Use();

            GL.BindVertexArray(vao);
            cubeMap.Use();
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Less);
        }

        public CubeMap GetCubeMap()
        {
            return cubeMap;
        }

    }
}
