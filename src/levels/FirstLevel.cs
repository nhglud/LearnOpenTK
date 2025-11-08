

using LearnOpenTK.src;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    public class FirstLevel : Level
    {
        private SkyBox skyBox;
        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;

        public FirstLevel(Game game) : base(game)
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

            //Entity lightEntity = new Entity();
            //lightEntity.transform = new Transform(lightPosition, Vector3.Zero, 0.2f * Vector3.One);

            //lightEntity.AddComponent(mesh);
            //lightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            //lightEntity.AddComponent(new PointLight(Color4.Green));


            Entity lightEntity2 = new Entity();
            lightEntity2.name = "Light 1";
            lightEntity2.transform = new Transform(new Vector3(1.3f, 1.4f, -1.0f), Vector3.Zero, 0.2f * Vector3.One);

            lightEntity2.AddComponent(mesh);
            lightEntity2.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity2.AddComponent(new PointLight(Color4.AliceBlue));


            Entity lightEntity3 = new Entity();
            lightEntity3.name = "Light 2";
            lightEntity3.transform = new Transform(new Vector3(-1.3f, 1.4f, -1.0f), Vector3.Zero, 0.2f * Vector3.One);

            lightEntity3.AddComponent(mesh);
            lightEntity3.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity3.AddComponent(new PointLight(Color4.BlueViolet));


            Entity lightEntity4 = new Entity();
            lightEntity4.name = "Light 3";
            lightEntity4.transform = new Transform(new Vector3(2.3f, 5.4f, 1.0f), Vector3.Zero, 0.2f * Vector3.One);
            lightEntity4.AddComponent(mesh);
            lightEntity4.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity4.AddComponent(new PointLight(Color4.DeepPink));


            Entity entity = new Entity();
            entity.name = "Torus";
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), Vector3.One);
            entity.AddComponent(AssetManager.GetMesh("torus"));
            entity.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity entity2 = new Entity();
            entity2.name = "Sphere";
            entity2.transform = new Transform(new Vector3(5.5f, 0.0f, 15.0f), new Vector3(0.0f, -30.0f, 0.0f), 5 * Vector3.One);
            entity2.AddComponent(AssetManager.GetMesh("sphere"));
            entity2.AddComponent(new Renderer(AssetManager.GetMaterial("white")));


            Entity entity3 = new Entity();
            entity3.name = "Monke";
            entity3.transform = new Transform(new Vector3(3.5f, 0.0f, 2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));
            entity3.AddComponent(monke);
            entity3.AddComponent(new Renderer(AssetManager.GetMaterial("white")));
            

            Entity entity4 = new Entity();
            entity4.name = "Floor";
            entity4.transform = new Transform(new Vector3(0.0f, -2.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(8.0f, 1.0f, 8.0f));
            entity4.AddComponent(AssetManager.GetMesh("quad"));
            entity4.AddComponent(new Renderer(AssetManager.GetMaterial("checker")));



            Entity entity5 = new Entity();
            entity5.name = "Cube";
            entity5.transform = new Transform(new Vector3(-2.5f, 0.5f, -3.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.One);
            entity5.AddComponent(AssetManager.GetMesh("cube"));
            entity5.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));

            Entity entity6 = new Entity();
            entity6.name = "Cube";
            entity6.transform = new Transform(new Vector3(-2.5f, 0.5f, 3.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.One);
            entity6.AddComponent(AssetManager.GetMesh("cube"));
            entity6.AddComponent(new Renderer(AssetManager.GetMaterial("brickwall")));




            Entity player = new Entity();
            player.name = "Camera";
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));


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

            var white = AssetManager.GetMaterial("white") as LitMaterial;

            white.SetEnvironmentMap(skyBox.GetCubeMap());
            white.reflectivity = 1;
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
            skyBox.draw();
            

            framebuffer.Unbind(game.ClientSize.X, game.ClientSize.Y);
            framebuffer.BindTexture();


        }


    }
}
