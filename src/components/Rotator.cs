using OpenTK.Windowing.Common;

namespace LearnOpenTK.src.components
{
    public class Rotator : Component, IUpdateable
    {

        float rotationSpeed = 10.0f;
        public Rotator()
        {


        }

        public void Update(FrameEventArgs e)
        {
            transform.rotation.Y += rotationSpeed * (float)e.Time;
            transform.rotation.Z += rotationSpeed * (float)e.Time;


            transform.rotation.Y = transform.rotation.Y % 360;
            transform.rotation.Z = transform.rotation.Z % 360;

        }
    }
}
