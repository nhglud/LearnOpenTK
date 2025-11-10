

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;


namespace LearnOpenTK
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 position;
        public Vector3 normal;
        public Vector2 uv;
        public Vector3 tangent;
        public Vector3 binormal;
    }


    public class Mesh : Component
    {
        private List<Vertex> vertices = new List<Vertex>();
        private int[] indices;
        private int VertexArrayObject;

        public Mesh(List<Vertex> vertices, int[] indices) : base()
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

            int size = Marshal.SizeOf<Vertex>();

            // Bind buffers
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * size, vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // Set vertex attributes

            // Positions
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, size, Marshal.OffsetOf<Vertex>("position"));
            GL.EnableVertexAttribArray(0);

            // Normals
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, size, Marshal.OffsetOf<Vertex>("normal"));
            GL.EnableVertexAttribArray(1);

            // UVs
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, size, Marshal.OffsetOf<Vertex>("uv"));
            GL.EnableVertexAttribArray(2);

            //Tangents
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, size, Marshal.OffsetOf<Vertex>("tangent"));
            GL.EnableVertexAttribArray(3);

            // Binormals
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, size, Marshal.OffsetOf<Vertex>("binormal"));
            GL.EnableVertexAttribArray(4);


            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawElements(PrimitiveType.Lines, indices.Length, DrawElementsType.UnsignedInt, 0);


        }

    }
}
