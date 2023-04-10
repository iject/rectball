namespace rectball
{
    public partial class Form1 : Form
    {
        Graphics g;
        bool flag = false;


        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Rect r = new Rect(10, 10, 30, 30, Color.Aqua);
            r.Paint(g);*/

            flag = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                Rect r = new Rect(e.X, e.Y, 30, 30);
                r.Paint(g);
                flag = false;
            }

        }
    }
}