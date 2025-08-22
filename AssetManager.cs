
using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public static class AssetManager
    {
        private const string path = "C:\\programming\\LearnOpenTK\\";


        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        private static Dictionary<string, Material> materials = new Dictionary<string, Material>();


        public static void LoadAssets()
        {
            Shader shader = new Shader(path + "basic_shader.vert", path + "basic_shader.frag");
            Shader singleColorShader = new Shader(path + "basic_shader.vert", path + "single_color.frag");
            Shader litShader = new Shader(path + "basic_shader.vert", path + "lit_shader.frag");


            Texture texture = new Texture(path + "container.jpg");
            Texture awesomeFace = new Texture(path + "awesomeface.png");


            AddMesh("cube", ModelLoader.LoadModel(path + "cube.obj"));
            AddMesh("monke", ModelLoader.LoadModel(path + "monke.obj"));
            AddMesh("quad", ModelLoader.LoadModel(path + "quad.obj"));


            AddShader("basic", shader);
            AddShader("single_color", singleColorShader);
            AddShader("lit", litShader);


            AddTexture("container", texture);
            AddTexture("awesomeface", awesomeFace);


            //AddMaterial("lit_material", new LitMaterial(new Vector3(0.4f, 0.36f, 0.23f)));

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

        public static void AddMaterial(string name, Material material)
        {
            materials.Add(name, material);
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

        public static Material GetMaterial(string name)
        {
            return materials[name];
        }



    }
}
