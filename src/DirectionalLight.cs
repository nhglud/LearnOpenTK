using OpenTK.Mathematics;


namespace LearnOpenTK
{
    public class DirectionalLight : Light
    {
        public Vector3 direction;

        public DirectionalLight(Vector3 lightColor, Vector3 direction) : base()
        {
            this.lightColor = lightColor;
            this.direction = direction;
        }



    }
}
