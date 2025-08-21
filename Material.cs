using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class Material
    {
        private Shader shader;

        private Texture diffuse;
        private Texture specular;
        private float shininess;


        public Material(Shader shader, Texture diffuse, Texture specular, float shininess)
        {
            this.shader = shader;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;

        }

        public void Use()
        {
            shader.Use();
            shader.SetFloat("material.shininess", shininess);
            diffuse.Use();
            specular.Use();
        }


    }
}
