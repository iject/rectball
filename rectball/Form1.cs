namespace rectball
{
    public partial class Form1 : Form
    {
        Painter p;
        bool flag = false;


        public Form1()
        {
            InitializeComponent();
            DB DataBase = new DB();
            DataBase.Truncate();
            p = new Painter(panel1.CreateGraphics(), DataBase);
            p.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                flag = false;
                p.AddRect(e);
            }

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            p.MainGraphics = panel1.CreateGraphics();
        }
    }
}