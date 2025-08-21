namespace LearnOpenTK
{
    public static class AssetManager
    {
        private const string path = "C:\\programming\\LearnOpenTK\\";



        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public static void LoadAssets()
        {
            Shader shader = new Shader(path + "basic_shader.vert", path + "basic_shader.frag");
            Shader singleColorShader = new Shader(path + "basic_shader.vert", path + "single_color.frag");

            Texture texture = new Texture(path + "container.jpg");
            Texture awesomeFace = new Texture(path + "awesomeface.png");

            //AddMesh("triangle", new Mesh(triangleVertices, triangleIndices));
            //AddMesh("quad", new Mesh(quadVertices, quadIndices));
            //AddMesh("cube", new Mesh(cubeVertices, cubeIndices));


            AddMesh("cube", ModelLoader.LoadModel(path + "cube.obj"));
            AddMesh("monke", ModelLoader.LoadModel(path + "monke.obj"));



            AddShader("basic", shader);
            AddShader("single_color", singleColorShader);


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
