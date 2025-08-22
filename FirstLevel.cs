
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    public class FirstLevel : Level
    {
        private Texture texture;
        private Texture texture2;

        public FirstLevel(Game game) : base(game)
        {
        }

        public override void LoadLevel()
        {
            base.LoadLevel();

            //AssetManager.LoadAssets();

            texture = AssetManager.GetTexture("container");
            texture2 = AssetManager.GetTexture("awesomeface");


            Mesh mesh = AssetManager.GetMesh("cube");
            Mesh monke = AssetManager.GetMesh("monke");

            Vector3 ambientColor = new Vector3(0.3f, 0.2f, 0.32f);
            float ambientStrength = 0.8f;

            Vector3 lightPosition = new Vector3(0.0f, 2.0f, 0.0f);
            Vector3 lightColor = new Vector3(1.0f, 1.0f, 1.0f);


            LitMaterial.SetAmbient(ambientColor, ambientStrength);
            LitMaterial.SetDiffuse(lightPosition, lightColor);


            Entity entity = new Entity();
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), new Vector3(1.0f, 1.0f, 1.0f));
            entity.AddComponent(mesh);
            entity.AddComponent(new Renderer(new LitMaterial(new Vector3(1.0f, 0.3f, 0.5f))));


            Entity entity2 = new Entity();
            entity2.transform = new Transform(new Vector3(0.5f, 0.0f, -2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));
            entity2.AddComponent(mesh);
            entity2.AddComponent(new Renderer(new LitMaterial(new Vector3(0.45f, 0.3f, 0.5f))));

            Entity entity3 = new Entity();
            entity3.transform = new Transform(new Vector3(3.5f, 0.0f, 2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));
            entity3.AddComponent(monke);
            entity3.AddComponent(new Renderer(new LitMaterial(new Vector3(0.25f, 0.53f, 0.5f))));

            Entity entity4 = new Entity();
            entity4.transform = new Transform(new Vector3(0.0f, -2.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(8.0f, 1.0f, 8.0f));
            entity4.AddComponent(AssetManager.GetMesh("quad"));
            entity4.AddComponent(new Renderer(new LitMaterial(new Vector3(0.2f, 0.3f, 0.5f))));

            Entity player = new Entity();

            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));


            entities.Add(player);
            entities.Add(entity);
            entities.Add(entity2);
            entities.Add(entity3);
            entities.Add(entity4);


            AssetManager.GetShader("basic").Use();
            AssetManager.GetShader("basic").SetInt("texture0", 0);
            AssetManager.GetShader("basic").SetInt("texture1", 1);

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

            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            Renderer.UpdateRenderers();

        }


    }
}
