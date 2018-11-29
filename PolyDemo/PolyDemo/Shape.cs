using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;

public interface IMoveable
{
    PointF Move();
}

public interface IRenderable
{
    void Render(GDIDrawer.CDrawer canvas);
}

namespace PolyDemo
{
    internal abstract class Shape : IComparable , IRenderable , IMoveable
    {
        #region Static Properties
        public static int CanvasWidth { set; get; }
        public static int CanvasHeight { set; get; }
        public static int MaxSpeed { set; get; }
        protected static Random Random { get; }
        #endregion

        #region Properties
        public PointF Location { set; get; }
        public PointF Velocity { set; get; }
        public Color Color { set; get; }
        #endregion

        #region Constructors
        static Shape()
        {
            //Bases on scaled width, height, etc.
            CanvasWidth = 800;
            CanvasHeight = 600;
            MaxSpeed = 5;
            Random = new Random();
        }

        public Shape() //Default ctor Randomificates
        {
            Location = new PointF(Random.Next(CanvasWidth),
                Random.Next(CanvasHeight));
            Velocity = new PointF((float)Random.NextDouble() * 2 * MaxSpeed - MaxSpeed, 
                (float)Random.NextDouble() * 2 * MaxSpeed - MaxSpeed);
            Color = RandColor.GetColor();
        }

        public Shape(bool b):this() //Overload for random color
        {
            //Could do this with enums and case statements,
            //but anonymous initializer lists are fun!
            Color = new List<Color> { Color.Red, Color.Blue,
                Color.Green, Color.Yellow, Color.Purple}[Random.Next(5)];
        }
        #endregion

        #region Methods
        public static void SetCanvasProperties(CDrawer canvas)
        {
            //Reset static canvas size based on the sample I just got
            CanvasWidth = canvas.ScaledWidth;
            CanvasHeight = canvas.ScaledHeight;
        }

        #endregion

        #region Overrides
        //Override base class methods
        public override bool Equals(object obj)
        {
            if (!(obj is Shape that))
                return false;
            return that.Color == this.Color
                && that.Location == this.Location
                && that.Velocity == this.Velocity;
        }

        public override int GetHashCode()
        {
            //Fancy Tuple trick
            return new Tuple<Color, PointF, PointF>(
                Color,Location,Velocity).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Color} Shape at ({Location.X},{Location.Y})";
        }
        #endregion

        #region Interfaces
        //Required for IComparable
        //Sort by color.
        public virtual int CompareTo(object obj)
        {
            if (!(obj is Shape that))
                throw new ArgumentException(
                    "Invalid object passed to Shape.CompareTo()");
            return this.Color.ToArgb() - that.Color.ToArgb();
        }

        //IRenderable: Display the shape on the provided canvas
        public abstract void Render(GDIDrawer.CDrawer canvas);

        //IMoveable: Move the object by a point worth of distance
        //Reset position+=velocity, corrected with bounce
        //Returns new position
        //TODO:  Eww.  PointFs aren't editable.  Redo with 
        //       a tuple or something.
        //NB! Assumes 'Location' is CENTER of shape.
        //Remember to paint with Centered methods
        public virtual PointF Move()
        {
            Location = new PointF(Location.X + Velocity.X,
                Location.Y + Velocity.Y);

            //Check if now out of bounds, correct
            if (Location.X < 0) //Left edge
            {
                Location = new PointF(0, Location.Y);
                Velocity = new PointF(-Velocity.X, Velocity.Y); //Boing!
            }
            if (Location.X > CanvasWidth) //Right edge
            {
                Location = new PointF(CanvasWidth, Location.Y);
                Velocity = new PointF(-Velocity.X, Velocity.Y); //Boing!
            }

            //Check if now out of bounds, correct
            if (Location.Y < 0) //Top edge
            {
                Location = new PointF(Location.X, 0);
                Velocity = new PointF(Velocity.X, -Velocity.Y); //Boing!
            }
            if (Location.Y > CanvasHeight) //Bottom edge
            {
                Location = new PointF(Location.X, CanvasHeight);
                Velocity = new PointF(Velocity.X, -Velocity.Y); //Boing!
            }

            return Location;
        }
        #endregion
    }
}
