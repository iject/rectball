using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace rectball
{
    public class Painter
    {
        private object locker = new();
        private List<Animator> animators = new();
        private List<Rect> rects = new();
        private Size containerSize;
        private Thread t;
        private Thread sqT;
        private Graphics mainGraphics;
        private BufferedGraphics bg;
        private bool isAlive;
        private DB DataBase;


        private volatile int objectsPainted = 0;
        public Thread PainterThread => t;
        public Graphics MainGraphics
        {
            get => mainGraphics;
            set
            {
                lock (locker)
                {
                    mainGraphics = value;
                    ContainerSize = mainGraphics.VisibleClipBounds.Size.ToSize();
                    bg = BufferedGraphicsManager.Current.Allocate(
                        mainGraphics, new Rectangle(new Point(0, 0), ContainerSize)
                    );
                    objectsPainted = 0;
                }
            }
        }

        public Size ContainerSize
        {
            get => containerSize;
            set
            {
                containerSize = value;
                foreach (var animator in animators)
                {
                    animator.ContainerSize = ContainerSize;
                }
            }
        }

        public Painter(Graphics mainGraphics, DB d)
        {
            MainGraphics = mainGraphics;
            DataBase = d;
        }


        public void AddRect(MouseEventArgs e)
        {
            Rect square = new Rect(e.X, e.Y, 40, 40);

            try
            {
                rects.Add(square);
                //square.ID = rects.Count();
                square.Paint(mainGraphics);
                DataBase.InsertNew(rects.Count(), square.Score);
            }
            catch (Exception exception) { }
            AddCircle(e, square.Color, rects.Count()); // передаем идентификатор rect'a
        }

        public void AddCircle(MouseEventArgs e, Color color, int rect_num)
        {
            sqT = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        var a = new Animator(ContainerSize, e.X, e.Y, color, rect_num);
                        animators.Add(a);
                        a.Start();
                        
                        Thread.Sleep(3000);
                    }

                }
                catch (ArgumentException e) { }

            });

            sqT.IsBackground = true;
            sqT.Start();
        }

        public void Start()
        {
            isAlive = true;
            t = new Thread(() =>
            {
                try
                {
                    while (isAlive)
                    {
                        animators.RemoveAll(it => !it.IsAlive);
                        lock (locker)
                        {
                            if (PaintOnBuffer())
                            {
                                bg.Render(MainGraphics);
                                check_crash();
                            }
                        }
                        if (isAlive) Thread.Sleep(10);
                    }
                }
                catch (ArgumentException e) { }
            });
            t.IsBackground = true;
            t.Start();
        }

        public void Stop()
        {
            isAlive = false;
            t.Interrupt();
        }

        private bool PaintOnBuffer()
        {
            objectsPainted = 0;
            var objectsCount = animators.Count + rects.Count;
            bg.Graphics.Clear(Color.White);

            for (int i = 0; i < animators.Count(); i++)
            {
                animators[i].PaintCircle(bg.Graphics);
                objectsPainted++;
            }

            /*    foreach (var animator in animators)
            {
                animator.PaintCircle(bg.Graphics);
                objectsPainted++;
            }*/

            for (int i = 0; i < rects.Count(); i++)
            {
                rects[i].Paint(bg.Graphics);
                objectsPainted++;
            }
            
            /*foreach (var square in rects)
            {
                square.Paint(bg.Graphics);
                objectsPainted++;
            }*/

            return objectsPainted == objectsCount;
        }

        private double dist(Circle A, Circle B)
        {
            return Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
        }

        private void check_crash()
        {
            bool flag = false;
                for (int i = 0; i < animators.Count();  i++)
                {
                    for (int j = 0; j < animators.Count(); j++)
                    {
                        if (animators[i].C.Rect_num != animators[j].C.Rect_num)
                        {
                            if ((int)dist(animators[i].C, animators[j].C) <= animators[i].C.Diam)
                            {

                                flag = true;
                                rects[animators[j].C.Rect_num - 1].Score += 1;
                                DataBase.Update(animators[j].C.Rect_num, rects[animators[j].C.Rect_num - 1].Score);
                                animators.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (flag) break;
                }
        }
    }
}
