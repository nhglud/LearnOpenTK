

using OpenTK.Graphics.OpenGL4;


namespace LearnOpenTK
{
    public class Mesh : Component
    {
        private float[] vertices;
        private int[] indices;
        private int VertexArrayObject;

        public Mesh(float[] vertices, int[] indices) : base()
        { 
            this.vertices = vertices;
            this.indices = indices;

            SetUpMesh();
        }   

        private void SetUpMesh()
        {
            VertexArrayObject = GL.GenVertexArray();
            int VertexBufferObject = GL.GenBuffer();
            int ElementBufferObject = GL.GenBuffer();

            // Bind buffers
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // Set vertex attributes

            // position + normal + uv + tangent + binormal
            int size = 3 + 3 + 2 + 3 + 3;
            size *= sizeof(float);


            // Positions
            int offset = 0;
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, size, offset);
            GL.EnableVertexAttribArray(0);

            // Normals
            offset += 3 * sizeof(float);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, size, offset);
            GL.EnableVertexAttribArray(1);

            // UVs
            offset += 3 * sizeof(float);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, size, offset);
            GL.EnableVertexAttribArray(2);

            //Tangents
            offset += 2 * sizeof(float);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, size, offset);
            GL.EnableVertexAttribArray(3);

            // Binormals
            offset += 3 * sizeof(float);
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, size, offset);
            GL.EnableVertexAttribArray(4);



            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

    }
}
