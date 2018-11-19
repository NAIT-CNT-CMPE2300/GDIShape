using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDemo
{
    internal class Circle:Ellipse
    {
        //A circle is just an elipse with equal diameters.
        public float Diameter { get => XDiameter;
        set { XDiameter = YDiameter = value; }
        }
        public Circle(float diam) : base() => Diameter = diam;
        public Circle() : this(Random.Next(10, 50)) { }
    }
}
