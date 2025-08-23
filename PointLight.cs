using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class PointLight : Light
    {
        
        public Vector3 lightColor;


        public PointLight(Vector3 lightColor) : base()
        {
            this.lightColor = lightColor;
        }



    }
}
