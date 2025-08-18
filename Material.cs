using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class Material
    {
        Shader shader;

        Texture diffuse;
        Texture specular;

        public Material()
        {

        }

        public void Use()
        {
            shader.Use();

        }
    }
}
