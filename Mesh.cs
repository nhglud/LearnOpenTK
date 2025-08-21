

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

            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);



            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);



            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

    }
}
