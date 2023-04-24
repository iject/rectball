using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rectball
{
    public class Circle
    {
        private Random r = new();
        private int diam;
        private int x, y;
        private int dx = 0, dy = 0;

        public int X => x;
        public int Y => y;
        public int Dx
        {
            get { return dx; }
            set { dx = value; }
        }
        public int Dy
        {
            get { return dy; }
            set { dy = value; }
        }
        public int Diam => diam;
        public Color Color { get; set; }
        public int Rect_num { get; set; }// идентификация кругляша по квадрату-родителю


        public Circle(int x, int y, int diam, Color color, int rect_num)
        {
            this.x = x - diam / 2;
            this.y = y - diam / 2;
            this.diam = diam;
            this.Color = color;
            Rect_num = rect_num;
        }

        public Circle(int x, int y, int diam)
        {
            this.x = x - diam/2;
            this.y = y - diam / 2;
            this.diam = diam;
            this.Color = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
        }

        public void Move()
        {
            x += Dx;
            y += Dy;
        }

        public void Paint(Graphics g)
        {
            var brush = new SolidBrush(Color);
            g.FillEllipse(brush, X, Y, Diam, Diam);
        }
    }
}
