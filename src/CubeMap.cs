using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System.IO;

namespace LearnOpenTK.src
{
    public class CubeMap
    {
        private int handle;
        private List<string> facesPaths;
        
        public CubeMap(List<string> facesPaths) 
        { 
            this.facesPaths = facesPaths;
            loadTextures();
        }    

        private void loadTextures()
        {
            handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, handle);

            for (int i = 0; i < facesPaths.Count; i++)
            {
                var face = facesPaths[i];
                StbImage.stbi_set_flip_vertically_on_load(0);
                ImageResult image = ImageResult.FromStream(File.OpenRead(face), ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(
                    TextureTarget.TextureCubeMapPositiveX + i,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width, image.Height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    image.Data);
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.TextureCubeMap, handle);
        }

    }
}
