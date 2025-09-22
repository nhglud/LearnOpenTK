
using Assimp;


namespace LearnOpenTK
{

    public static class ModelLoader
    {

        public static Mesh LoadModel(string path)
        {
            List<float> vertices = new List<float>();
            List<int> indices = new List<int>();

            var importer = new AssimpContext();
            Assimp.Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals | PostProcessSteps.FlipUVs | PostProcessSteps.CalculateTangentSpace);

            foreach (var mesh in scene.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                {

                    var v = mesh.Vertices[i];
                    var n = mesh.Normals[i];
                    var uv = mesh.TextureCoordinateChannels[0].Count > 0 ? mesh.TextureCoordinateChannels[0][i] : new Vector3D();
                    var tangent = mesh.Tangents[i];
                    var bitangent = mesh.BiTangents[i];

                    vertices.Add(v.X);
                    vertices.Add(v.Y);
                    vertices.Add(v.Z);

                    vertices.Add(n.X);
                    vertices.Add(n.Y);
                    vertices.Add(n.Z);

                    vertices.Add(uv.X);
                    vertices.Add(uv.Y);

                    vertices.Add(tangent.X);
                    vertices.Add(tangent.Y);
                    vertices.Add(tangent.Z);

                    vertices.Add(bitangent.X);
                    vertices.Add(bitangent.Y);
                    vertices.Add(bitangent.Z);

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
