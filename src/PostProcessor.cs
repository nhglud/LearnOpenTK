using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;



namespace LearnOpenTK
{
    public class PostProcessor
    {
        public Shader postProcessingShader;
        private int postProcessingQuad;
        //private Framebuffer framebuffer;
        private Dictionary<string, PostProcessingFilter> filters;


        public PostProcessor(Shader postProcessingShader)
        {
            this.postProcessingShader = postProcessingShader;
            postProcessingQuad = GL.GenVertexArray();

            filters = new Dictionary<string, PostProcessingFilter>();

        }

        public void ApplyPostProcessing(FrameEventArgs e, int width, int height)
        {
            //framebuffer.BindTexture();

            foreach (var filter in filters.Values)
            {
                filter.Apply(width, height);
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.DepthTest);

            postProcessingShader.Use();

            postProcessingShader.SetInt("screenTexture", 0);

            GL.BindVertexArray(postProcessingQuad);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }

        public void AddFilter(string name, PostProcessingFilter filter)
        {
            if(!filters.ContainsKey(name))
                filters.Add(name, filter);
        }

        public void RemoveFilter(string name)
        {
            filters.Remove(name);
        }

        public void Resize(int width, int height)
        {
            foreach (var filter in filters.Values)
            {
                filter.Resize(width, height);

            }

        }

    }
}
