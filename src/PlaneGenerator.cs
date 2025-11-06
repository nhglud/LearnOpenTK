
using OpenTK.Mathematics;

namespace LearnOpenTK.src
{
    public  class PlaneGenerator
    {
       
        private List<Vertex> vertices;
        private List<int> indices;

        private List<Vector2> uvs;

        private Mesh plane;


        public PlaneGenerator() 
        {
            vertices = new List<Vertex>();
            indices = new List<int>();
            uvs = new List<Vector2>();

        }




        public Mesh GetMesh()
        {
            return plane;
        }

        public void CreatePlane(float width, float depth, int divX, int divZ, float u, float v)
        {

            float w = width * 0.5f;
            float d = depth * 0.5f;
            float sw = width / (divX + 1);
            float sd = depth / (divZ + 1);
            float tu = u / (divX + 1);
            float tv = v / (divZ + 1);


            Vector3 normal = Vector3.UnitY;
            Vector3 binormal = Vector3.UnitZ;
            Vector3 tangent = Vector3.UnitX;


            for (float z = 0; z < divZ; z++)
            {
                for (float x = 0; x < divX; x++)
                {
                    Vertex v1 = new Vertex();
                    Vertex v2 = new Vertex();
                    Vertex v3 = new Vertex();
                    Vertex v4 = new Vertex();

                    v1.position = new Vector3(-w + sw * (x + 1), 0, -d + sd * (z + 1));
                    v1.normal = normal;
                    v1.uv = new Vector2(-tu * (x + 1), tv * (z + 1));
                    v1.tangent = tangent;
                    v1.binormal = binormal;

                    v2.position = new Vector3(-w + sw * (x + 1), 0, -d + sd * (z + 0));
                    v2.normal = normal;
                    v2.uv = new Vector2(-tu * (x + 1), tv * (z + 0));
                    v2.tangent = tangent;
                    v2.binormal = binormal;

                    v3.position = new Vector3(-w + sw * (x + 0), 0, -d + sd * (z + 0));
                    v3.normal = normal;
                    v3.uv = new Vector2(-tu * (x + 0), tv * (z + 0));
                    v3.tangent = tangent;
                    v3.binormal = binormal;

                    v4.position = new Vector3(-w + sw * (x + 0), 0, -d + sd * (z + 1));
                    v4.normal = normal;
                    v4.uv = new Vector2(-tu * (x + 0), tv * (z + 1));
                    v4.tangent = tangent;
                    v4.binormal = binormal;

                    CreateQuad(v1, v2, v3, v4);
                }


            }
            
            plane = new Mesh(vertices, indices.ToArray());
        }

       

        private void CreateQuad(Vertex v1, Vertex v2 , Vertex v3, Vertex v4)
        {
            CreateTriangle(v1, v2, v3);
            CreateTriangle(v1, v3, v4);
        }


        private void CreateTriangle(Vertex v1, Vertex v2, Vertex v3)
        {
            CreateVertex(v1);
            CreateVertex(v2);
            CreateVertex(v3);
        }

        private void CreateVertex(Vertex v)
        {
            CreateVertex(v.position, v.normal, v.uv, v.tangent, v.binormal);
        }



        private void CreateVertex(Vector3 position, Vector3 normal, Vector2 uv, Vector3 tangent, Vector3 binormal)
        {
            var vertex = new Vertex();
            vertex.position = position;
            vertex.normal = normal;
            vertex.uv = uv;
            vertex.tangent = tangent;
            vertex.binormal = binormal;

            vertices.Add(vertex);

            indices.Add(indices.Count);

            uvs.Add(uv);

        }



    }
}
