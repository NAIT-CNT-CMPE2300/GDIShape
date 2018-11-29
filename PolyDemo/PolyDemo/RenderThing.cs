using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;

namespace PolyDemo
{
    public class RenderThing : IRenderable
    {
        public void Render(CDrawer canvas)
        {
            canvas.AddPolygon(0,0,50,6,0,GDIDrawer.RandColor.GetColor());
        }
    }
}
