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
        //List<float> vertices;
        //List<uint> indices;


        //public ModelLoader(string path)
        //{
        //    LoadModel(path);
        //}

        //public List<float> GetVertices()
        //{
        //    return vertices;
        //}


        //public List<uint> GetIndices()
        //{
        //    return indices;
        //}


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


    //public class ModelLoader
    //{

    //    private List<float> model;


    //    public ModelLoader(string path)
    //    {
    //       model = LoadModel(path);
    //    }

    //    public List<float> GetModel()
    //    {
    //        return model;
    //    }

    //    public List<float> LoadModel(string path)
    //    {
    //        List<float> vertexData = new List<float>();


    //        List<Vector3> positions = new List<Vector3>();
    //        List<Vector3> normals = new List<Vector3>();
    //        List<Vector2> uvs = new List<Vector2>();
    //        List<int> indices = new List<int>();

    //        var lines = File.ReadLines(path);

    //        foreach (var line in lines)
    //        {

    //            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //            if (parts.Length == 0) continue;


    //            switch(parts[0])
    //            {
    //                case "v":

    //                    positions.Add(new Vector3(
    //                        float.Parse(parts[1]), 
    //                        float.Parse(parts[2]), 
    //                        float.Parse(parts[3])
    //                    ));
                        
    //                    break;


    //                case "vn":

    //                    normals.Add(new Vector3(
    //                        float.Parse(parts[1]),
    //                        float.Parse(parts[2]),
    //                        float.Parse(parts[3])
    //                    ));

    //                    break;

    //                case "vt":

    //                    uvs.Add(new Vector2(
    //                        float.Parse(parts[1]),
    //                        float.Parse(parts[2])
    //                    ));

    //                    break;

    //                case "f":
    //                    for (int i = 1; i < parts.Length; i++)
    //                    {




    //                        var indicesParts = parts[i].Split('/');
    //                        indices.Add(int.Parse(indicesParts[0]) - 1); // OBJ indices are 1-based
                            
                        
                        
                        
    //                    }

    //                    break;

    //            }


    //        }


         

    //        Console.WriteLine(positions);
    //        Console.WriteLine(normals.Count);
    //        Console.WriteLine(uvs.Count);
    //        Console.WriteLine(indices.Count);


    //        return vertexData;
    //    }





    
}
