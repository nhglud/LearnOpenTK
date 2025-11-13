using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.src.shaders;
using LearnOpenTK.src.components;

namespace LearnOpenTK.src.levels
{
    public class TerrainLevel1 : Level
    {

        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;
        private SkyBox skyBox;
        private float time;
        public TerrainLevel1(Game game) : base(game)
        {


        }



        public override void LoadLevel()
        {
            base.LoadLevel();

            Mesh mesh = AssetManager.GetMesh("cube");


            Vector3 lightPosition = new Vector3(0.0f, 4.0f, 2.0f);


            Entity lightEntity = new Entity();
            lightEntity.transform = new Transform(lightPosition, Vector3.Zero, 0.2f * Vector3.One);

            lightEntity.AddComponent(mesh);
            lightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity.AddComponent(new DirectionalLight((Color4.White), Vector3.Normalize(new Vector3(-1.0f, -2.0f, 0.0f))));


            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

            PlaneGenerator terrainGenerator = new PlaneGenerator();
            terrainGenerator.CreatePlane(20, 20, 100, 100, 1, 1);


            Entity terrain = new Entity();
            terrain.name = "Terrain";
            terrain.transform = new Transform(new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.2f * 5, 1, 5));
            terrain.AddComponent(terrainGenerator.GetMesh());

            var heightmap = new Texture(path + "assets/hills_heightmap.png");
            var terrainDiffuse  = new Texture(path + "assets/hills_diffuse.png");
            var terrianShader = new Shader(path + "src/shaders/terrain.vert", path + "src/shaders/terrain.frag");

            float heightScale = 100.0f;
            var terrainMat = new TerrainMaterial(heightmap, terrianShader, terrainDiffuse, heightScale);

            terrain.AddComponent(new Renderer(terrainMat));

            var billboardMat = new TerrainBillboardMaterial(heightmap, new Texture(AssetManager.path + "assets/grass.png"), heightScale);
            terrain.GetComponent<Renderer>().AddMaterial(billboardMat);


            Entity player = new Entity();
            player.name = "Camera";
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));


            Entity entity = new Entity();
            entity.name = "Torus";
            entity.transform = new Transform(new Vector3(0.0f, 20.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), 5.0f * Vector3.One);
            entity.AddComponent(AssetManager.GetMesh("torus"));
            entity.AddComponent(new Rotator());

            entity.AddComponent(new Renderer(AssetManager.GetMaterial("white")));


            List<string> cubemapFaces = new List<string>()
            {
                AssetManager.path + "assets/skybox/right.jpg",   // +X
                AssetManager.path + "assets/skybox/left.jpg",    // -X
                AssetManager.path + "assets/skybox/top.jpg",     // +Y
                AssetManager.path + "assets/skybox/bottom.jpg",  // -Y
                AssetManager.path + "assets/skybox/front.jpg",   // +Z
                AssetManager.path + "assets/skybox/back.jpg"     // -Z
            };

            skyBox = new SkyBox(cubemapFaces);


            lightingSystem = new LightingSystem(entities);
            renderingSystem = new RenderingSystem(entities);
            updateSystem = new UpdateSystem(entities);
            framebuffer = new Framebuffer(game.ClientSize.X, game.ClientSize.Y);
        }


        public override void UpdateLevel(FrameEventArgs e)
        {
            base.UpdateLevel(e);

            updateSystem.Update(e);

            //Component.UpdateComponents(e)
        }

        public override void RenderLevel(FrameEventArgs e)
        {
            base.RenderLevel(e);

            GL.Enable(EnableCap.DepthTest);

            framebuffer.Bind(game.ClientSize.X, game.ClientSize.Y);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            time += (float)e.Time;
            TerrainBillboardMaterial.time = time;

            Camera.main.UpdateUBO(game.ClientSize.X, game.ClientSize.Y);
            lightingSystem.Update();

            LitMaterial.UpdateStaticProperties();
            renderingSystem.Render();
            skyBox.draw();

            framebuffer.Unbind(game.ClientSize.X, game.ClientSize.Y);
            framebuffer.BindTexture();

        }


    }
}
