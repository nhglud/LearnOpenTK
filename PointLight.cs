using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class PointLight : Light
    {
        
        public PointLight(Vector3 lightColor) : base()
        {
            this.lightColor = lightColor;
        }


    }
}
