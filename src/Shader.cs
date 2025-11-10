
using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public class Shader : IDisposable
    {
        private int Handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            SetUpShader(vertexPath, fragmentPath);
            Use();

        }

        public Shader(string vertexPath, string geometryPath, string fragmentPath)
        {
            SetUpShader(vertexPath, geometryPath, fragmentPath);
            Use();
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        private void SetUpShader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource = File.ReadAllText(vertexPath);
            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            int VertexShader = CreateShader(VertexShaderSource, ShaderType.VertexShader);
            int FragmentShader = CreateShader(FragmentShaderSource, ShaderType.FragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            LinkProgram();

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        private void SetUpShader(string vertexPath, string geometryPath, string fragmentPath)
        {
            string VertexShaderSource = File.ReadAllText(vertexPath);
            string GeometryShaderSource = File.ReadAllText(geometryPath);
            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            int VertexShader = CreateShader(VertexShaderSource, ShaderType.VertexShader);
            int GeometryShader = CreateShader(GeometryShaderSource, ShaderType.GeometryShader);
            int FragmentShader = CreateShader(FragmentShaderSource, ShaderType.FragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, GeometryShader);
            GL.AttachShader(Handle, FragmentShader);

            LinkProgram();

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DetachShader(Handle, GeometryShader);

            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(GeometryShader);
            GL.DeleteShader(VertexShader);
        }


        private int CreateShader(string shaderSource, ShaderType shaderType)
        {
            int shader = GL.CreateShader(shaderType);

            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int succes);
            if (succes == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine(infoLog);
            }

            return shader;
        } 

        private void LinkProgram()
        {
            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }


            int blockIndex = GL.GetUniformBlockIndex(Handle, "CameraData");
            GL.UniformBlockBinding(Handle, blockIndex, 0);
        }


        public void Use()
        {
            GL.UseProgram(Handle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SetInt(string name , int value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        public void SetBool(string name, bool value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value ? 1 : 0);
        }

        public void SetFloat(string name, float value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        public void SetVector2(string name, Vector2 vector)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform2(location, vector);
        }


        public void SetVector3(string name, Vector3 vector)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform3(location, vector);
        }


        public void SetColor4(string name, Color4 color)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform4(location, color);
        }



        public void SetMat4(string name, Matrix4 matrix)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }

    }
        
}
