
using OpenTK.Mathematics;

using System.IO;

namespace LearnOpenTK
{
    public static class AssetManager
    {
        private static string path = "";


        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        private static Dictionary<string, Material> materials = new Dictionary<string, Material>();


        public static void LoadAssets()
        {

            path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

  
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
            Texture containerNormal = new Texture(path + "assets/container2_normal.png");

            Texture checker = new Texture(path + "assets/checker.png");
            Texture white = new Texture(path + "assets/white.png");
            Texture blue = new Texture(path + "assets/normalmap.png");
            Texture brickwall = new Texture(path + "assets/brickwall.jpg");
            Texture brickwallNormal = new Texture(path + "assets/brickwall_normal.jpg");
            Texture black = new Texture(path + "assets/solid_black.png");

            Texture blood = new Texture(path + "assets/blood_splatter_0.png");
            Texture bloodSpecular = new Texture(path + "assets/blood_specular.png");

            Texture bloodNormal = new Texture(path + "assets/blood_normal.png");



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
            AddTexture("container2_normal", containerNormal);

            AddTexture("checker", checker);
            AddTexture("white", white);
            AddTexture("blue", blue);

            AddTexture("blood", blood);
            AddTexture("blood_specular", bloodSpecular);
            AddTexture("blood_normal", bloodNormal);



            AddMaterial(
                "container2_mat", 
                new LitMaterial(
                    GetTexture("container2_diffuse"), 
                    GetTexture("container2_specular"), 
                    GetTexture("container2_normal")
            ));



            AddMaterial(
                "checker", 
                new LitMaterial(
                    GetTexture("checker"), 
                    GetTexture("white"), 
                    GetTexture("blue")
            ));

            AddMaterial(
                "white", 
                new LitMaterial(
                    GetTexture("white"), 
                    GetTexture("white"), 
                    GetTexture("blue")
            ));
            
            AddMaterial(
                "brickwall", 
                new LitMaterial(
                    brickwall, 
                    white, 
                    brickwallNormal
            ));


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
