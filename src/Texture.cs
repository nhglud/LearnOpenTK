using OpenTK.Graphics.OpenGL4;
using StbImageSharp;


namespace LearnOpenTK
{
    public class Texture
    {

        public int width;
        public int height;
        public ImageResult image;
  

        private int handle;

        private string path;
        private TextureFilter filter;

        public Texture(string path, TextureFilter textureFilter = TextureFilter.TrilinearMipMap)
        {
            this.path = path;
            filter = textureFilter;
            handle = GL.GenTexture();
            Use();
            LoadTexture();
        }


        private void LoadTexture()
        {
            StbImage.stbi_set_flip_vertically_on_load(1);

            image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            
            width = image.Width;
            height = image.Height;
      
            
           
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            ApplyTextureFilter(filter);
        }

        public void SetTextureFilter(TextureFilter filter)
        {
            this.filter = filter;
            LoadTexture();
        }


        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

        public static void ApplyTextureFilter(TextureFilter textureFilter)
        {

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            switch (textureFilter)
            {
                case TextureFilter.Nearest:

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    break;
                case TextureFilter.Linear:

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    break;
                case TextureFilter.BilinearMipmap:

                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    break;
                case TextureFilter.TrilinearMipMap:

                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    break;
                default:
                    throw new NotImplementedException("Texture filter not implemented.");
            }
        }

    }
    
    public enum TextureFilter
    {
        Nearest,
        Linear,
        BilinearMipmap,
        TrilinearMipMap
    }


}
