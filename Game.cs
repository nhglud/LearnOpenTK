

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace LearnOpenTK
{
    public class Game : GameWindow
    {

        private Shader shader;
        private Texture texture;
        private Texture texture2;

        //private Camera camera;
        private Entity entity;
        private Entity player;


        public Game(int width, int height, string title) : 
            base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title }) 
        {
           
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.33f, 1.0f);

            CursorState = CursorState.Grabbed;

            AssetManager.LoadAssets();

            Mesh mesh = AssetManager.GetMesh("cube");
            shader = AssetManager.GetShader("basic");
            texture = AssetManager.GetTexture("container");
            texture2 = AssetManager.GetTexture("awesomeface");

            
            //camera = new Camera(new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f)));

            shader.Use();
            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);


            entity = new Entity();
            entity.transform = new Transform(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 32.0f, 54.0f), new Vector3(1.0f, 1.0f, 1.0f));
            
            entity.AddComponent(mesh);
            entity.AddComponent(new Renderer(shader));


            player = new Entity();

            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));

            player.AddComponent(new Camera());
            player.AddComponent(new CharactorController(player, KeyboardState));

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            
            player.GetComponent<CharactorController>().Update(e);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();

            shader.SetMat4("view", Camera.main.GetViewMatrix());
            shader.SetMat4("projection", Camera.main.GetProjectionMatrix(ClientSize.X / (float)ClientSize.Y));


            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            entity.GetComponent<Renderer>().Render();

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
