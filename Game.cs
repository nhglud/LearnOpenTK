

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace LearnOpenTK
{

    public class Game : GameWindow
    {

        private Level currentLevel;
        private Framebuffer framebuffer;
        private Shader postProcessingShader;
        private int postProcessingQuad;

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

            //currentLevel = new LevelTwo(this);

            currentLevel.LoadLevel();


            framebuffer = new Framebuffer(ClientSize.X, ClientSize.Y);
            postProcessingShader = AssetManager.GetShader("post_processing");
            postProcessingQuad = GL.GenVertexArray();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            currentLevel.UpdateLevel(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            framebuffer.Bind(ClientSize.X, ClientSize.Y);
            
                GL.Enable(EnableCap.DepthTest);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                currentLevel.RenderLevel(e);

            framebuffer.Unbind(ClientSize.X, ClientSize.Y);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.DepthTest);

            postProcessingShader.Use();
            framebuffer.BindTexture();
            postProcessingShader.SetInt("screenTexture", 0);

            GL.BindVertexArray(postProcessingQuad);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            ClientSize = (e.Width, e.Height);
        }

        public void ChangeLevel(Level newLevel)
        {
            currentLevel = newLevel;
            newLevel.LoadLevel();
        }

    }

}