using System;
using System.Drawing;
using System.Windows.Forms;
using Night_Warrior.TheScene;

namespace Night_Warrior {
    public partial class MainMenuForm : Form {
        private bool isADown = false;
        private bool isDDown = false;
        private bool isSpaceDown = false;
        private TestLevel testLevel;
        public MainMenuForm() {
            InitializeComponent();
            timer1 = new Timer();
            timer1.Interval = 33;
            timer1.Tick += new EventHandler(Update);

            examinationTimer = new Timer();
            examinationTimer.Interval = 1;
            examinationTimer.Tick += new EventHandler(Examination);

            TestLevel.CreateTestLevel();
            testLevel = TestLevel.GetTestLevel();
        }
        private void Form_Load(object sender, EventArgs e) {
            timer1.Start();
            examinationTimer.Start();
        }
        public void Examination(object sender, EventArgs e) {
            testLevel.Examination();
        }
        public void Update(object sender, EventArgs e) {
            testLevel.Drop();
            if (isADown) {
                testLevel.MoveLeft();
            }
            if (isDDown) {
                testLevel.MoveReight();
            }
            if (isSpaceDown) {
                testLevel.Jump();
            }
            Invalidate();
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            testLevel.SetGrafics(e.Graphics);
            testLevel.PaintBackground();
        }
        private void MainMenuForm_Paint(object sender, PaintEventArgs e) {
            testLevel.SetGrafics(e.Graphics);
            testLevel.PaintInsides();
        }
        private void MainMenuForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.A:
                    isADown = true;
                    break;
                case Keys.D:
                    isDDown = true;
                    break;
                case Keys.Space:
                    isSpaceDown = true;
                    break;
            }
        }
        private void MainMenuForm_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.A:
                    isADown = false;
                    testLevel.StopMoveLeft();
                    break;
                case Keys.D:
                    isDDown = false;
                    testLevel.StopMoveReight();
                    break;
                case Keys.Space:
                    isSpaceDown = false;
                    break;
            }
        }
    }
}
