

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace LearnOpenTK
{
    public class Game : GameWindow
    {
        private Texture texture;
        private Texture texture2;

        private List<Entity> entities;

        public Game(int width, int height, string title) : 
            base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title }) 
        {
           
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //var ml = new ModelLoader("C:\\programming\\LearnOpenTK\\cube.obj");

            //var vs = ml.GetVertices();

            //foreach (var vert in vs)
            //{
            //    Console.WriteLine(vert.ToString());
            //}


            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.33f, 1.0f);

            CursorState = CursorState.Grabbed;

            AssetManager.LoadAssets();
            entities = new List<Entity>();

            Mesh mesh = AssetManager.GetMesh("cube");
            Mesh monke = AssetManager.GetMesh("monke");


            texture = AssetManager.GetTexture("container");
            texture2 = AssetManager.GetTexture("awesomeface");


            Entity entity = new Entity();
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), new Vector3(1.0f, 1.0f, 1.0f));
            
            entity.AddComponent(mesh);
            entity.AddComponent(new Renderer(AssetManager.GetShader("basic")));

            Entity entity2 = new Entity();
            entity2.transform = new Transform(new Vector3(0.5f, 0.0f, -2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));

            entity2.AddComponent(mesh);
            entity2.AddComponent(new Renderer(AssetManager.GetShader("single_color")));


            Entity entity3 = new Entity();
            entity3.transform = new Transform(new Vector3(3.5f, 0.0f, 2.0f), new Vector3(0.0f, -30.0f, 0.0f), new Vector3(1.5f, 1.5f, 1.5f));

            entity3.AddComponent(monke);

            entity3.AddComponent(new Renderer(AssetManager.GetShader("basic")));


            Entity player = new Entity();

            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(KeyboardState, MouseState));

            entities.Add(player);
            entities.Add(entity);
            entities.Add(entity2);
            entities.Add(entity3);


            AssetManager.GetShader("basic").Use();
            AssetManager.GetShader("basic").SetInt("texture0", 0);
            AssetManager.GetShader("basic").SetInt("texture1", 1);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }


            Component.UpdateComponents(e);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Camera.main.UpdateUBO(ClientSize.X, ClientSize.Y);


            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            Renderer.UpdateRenderers();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            ClientSize = (e.Width, e.Height);
        }

    }
}
