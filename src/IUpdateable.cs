using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK.src
{
    public interface IUpdateable
    {

        void Update(FrameEventArgs e);
    }
}
