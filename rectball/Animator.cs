using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rectball
{
    public class Animator
    {
        private Graphics _mainG;

        //private Random r = new Random((int)DateTime.Now.Ticks);

        public Graphics MainGraphics
        {
            get => _mainG;
            set
            {
                _mainG = value;
            }
        }

        public Animator(Graphics graphics)
        {
            MainGraphics = graphics;
        }

        /*public void Animate()
        {
            var r = new Rect() 
        }*/

    }
}
