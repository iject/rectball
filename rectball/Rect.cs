using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace rectball
{
    public class Rect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public int Score { get; set; }
        public int ID { get; set; }

        //private Circle c;
        Random rand = new Random();
        private int red;
        private int green;
        private int blue;


        public Rect(int x, int y, int width, int height) // создание прямоугольника с рандомным цветом
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            red = rand.Next(0, 255);
            green = rand.Next(0, 255);
            blue = rand.Next(0, 255);
            Color = Color.FromArgb(red, green, blue);
        }


        public void Paint(Graphics g)
        {
            var p = new SolidBrush(Color);
            RectangleF drawRect = new RectangleF(X - Width / 2, Y - Height / 2, Width, Height);
            RectangleF drawRect_id = new RectangleF(X - Width / 6, Y - Height / 4, Width, Height);
            //RectangleF ballRect = new RectangleF(X - Width / 4, Y - Height / 4, Width / 2, Height / 2);

            Font drawFont = new Font("Arial", Width/2+1);
            Font drawFont_id = new Font("Arial", Width/6+1);
            SolidBrush drawBrush;
            if (1 - (0.299 * red + 0.587 * green + 0.114 * blue) / 255 < 0.5)
            {
                drawBrush = new SolidBrush(Color.Black);
            }
            else
            {
                drawBrush = new SolidBrush(Color.White);
            }
            StringFormat drawFormat = new StringFormat(StringFormatFlags.NoClip);
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;


            g.FillRectangle(p, drawRect);
            //g.FillEllipse(drawBrush, ballRect);

            // Отрисовка score
            g.DrawString(Score.ToString(), drawFont, drawBrush, drawRect, drawFormat);
            //g.DrawString(ID.ToString(), drawFont_id, drawBrush, drawRect_id, drawFormat);
            
        }
    }
}
