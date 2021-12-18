using System;
using System.Drawing;
using System.Windows.Forms;

namespace Night_Warrior {
    public partial class MainMenuForm : Form {
        private Timer timer;
        private PictureBox exit;
        private PictureBox menuFon;
        private bool first = true;
        private int cadr = 1;
        public MainMenuForm() {
            InitializeComponent();
            exit.Location = new Point(Width / 2 - exit.Width/2, Height/2 - exit.Height/2);
        }
        private void GlControl1_Loaded(object sender, EventArgs e) {
        }
        private void GlControl1_Paint(object sender, PaintEventArgs e) {
        }
        private void GlControl1_Click(object sender, EventArgs e) {
        }
        private void Form_Load(object sender, EventArgs e) {

        }
        private void exit_Click(object sender, EventArgs e) {
            Close();
        }
        private void menuFon_Click(object sender, EventArgs e) {

        }
        private void exit_MouseHover(object sender, EventArgs e) {
            timer = new Timer();
            if (first) {
                timer.Interval = 150;
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
            }
        }
        private void Timer_Tick(object Sender, EventArgs e) {
            exit.Image = Image.FromFile(@$"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\exit_{cadr}.png");
            cadr++;
            if(cadr == 6) {
                first = false;
                cadr = 1;
                timer.Stop();
            }
        }
        private void Exit_MouseLeave(object sender, EventArgs e) {
            first = true;
            exit.Image = Image.FromFile(@"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\exit_5.png");
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            menuFon.Image = Image.FromFile(@"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\menu_fon1.gif");
            menuFon.Location = new Point(0, 0);
            menuFon.Name = "menuFon";
            menuFon.Size = new Size(1920, 1080);
            menuFon.SizeMode = PictureBoxSizeMode.AutoSize;
            menuFon.TabIndex = 0;
            menuFon.TabStop = false;
            menuFon.Controls.Add(exit);
        }
    }
}
