
using Assimp;


namespace LearnOpenTK
{

    public static class ModelLoader
    {

        public static Mesh LoadModel(string path)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<int> indices = new List<int>();

            var importer = new AssimpContext();
            Assimp.Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals | PostProcessSteps.FlipUVs | PostProcessSteps.CalculateTangentSpace);

            foreach (var mesh in scene.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                {
                    var vert = new Vertex();

                    var vertex = mesh.Vertices[i];
                    var normal = mesh.Normals[i];
                    var uv = mesh.TextureCoordinateChannels[0].Count > 0 ? mesh.TextureCoordinateChannels[0][i] : new Vector3D();
                    var tangent = mesh.Tangents[i];
                    var bitangent = mesh.BiTangents[i];

                    vert.position = new OpenTK.Mathematics.Vector3(vertex.X, vertex.Y, vertex.Z);
                    vert.normal = new OpenTK.Mathematics.Vector3(normal.X, normal.Y, normal.Z);
                    vert.uv = new OpenTK.Mathematics.Vector2(uv.X, uv.Y);
                    vert.tangent = new OpenTK.Mathematics.Vector3(tangent.X, tangent.Y, tangent.Z);
                    vert.binormal = new OpenTK.Mathematics.Vector3(bitangent.X, bitangent.Y, bitangent.Z);

                    vertices.Add(vert);
                }


                foreach (var face in mesh.Faces)
                {
                    indices.AddRange(face.Indices.Select(i => (int)i));

                }

            }

            return new Mesh(vertices, indices.ToArray());
        }

    }


    
}
