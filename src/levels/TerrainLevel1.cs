using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.src.shaders;

namespace LearnOpenTK.src.levels
{
    public class TerrainLevel1 : Level
    {

        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;

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
            terrainGenerator.CreatePlane(40, 40, 600, 600, 1, 1);


            Entity terrain = new Entity();
            terrain.name = "Terrain";
            terrain.transform = new Transform(new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.2f * 5, 1, 5));
            terrain.AddComponent(terrainGenerator.GetMesh());

            var heightmap = new Texture(path + "assets/hills_heightmap.png");
            var terrainDiffuse  = new Texture(path + "assets/hills_diffuse.png");
            var terrianShader = new Shader(path + "src/shaders/terrain.vert", path + "src/shaders/terrain.frag");
            var terrainMat = new TerrainMaterial(heightmap, terrianShader, terrainDiffuse);

            terrain.AddComponent(new Renderer(terrainMat));

            var billboardMat = new TerrainBillboardMaterial(heightmap, new Texture(AssetManager.path + "assets/grass.png"), 60.0f);
            terrain.GetComponent<Renderer>().AddMaterial(billboardMat);


            Entity player = new Entity();
            player.name = "Camera";
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));


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


            Camera.main.UpdateUBO(game.ClientSize.X, game.ClientSize.Y);
            lightingSystem.Update();

            LitMaterial.UpdateStaticProperties();
            renderingSystem.Render();

            framebuffer.Unbind(game.ClientSize.X, game.ClientSize.Y);
            framebuffer.BindTexture();

        }


    }
}
