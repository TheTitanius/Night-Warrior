using System;
using System.Drawing;
using System.Windows.Forms;
using Night_Warrior.TheScene;
using System.Collections.Generic;
using System.Diagnostics;

namespace Night_Warrior {
    public partial class MainMenuForm : Form {
        private bool isADown = false;
        private bool isDDown = false;
        private bool isSpaceDown = false;
        private bool isSpaceUp = false;
        private TestLevel testLevel;
        int i = 5;
        public MainMenuForm() {
            InitializeComponent();
            timer1 = new Timer();
            timer1.Interval = 33;
            timer1.Tick += new EventHandler(Update);

            examinationTimer = new Timer();
            examinationTimer.Interval = 2;
            examinationTimer.Tick += new EventHandler(Examination);

            dashTimer = new Timer();
            dashTimer.Interval = 800;
            dashTimer.Tick += new EventHandler(DelayDash);

            TestLevel.CreateTestLevel();
            testLevel = TestLevel.GetTestLevel();
        }
        private void Form_Load(object sender, EventArgs e) {
            examinationTimer.Start();
            timer1.Start();
            dashTimer.Start();
        }
        public void DelayDash(object sender, EventArgs e) {
            Scene.character.DelayDash();
        }
        public void Examination(object sender, EventArgs e) {
            testLevel.Examination();
            if (Scene.character.HP <= 0) {
                Close();
            }/*
            if(testLevel.enemies[0].framesAttack == 1) {
                examinationTimer.Stop();
                timer1.Stop();
                dashTimer.Stop();
            }*/
        }
        public void Update(object sender, EventArgs e) {
            testLevel.Update();
            if (isADown) {
                testLevel.MoveLeft();
            }
            if (isDDown) {
                testLevel.MoveReight();
            }
            if (isSpaceDown && !isSpaceUp) {
                //isSpaceUp = true;
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
            if (Scene.character.IsAttack) {
                i--;/*
                if (i == 0) {
                    examinationTimer.Stop();
                    timer1.Stop();
                    dashTimer.Stop();
                }*/
            }
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
                    Scene.StopMoveLeft();
                    break;
                case Keys.D:
                    isDDown = false;
                    Scene.StopMoveReight();
                    break;
                case Keys.Space:
                    isSpaceDown = false;
                    isSpaceUp = false;
                    break;
            }
        }
        private void MainMenuForm_MouseDown(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Right:
                    testLevel.Dash();
                    break;
                case MouseButtons.Left:
                    Scene.character.StartAttack();
                    break;
            }
        }
    }
}
