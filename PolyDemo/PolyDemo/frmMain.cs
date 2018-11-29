using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using GDIDrawer;


namespace PolyDemo
{
    public partial class frmMain : Form
    {
        private static CDrawer canvas = null;
        private static Random random = null;
        private Thread Animator = null;
        private List<Shape> Shapes = null;
        private List<Shape> newShapes = null;
        private const int canvWidth = 800;
        private const int canvHeight = 600;
        private const int delay = 50; //ms of sleep in animate cycle

        static frmMain()
        {
            canvas = new CDrawer(canvWidth, canvHeight);
            random = new Random();
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //The set of Shapes that I will animate (empty at first)
            //My GUI thread will only add Shapes and never remove
            //or modify them, so thread safety should be good.
            //Otherwise, should probably add some locking and or
            //set up more sophisticated inter-thread communication.
            Shapes = new List<Shape>(10);

            //Start animation thread
            Animator = new Thread(new ParameterizedThreadStart(Animate));
            Animator.IsBackground = true;
            Animator.Start(Shapes);

            //set up my event handlers
            if (!(canvas is null))
            {
                canvas.MouseLeftClick += new GDIDrawerMouseEvent(HandleMouse);
                canvas.KeyboardEvent += new GDIDrawerKeyEvent(HandleKey);
            }
        }

        //NB: if button pushed again, we'll just add 10 more Shapes.
        private void btnGo_Click(object sender, EventArgs e)
        {
            //If timer is running, it could goof with my list.
            tmrAddShapes.Enabled = false;

            //Make sure I got a canvas
            if(canvas is null || canvas.DrawerWindowSize.IsEmpty) canvas = new CDrawer(canvWidth, canvHeight);

            //Set my Shapes to bounce on that size canvas
            Shape.SetCanvasProperties(canvas);

            //Create my list of 10 Shapes, obeying the rules for 
            //resolving conflicts
            newShapes = new List<Shape>(10);
            while(newShapes.Count < 10)
            {
                Shape shape = random.Next(2)== 0 ? new Circle() : new Ellipse();
                if (!newShapes.Contains(shape)) newShapes.Add(shape);
            }
            //Sort them
            newShapes.Sort();

            //Start adding them to the animation by letting my timer go.
            tmrAddShapes.Enabled = true;
        }

        private void Animate(object ListOShapes)
        {
            //Quick and dirty erase everything (could get fancy and overwrite
            //existing Shapes with black just before move and redraw, but that's
            //probably premature optimization).
            if(!(ListOShapes is List<Shape> Shapes)) return; //What is this thing?

            while (true)//Forevs, unless parent thread goes away.
            {
                if(cbErase.Checked)canvas.Clear();
                //My kingdom for a foreach with mutable list elements
                lock (Shapes)
                {//Added because my event handlers could crossthread
                    for (int i = 0; i < Shapes.Count; ++i)
                    {
                        Shape s = Shapes[i];
                        //Move first.  That makes sure everything is on the canvas.
                        s.Move();//This changes b! no other threads should be changing b, or I need to do more.
                        s.Render(canvas);
                    }
                }
                //Show the pretty thingees
                canvas.Render();
                Thread.Sleep(delay);
            }
        }

        private void tmrAddShapes_Tick(object sender, EventArgs e)
        {
            if (newShapes.Count > 0)
            {
                //What clown didn't have RemoveAt return the item removed?
                Shape shape = newShapes[0];
                newShapes.RemoveAt(0);
                Shapes.Add(shape);
            }
            else //list empty.  no real point in ticking.
                tmrAddShapes.Enabled = false;
        }

        //Event Handler for Mouse things
        private void HandleMouse(System.Drawing.Point pos, GDIDrawer.CDrawer drawer)
        {
            //add a Shape when and where you click
            lock(Shapes) //The other thread might be using it
            {
                Shape s = new Ellipse();
                s.Location = new PointF(pos.X, pos.Y);
                Shapes.Add(s);
            }
        }

        //Event Handler for Keyboard things 
        private void HandleKey(bool bIsDown, Keys keycode, GDIDrawer.CDrawer drawer)
        {
            // Don't do anything until they release the key (the event fires multiples).
            if (bIsDown) return;
            switch (keycode)
            {
                case Keys.D:
                    //Delete a Shape
                    lock (Shapes)
                        if (Shapes.Count > 0) Shapes.RemoveAt(0);
                    break;
                case Keys.A:
                    //Add a Shape
                    lock (Shapes)
                        Shapes.Add(new Circle());
                    break;
            }
        }

        private void btnRenderable_Click(object sender, EventArgs e)
        {
            List<IRenderable> Renderables = new List<IRenderable>();
            Renderables.Add(new Ellipse());
            Renderables.Add(new Circle());
            Renderables.Add(new RenderThing());
            foreach (IRenderable R in Renderables)
                R.Render(canvas);
        }
        
    }
}
