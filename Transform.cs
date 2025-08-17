

using OpenTK.Mathematics;


namespace LearnOpenTK
{
    internal class Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public Matrix4 GetMatrixTransform()
        {
            var translationMatrix = Matrix4.CreateTranslation(position);

            var scaleMatrix = Matrix4.CreateScale(this.scale);

            var rotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            var rotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            var rotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            var rotationMatrix = rotationZ * rotationY * rotationX;

            return translationMatrix * rotationMatrix * scaleMatrix;
        }
    }
}
