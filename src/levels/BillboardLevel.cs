
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK.src.levels
{
    public class BillboardLevel : Level
    {

        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;


        public BillboardLevel(Game game) : base(game)
        {



        }

        public override void LoadLevel()
        {

            Entity player = new Entity();
            player.name = "Camera";
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));


            var planeGenerator = new PlaneGenerator();
            planeGenerator.CreatePlane(10, 10, 4, 4, 1, 1);
            var plane = planeGenerator.GetMesh();
            var terrain = new Entity();
            terrain.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f), new Vector3(1.0f));
            terrain.AddComponent(plane);

            AssetManager.GetMaterial("checker");


            //var billboardShader = new Shader(
            //    AssetManager.path + "src/shaders/billboard.vert",
            //    AssetManager.path + "src/shaders/billboard.geom",
            //    AssetManager.path + "src/shaders/billboard.frag");


            Texture grassTexture = new Texture(AssetManager.path + "assets/grass.png");
            var billboardMat = new BillboardMaterial(grassTexture);
            

            terrain.AddComponent(new Renderer(AssetManager.GetMaterial("checker")));

            terrain.GetComponent<Renderer>().AddMaterial(billboardMat);

            lightingSystem = new LightingSystem(entities);
            renderingSystem = new RenderingSystem(entities);
            updateSystem = new UpdateSystem(entities);

            framebuffer = new Framebuffer(game.ClientSize.X, game.ClientSize.Y);

        }


        public override void UpdateLevel(FrameEventArgs e)
        {
            base.UpdateLevel(e);

            updateSystem.Update(e);

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
