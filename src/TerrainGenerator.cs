
using OpenTK.Mathematics;

namespace LearnOpenTK.src
{
    public  class TerrainGenerator
    {
        private struct Vertex
        {
            public Vector3 position;
            public Vector3 normal;
            public Vector2 uv;
            public Vector3 tangent;
            public Vector3 binormal;

        }

        private List<float> vertices;
        private List<int> indices;

        private Mesh plane;


        public TerrainGenerator() 
        {
            
        }


        public Mesh GetMesh()
        {
            return plane;
        }

        public void CreatePlane(float width, float depth, int divX, int divZ, float u, float v)
        {
            
            vertices = new List<float>();
            indices = new List<int>();

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



            plane = new Mesh(vertices.ToArray(), indices.ToArray());
        }

        public void UpdateHeight(Texture heightmap, int vertexSize, float height)
        {

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

            vertices.Add(position.X);
            vertices.Add(position.Y);
            vertices.Add(position.Z);

            vertices.Add(normal.X);
            vertices.Add(normal.Y);
            vertices.Add(normal.Z);

            vertices.Add(uv.X);
            vertices.Add(uv.Y);

            vertices.Add(tangent.X);
            vertices.Add(tangent.Y);
            vertices.Add(tangent.Z);

            vertices.Add(binormal.X);
            vertices.Add(binormal.Y);
            vertices.Add(binormal.Z);

            indices.Add(indices.Count);

        }








    }
}
