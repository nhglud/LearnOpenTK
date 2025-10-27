using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


using OpenTK.Graphics.OpenGL4;

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



            TerrainGenerator terrainGenerator = new TerrainGenerator();
            terrainGenerator.CreatePlane(10, 10, 4, 4, 4, 4);


            Entity entity = new Entity();
            entity.name = "Terrain";
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.One);
            entity.AddComponent(terrainGenerator.GetMesh());
            entity.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


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
