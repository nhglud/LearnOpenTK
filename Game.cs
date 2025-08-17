using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace LearnOpenTK
{
    public class Game : GameWindow
    {

        private Mesh mesh;
        private Shader shader;
        private Texture texture;
        private Texture texture2;


        public Game(int width, int height, string title) : 
            base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title }) 
        {
           
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            AssetManager.LoadAssets();

            mesh = AssetManager.GetMesh("quad");
            shader = AssetManager.GetShader("basic");
            texture = AssetManager.GetTexture("container");
            texture2 = AssetManager.GetTexture("awesomeface");

            shader.Use();
            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();



            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            mesh.Draw();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }


    }
}
