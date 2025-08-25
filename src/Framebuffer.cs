

using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK
{
    public class Framebuffer
    {
        private int frameBufferObject;
        private int renderBufferObject;
        public int colorTexture;


        public Framebuffer(int width, int height)
        {
            frameBufferObject = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferObject);

            colorTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, colorTexture);
            GL.TexImage2D(
                TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 
                0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                                    TextureTarget.Texture2D, colorTexture, 0);

            renderBufferObject = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBufferObject);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, width, height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment,
                                       RenderbufferTarget.Renderbuffer, renderBufferObject);


            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception($"Framebuffer is not complete: {status}");
            }

            // Unbind
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Bind(int width, int height)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferObject);
            GL.Viewport(0, 0, width, height);
        }

        public void BindTexture()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, colorTexture);
        }

        public void Unbind(int width, int height)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, width, height);
        }


        public void Resize(int width, int height)
        {
            // Resize color texture
            GL.BindTexture(TextureTarget.Texture2D, colorTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height,
                          0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            // Resize depth-stencil renderbuffer
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBufferObject);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, width, height);
        }



    }
}
