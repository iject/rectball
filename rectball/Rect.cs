using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rectball
{
    public class Rect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }



        public Rect(int x, int y, int width, int height, Color color) // создание прямоугольника с цветом пользователя
        {
            X = x - width / 2;
            Y = y - height / 2;
            Width = width;
            Height = height;
            Color = color;
        }

        Random rand = new Random();

        public Rect(int x, int y, int width, int height) // создание прямоугольника с рандомным цветом
        {
            X = x - width / 2;
            Y = y - height / 2;
            Width = width;
            Height = height;
            Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }



        public void Paint(Graphics g)
        {
            var p = new SolidBrush(Color);
            g.FillRectangle(p, new Rectangle(X, Y, Width, Height));
        }
    }
}
