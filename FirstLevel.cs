
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    public class FirstLevel : Level
    {
        //private Texture texture;
        //private Texture texture2;

        private LightingSystem lightingSystem;

        public FirstLevel(Game game) : base(game)
        {
        }

        public override void LoadLevel()
        {
            base.LoadLevel();

            //AssetManager.LoadAssets();

            //texture = AssetManager.GetTexture("container");
            //texture2 = AssetManager.GetTexture("awesomeface");


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
            lightEntity.AddComponent(new PointLight(Vector3.One));


            Entity lightEntity2 = new Entity();
            lightEntity2.transform = new Transform(new Vector3(1.3f, 1.4f, -1.0f), Vector3.Zero, 0.2f * Vector3.One);

            lightEntity2.AddComponent(mesh);
            lightEntity2.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity2.AddComponent(new PointLight(Vector3.One));


            Entity lightEntity3 = new Entity();
            lightEntity3.transform = new Transform(new Vector3(-1.3f, 1.4f, -1.0f), Vector3.Zero, 0.2f * Vector3.One);

            lightEntity3.AddComponent(mesh);
            lightEntity3.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity3.AddComponent(new PointLight(Vector3.One));




            Entity lightEntity4 = new Entity();
            lightEntity4.transform = new Transform(new Vector3(2.3f, 5.4f, 1.0f), Vector3.Zero, 0.2f * Vector3.One);
            lightEntity4.AddComponent(mesh);
            lightEntity4.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity4.AddComponent(new PointLight(Vector3.One));



            Entity entity = new Entity();
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), Vector3.One);
            entity.AddComponent(mesh);
            entity.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity entity2 = new Entity();
            entity2.transform = new Transform(new Vector3(0.5f, 0.0f, -5.0f), new Vector3(0.0f, -30.0f, 0.0f), Vector3.One);
            entity2.AddComponent(mesh);
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


            entities.Add(player);
            entities.Add(entity);
            entities.Add(entity2);
            entities.Add(entity3);
            entities.Add(entity4);
            entities.Add(lightEntity);
            entities.Add(lightEntity2);
            entities.Add(lightEntity3);
            entities.Add(lightEntity4);


            AssetManager.GetShader("basic").Use();
            AssetManager.GetShader("basic").SetInt("texture0", 0);
            AssetManager.GetShader("basic").SetInt("texture1", 1);

            List<PointLight> lights = new List<PointLight>();

            lights.Add(lightEntity.GetComponent<PointLight>());
            lights.Add(lightEntity2.GetComponent<PointLight>());
            lights.Add(lightEntity3.GetComponent<PointLight>());
            lights.Add(lightEntity4.GetComponent<PointLight>());

            lightingSystem = new LightingSystem(lights, new List<DirectionalLight>() { new DirectionalLight(Vector3.One, Vector3.One) });
        }


        public override void UpdateLevel(FrameEventArgs e)
        {
            base.UpdateLevel(e);

            Component.UpdateComponents(e);
        }

        public override void RenderLevel(FrameEventArgs e)
        {
            base.RenderLevel(e);

            Camera.main.UpdateUBO(game.ClientSize.X, game.ClientSize.Y);
            lightingSystem.Update();

            LitMaterial.UpdateStaticProperties();

            //texture.Use(TextureUnit.Texture0);
            //texture2.Use(TextureUnit.Texture1);

            Renderer.UpdateRenderers();

        }


    }
}
