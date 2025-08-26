using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class SpotLight : Light
    {

        public Vector3 direction;
        public float innerRadius;
        public float outerRadius;

        public SpotLight(Color4 lightColor, float innerRadius, float outerRadius, Vector3 direction)
        {
            this.lightColor = lightColor;
            this.innerRadius = innerRadius;
            this.outerRadius = outerRadius;
            this.direction = direction;
        }

    }
}
