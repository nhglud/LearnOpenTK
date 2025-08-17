using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public static class AssetManager
    {

        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();
        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();

        public static void LoadAssets()
        {
            float[] vertices = {
                  -0.5f, -0.5f, 0.0f, //Bottom-left vertex
                   0.5f, -0.5f, 0.0f, //Bottom-right vertex
                   0.0f,  0.5f, 0.0f  //Top vertex
            };

            int[] indices = { 0, 1, 2 };

            AddMesh("triangle", new Mesh(vertices, indices));

            float[] quadVertices = {
               0.5f,  0.5f, 0.0f,  // top right
               0.5f, -0.5f, 0.0f,  // bottom right
              -0.5f, -0.5f, 0.0f,  // bottom left
              -0.5f,  0.5f, 0.0f   // top left
            };

            int[] quadIndices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };

            AddMesh("quad", new Mesh(quadVertices, quadIndices));


            Shader shader = new Shader("C:\\programming\\LearnOpenTK\\basic_shader.vert", "C:\\programming\\LearnOpenTK\\basic_shader.frag");

            AddShader("basic", shader);
        }

        public static void AddMesh(string name, Mesh mesh)
        {
            meshes.Add(name, mesh);
        }

        public static void AddShader(string name, Shader shader)
        {
            shaders.Add(name, shader);
        }

        public static Mesh GetMesh(string name)
        {
            return meshes[name];
        }

        public static Shader GetShader(string name)
        {
            return shaders[name];
        }
    }
}
