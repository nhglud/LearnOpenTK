using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//using System.Numerics;



namespace LearnOpenTK
{
    public class PostProcessor
    {
        public Shader postProcessingShader;
        private int postProcessingQuad;
        //private Framebuffer framebuffer;
        private Dictionary<string, PostProcessingFilter> filters;
        private Game game;

        public PostProcessor(Shader postProcessingShader, Game game)
        {
            this.postProcessingShader = postProcessingShader;
            postProcessingQuad = GL.GenVertexArray();

            filters = new Dictionary<string, PostProcessingFilter>();
            this.game = game;
        }

        public void ApplyPostProcessing(FrameEventArgs e, int width, int height)
        {
            //framebuffer.BindTexture();

            foreach (var filter in filters.Values)
            {
                filter.Apply(width, height);
                filter.filterShader.SetVector2("resolution", new Vector2(game.ClientSize.X, game.ClientSize.Y));
                filter.filterShader.SetFloat("time", game.elapsedTime);
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
