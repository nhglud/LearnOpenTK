namespace LearnOpenTK
{
    public static class AssetManager
    {

        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public static void LoadAssets()
        {
            float[] triangleVertices = {
                  -0.5f, -0.5f, 0.0f, //Bottom-left vertex
                   0.5f, -0.5f, 0.0f, //Bottom-right vertex
                   0.0f,  0.5f, 0.0f  //Top vertex
            };

            int[] triangleIndices = { 0, 1, 2 };

            float[] quadVertices =
            {
                //Position          Texture coordinates
                 0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
                 0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
                -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
                -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
            };

            int[] quadIndices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };


            float[] cubeVertices = {
                 -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
                  0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
                  0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
                  0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
                 -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
                 -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

                 -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
                  0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
                  0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
                  0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
                 -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
                 -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

                 -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
                 -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
                 -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
                 -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
                 -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
                 -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

                  0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
                  0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
                  0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
                  0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
                  0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
                  0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

                 -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
                  0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
                  0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
                  0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
                 -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
                 -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

                 -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
                  0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
                  0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
                  0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
                 -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
                 -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
            };

            int[] cubeIndices = new int[cubeVertices.Length];

            for (int i = 0; i < cubeVertices.Length; i++)
            {
                cubeIndices[i] = i;
            }


            Shader shader = new Shader("C:\\programming\\LearnOpenTK\\basic_shader.vert", "C:\\programming\\LearnOpenTK\\basic_shader.frag");

            Texture texture = new Texture("C:\\programming\\LearnOpenTK\\container.jpg");
            Texture awesomeFace = new Texture("C:\\programming\\LearnOpenTK\\awesomeface.png");

            AddMesh("triangle", new Mesh(triangleVertices, triangleIndices));
            AddMesh("quad", new Mesh(quadVertices, quadIndices));
            AddMesh("cube", new Mesh(cubeVertices, cubeIndices));

            AddShader("basic", shader);

            AddTexture("container", texture);
            AddTexture("awesomeface", awesomeFace);
        }

        public static void AddMesh(string name, Mesh mesh)
        {
            meshes.Add(name, mesh);
        }

        public static void AddShader(string name, Shader shader)
        {
            shaders.Add(name, shader);
        }

        public static void AddTexture(string name, Texture texture)
        {
            textures.Add(name, texture);
        }


        public static Mesh GetMesh(string name)
        {
            return meshes[name];
        }

        public static Shader GetShader(string name)
        {
            return shaders[name];
        }

        public static Texture GetTexture(string name)
        {
            return textures[name];
        }

    }
}
