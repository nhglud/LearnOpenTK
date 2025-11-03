
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

        private List<Vector2> uvs;

        private Mesh plane;
        private Texture heightmap;


        public TerrainGenerator() 
        {
            vertices = new List<float>();
            indices = new List<int>();
            uvs = new List<Vector2>();

        }

        public TerrainGenerator(Texture heightmap)
        {
            vertices = new List<float>();
            indices = new List<int>();
            uvs = new List<Vector2>();
            this.heightmap = heightmap;
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
      
            //UpdateHeight(heightmap, 14, 100);
      
            plane = new Mesh(vertices.ToArray(), indices.ToArray());
        }

        public void UpdateHeight(Texture heightmap, int vertexSize, float height)
        {
            int vertexCount = vertices.Count / vertexSize;

            Console.WriteLine(heightmap.image.Data.Length);
            Console.WriteLine(vertexCount);

            for (int i = 0; i < vertexCount; i++)
            {
                float u = vertices[i * vertexSize + 6];
                float v = vertices[i * vertexSize + 7];

                int px = (int)(u * (heightmap.width - 1));
                int py = (int)(v * (heightmap.height - 1));
                px = Math.Clamp(px, 0, heightmap.width - 1);
                py = Math.Clamp(py, 0, heightmap.height - 1);

                int pixelIndex = (py * heightmap.width + px) * 4;

                byte r = heightmap.image.Data[pixelIndex]; // red channel as height
                Console.WriteLine(r);
                float h = r / 255f; // normalized 0–1
                Console.WriteLine(h);
                // Apply displacement on Y
                int yIndex = i * vertexSize + 1; // Y is the second component in position (x,y,z)
                vertices[yIndex] += h * height;
            }



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

            uvs.Add(uv);

        }



    }
}
