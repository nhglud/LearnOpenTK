
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
            Shader shader = new Shader(path + "src/shaders/basic_shader.vert", path + "src/shaders/basic_shader.frag");
            Shader singleColorShader = new Shader(path + "src/shaders/basic_shader.vert", path + "src/shaders/single_color.frag");
            Shader litShader = new Shader(path + "src/shaders/basic_shader.vert", path + "src/shaders/lit_shader.frag");
            Shader postPorcessingShader = new Shader(path + "src/shaders/post_processing_shader.vert", path + "src/shaders/post_processing_shader.frag");
            Shader negativeFilter = new Shader(path + "src/shaders/post_processing_shader.vert", path + "src/shaders/negative_filter.frag");
            Shader bnwFilter = new Shader(path + "src/shaders/post_processing_shader.vert", path + "src/shaders/bnw_filter.frag");
            Shader toonFilter = new Shader(path + "src/shaders/post_processing_shader.vert", path + "src/shaders/toon_filter.frag");
            Shader pixelateFilter = new Shader(path + "src/shaders/post_processing_shader.vert", path + "src/shaders/pixelate_filter.frag");




            Texture texture = new Texture(path + "assets/container.jpg");
            Texture awesomeFace = new Texture(path + "assets/awesomeface.png");
            Texture diffuse = new Texture(path + "assets/container2_diffuse.png");
            Texture specular = new Texture(path + "assets/container2_specular.png");
            Texture checker = new Texture(path + "assets/checker.png");
            Texture white= new Texture(path + "assets/white.png");




            AddMesh("cube", ModelLoader.LoadModel(path + "assets/cube.obj"));
            AddMesh("monke", ModelLoader.LoadModel(path + "assets/monke.obj"));
            AddMesh("quad", ModelLoader.LoadModel(path + "assets/quad.obj"));
            AddMesh("sphere", ModelLoader.LoadModel(path + "assets/sphere.obj"));
            AddMesh("torus", ModelLoader.LoadModel(path + "assets/torus.obj"));


            AddShader("basic", shader);
            AddShader("single_color", singleColorShader);
            AddShader("lit", litShader);
            AddShader("post_processing", postPorcessingShader);
            AddShader("negative_filter", negativeFilter);
            AddShader("bnw_filter", bnwFilter);
            AddShader("toon_filter", toonFilter);
            AddShader("pixelate_filter", pixelateFilter);




            AddTexture("container", texture);
            AddTexture("awesomeface", awesomeFace);
            AddTexture("container2_diffuse", diffuse);
            AddTexture("container2_specular", specular);
            AddTexture("checker", checker);
            AddTexture("white", white);



            AddMaterial("container2_mat", new LitMaterial(GetTexture("container2_diffuse"), GetTexture("container2_specular")));
            AddMaterial("checker", new LitMaterial(GetTexture("checker"), GetTexture("white")));
            AddMaterial("white", new LitMaterial(GetTexture("white"), GetTexture("white")));



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
