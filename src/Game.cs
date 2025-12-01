

using goo;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;


namespace LearnOpenTK
{
    public class Game : GameWindow
    {
        public float elapsedTime = 0;
        public Level currentLevel;
        //private Framebuffer framebuffer;
        public PostProcessor postProcessor;
        private UIManager uiManager;

        public Game(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.33f, 1.0f);

            AssetManager.LoadAssets();

            currentLevel = new FirstLevel(this);
            currentLevel.LoadLevel();

            //framebuffer = new Framebuffer(ClientSize.X, ClientSize.Y);
            postProcessor = new PostProcessor(AssetManager.GetShader("post_processing"), this);


            uiManager = new UIManager(this);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            elapsedTime += (float)e.Time;
            // HANDLE INPUTS

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if(MouseState.IsButtonDown(MouseButton.Right))
            {
                CursorState = CursorState.Grabbed;
            }
            else
            {
                CursorState = CursorState.Normal;

            }

            // UPDATE SCENE

            currentLevel.UpdateLevel(e);

            // UPDATE UI
            
            uiManager.Update(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // SCENE RENDERING
            currentLevel.RenderLevel(e);

            // POST PROCESSING
            postProcessor.ApplyPostProcessing(e, ClientSize.X, ClientSize.Y);

            // RENDER UI

            uiManager.Render();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            uiManager.Resize(e);
            currentLevel.framebuffer.Resize(e.Width, e.Height);
            postProcessor.Resize(e.Width, e.Height);
            ClientSize = (e.Width, e.Height);
        }

        public void ChangeLevel(Level newLevel)
        {
            currentLevel.Clear();
            Camera.main = null;
            currentLevel = newLevel;
            newLevel.LoadLevel();
        }

    }

}