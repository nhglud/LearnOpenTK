using OpenTK.Mathematics;

namespace LearnOpenTK
{
    public class LightSource : Component
    {
        
        public Vector3 lightColor;


        public LightSource(Vector3 lightColor) : base()
        {
            this.lightColor = lightColor;
        }



    }
}
