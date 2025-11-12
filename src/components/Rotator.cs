using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK.src.components
{
    public class Rotator : Component, IUpdateable
    {

        float rotationSpeed = 10.0f;
        public Rotator() { 
        
            
        }

        public void Update(FrameEventArgs e)
        {
            transform.rotation.Y += rotationSpeed * (float)e.Time;
            transform.rotation.Z += rotationSpeed * (float)e.Time;

        }
    }
}
