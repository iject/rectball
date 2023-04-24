using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rectball
{
    public class Animator
    {
        private Circle c;
        private Rect r;
        private Thread? t = null;
        private Random random = new Random();
        public bool IsAlive => t == null || t.IsAlive;

        public Size ContainerSize { get; set; }
        public Circle C
        {
            get { return c; }
            set { c = value; }
        }


        public Animator(Size containerSize, int x, int y, Color color, int rect_num)
        {
            ContainerSize = containerSize;
            this.c = new Circle(x, y, 40, color, rect_num);
        }

        public void Start()
        {
            Random rnd = new Random();
            t = new Thread(() =>
            {
                int sign = rnd.Next(0, 2);
                while (c.Dx == 0 && c.Dy == 0)
                {
                    c.Dx = rnd.Next(-5, 6);
                    if (sign == 0) { sign = -1; }
                    c.Dy = sign * Convert.ToInt32(Math.Sqrt(25 - c.Dx * c.Dx));
                }
                while (true)
                {
                    Thread.Sleep(30);
                    c.Move();
                    wall_check();
                }


                /*while (c.X + c.Diam < ContainerSize.Width)
                {
                    Thread.Sleep(30);
                    c.Move(1, 0);
                }*/
            });
            t.IsBackground = true;
            t.Start();
        }

        public void wall_check()
        {
            if (c.X + c.Diam >= ContainerSize.Width || c.X <= 0)
            {
                c.Dx = -c.Dx;
            }
            if (c.Y + c.Diam >= ContainerSize.Height || c.Y <= 0)
            {
                c.Dy = -c.Dy;
            }
        }

        public void PaintCircle(Graphics g)
        {
            c.Paint(g);
        }

        public void PaintRect(Graphics g)
        {
            r.Paint(g);
        }
    }
}
