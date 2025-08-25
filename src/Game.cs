

using goo;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;
using LearnOpenTK.src;

namespace LearnOpenTK
{

    public class Game : GameWindow
    {

        private Level currentLevel;
        private Framebuffer framebuffer;
        private PostProcessor postProcessor;
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

            CursorState = CursorState.Grabbed;

            AssetManager.LoadAssets();

            currentLevel = new FirstLevel(this);


            currentLevel.LoadLevel();


            framebuffer = new Framebuffer(ClientSize.X, ClientSize.Y);

            postProcessor = new PostProcessor(AssetManager.GetShader("post_processing"));


            uiManager = new UIManager(this);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // Handle Inputs

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

            currentLevel.UpdateLevel(e);


            // Update UI

            uiManager.Update(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // SCENE RENDERING
            
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            currentLevel.RenderLevel(e);

            framebuffer.Bind(ClientSize.X, ClientSize.Y);

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            currentLevel.RenderLevel(e);

            framebuffer.Unbind(ClientSize.X, ClientSize.Y);

            // POST PROCESSING

            postProcessor.ApplyPostProcessing(e, framebuffer);

            // RENDER UI

            uiManager.Render();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            uiManager.Resize(e);
            framebuffer.Resize(e.Width, e.Height);
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