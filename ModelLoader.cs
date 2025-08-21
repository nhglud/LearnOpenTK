using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using OpenTK.Mathematics;

namespace LearnOpenTK
{


    public static class ModelLoader
    {

        public static Mesh LoadModel(string path)
        {
            List<float> vertices = new List<float>();
            List<int> indices = new List<int>();

            var importer = new AssimpContext();
            Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals | PostProcessSteps.FlipUVs);

            foreach (var mesh in scene.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                {

                    var v = mesh.Vertices[i];
                    var n = mesh.Normals[i];
                    var uv = mesh.TextureCoordinateChannels[0].Count > 0 ? mesh.TextureCoordinateChannels[0][i] : new Vector3D();

                    vertices.Add(v.X);
                    vertices.Add(v.Y);
                    vertices.Add(v.Z);

                    vertices.Add(n.X);
                    vertices.Add(n.Y);
                    vertices.Add(n.Z);


                    vertices.Add(uv.X);
                    vertices.Add(uv.Y);

                }

          
                foreach (var face in mesh.Faces)
                {
                    indices.AddRange(face.Indices.Select(i => (int)i));
                }

            }

            return new Mesh(vertices.ToArray(), indices.ToArray());
        }

    }


    
}
