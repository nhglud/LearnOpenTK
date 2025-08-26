using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;



namespace LearnOpenTK
{
    public class PostProcessingFilter
    {

        public Shader filterShader;
        public Framebuffer filterFrameBuffer;
        private int vao;

        public PostProcessingFilter(Shader shader, int width, int height)
        {

            this.filterShader = shader;
            vao = GL.GenVertexArray();
            filterFrameBuffer = new Framebuffer(width, height);
        }


        public void Apply(int width, int height)
        {
            filterFrameBuffer.Bind(width, height);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.DepthTest);

            filterShader.Use();

            filterShader.SetInt("screenTexture", 0);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            filterFrameBuffer.Unbind(width, height);

            filterFrameBuffer.BindTexture();
        }

        public void Resize(int width, int height)
        {
            filterFrameBuffer.Resize(width, height);
        }

    }
}
