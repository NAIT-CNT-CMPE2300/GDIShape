using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;

namespace PolyDemo
{
    class Ellipse : Shape
    {
        #region CTOR
        public Ellipse(float Xdia, float Ydia) : base(true)
        {
            XDiameter = Xdia;
            YDiameter = Ydia;
        }
        public Ellipse() : this(Random.Next(10, 50),
            Random.Next(10, 50)) { }
        #endregion

        #region PROP
        public float XDiameter { get; set; }
        public float YDiameter { get; set; }
        #endregion

        #region OVER
        public override void Render(CDrawer canvas)
        { 
            canvas.AddCenteredEllipse((int)Location.X, (int)Location.Y,
               (int)XDiameter, (int)YDiameter, this.Color);
        }

        public override PointF Move()
        {
            Location = new PointF(Location.X + Velocity.X,
               Location.Y + Velocity.Y);

            //Check if now out of bounds, correct
            if (Location.X - XDiameter / 2 < 0) //Left edge
            {
                Location = new PointF(XDiameter / 2, Location.Y);
                Velocity = new PointF(-Velocity.X, Velocity.Y); //Boing!
            }
            if (Location.X + XDiameter / 2 > CanvasWidth) //Right edge
            {
                Location = new PointF(CanvasWidth - XDiameter / 2, Location.Y);
                Velocity = new PointF(-Velocity.X, Velocity.Y); //Boing!
            }

            //Check if now out of bounds, correct
            if (Location.Y - YDiameter / 2 < 0) //Top edge
            {
                Location = new PointF(Location.X, YDiameter / 2);
                Velocity = new PointF(Velocity.X, -Velocity.Y); //Boing!
            }
            if (Location.Y + YDiameter / 2 > CanvasHeight) //Bottom edge
            {
                Location = new PointF(Location.X, CanvasHeight - YDiameter / 2);
                Velocity = new PointF(Velocity.X, -Velocity.Y); //Boing!
            }

            return Location;
        }

        //Leverage base compareto (which is color)
        public override int CompareTo(object obj)
        {
            if (!(obj is Ellipse that))
                throw new ArgumentException(
                    "Attempt to compare non-Circle");
            return base.CompareTo(that) != 0 ? //Color
                base.CompareTo(that) :
                XDiameter.CompareTo(that.XDiameter) != 0 ? //XAxis
               XDiameter.CompareTo(that.XDiameter) : 
               YDiameter.CompareTo(that.YDiameter); //YAxis
        }
        #endregion
    }
}
