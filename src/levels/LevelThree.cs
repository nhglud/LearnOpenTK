

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    public class LevelThree : Level
    {

        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;

        public LevelThree(Game game) : base(game)
        {


        }

        public override void LoadLevel()
        {
            base.LoadLevel();


            Mesh mesh = AssetManager.GetMesh("cube");
            Mesh monke = AssetManager.GetMesh("monke");

            Vector3 ambientColor = new Vector3(0.3f, 0.2f, 0.32f);
            float ambientStrength = 0.8f;

            Vector3 lightPosition = new Vector3(0.0f, 4.0f, 2.0f);
            Vector3 lightColor = new Vector3(1.0f, 1.0f, 1.0f);


            LitMaterial.SetAmbient(ambientColor, ambientStrength);
            LitMaterial.SetDiffuse(lightPosition, lightColor);

            Entity lightEntity = new Entity();
            lightEntity.transform = new Transform(lightPosition, Vector3.Zero, 0.2f * Vector3.One);

            lightEntity.AddComponent(mesh);
            lightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity.AddComponent(new DirectionalLight((Color4.White), Vector3.Normalize(new Vector3(-1.0f, -2.0f, 0.0f))));


            Entity entity = new Entity();
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), Vector3.One);
            entity.AddComponent(AssetManager.GetMesh("torus"));
            entity.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity entity2 = new Entity();
            entity2.transform = new Transform(new Vector3(0.5f, 0.0f, -5.0f), new Vector3(0.0f, -30.0f, 0.0f), Vector3.One);
            entity2.AddComponent(AssetManager.GetMesh("sphere"));
            entity2.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity entity3 = new Entity();
            entity3.transform = new Transform(new Vector3(3.5f, 0.0f, 2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));
            entity3.AddComponent(monke);
            entity3.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity entity4 = new Entity();
            entity4.transform = new Transform(new Vector3(0.0f, -2.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(8.0f, 1.0f, 8.0f));
            entity4.AddComponent(AssetManager.GetMesh("quad"));
            entity4.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity player = new Entity();

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
