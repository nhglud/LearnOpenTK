using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK.src
{
    public class PostProcessor
    {
        private Shader postProcessingShader;
        private int postProcessingQuad;
        //private Framebuffer framebuffer;

        public PostProcessor(Shader postProcessingShader)
        {
            this.postProcessingShader = postProcessingShader;
            postProcessingQuad = GL.GenVertexArray();
        }
        public void ApplyPostProcessing(FrameEventArgs e, Framebuffer framebuffer)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.DepthTest);

            postProcessingShader.Use();

            framebuffer.BindTexture();
            postProcessingShader.SetInt("screenTexture", 0);

            GL.BindVertexArray(postProcessingQuad);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }
    }
}
