

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

        private Level currentLevel;
        private Framebuffer framebuffer;
        private Shader postProcessingShader;
        private int postProcessingQuad;

        private ImGuiController uiController;

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

            ImGui.CreateContext();
            ImGui.GetIO().Fonts.AddFontDefault();

            uiController = new ImGuiController(ClientSize.X, ClientSize.Y);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

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


            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.DepthTest);

            postProcessingShader.Use();
    
            framebuffer.BindTexture();
            postProcessingShader.SetInt("screenTexture", 0);

            GL.BindVertexArray(postProcessingQuad);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

            // UI

            uiController.Update(this, (float)e.Time);

            ImGui.Begin("Select Level");
            if (ImGui.Button("Level 1"))
                ChangeLevel(new FirstLevel(this));

            if (ImGui.Button("Level 2"))
                ChangeLevel(new LevelTwo(this));


            ImGui.End();

            uiController.Render();
            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            uiController.WindowResized(e.Width, e.Height);
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