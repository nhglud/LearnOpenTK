//using System;
//using System.Collections.Generic;
//using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
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

        private Transform transform;
        


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

            transform = new Transform(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(90.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f));

            shader.Use();
            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            GL.Enable(EnableCap.DepthTest);

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

            Matrix4 view = Matrix4.LookAt(
                new Vector3(0.0f, 0.0f, 3.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f));

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();
            shader.SetMat4("model", transform.GetMatrixTransform());
            shader.SetMat4("view", view);
            shader.SetMat4("projection", projection);

            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            mesh.Draw();

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
