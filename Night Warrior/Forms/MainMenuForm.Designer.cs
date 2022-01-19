
namespace Night_Warrior {
    partial class MainMenuForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.examinationTimer = new System.Windows.Forms.Timer(this.components);
            this.dashTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // MainMenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenuForm";
            this.Text = "Night Warrior";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainMenuForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainMenuForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainMenuForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainMenuForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer examinationTimer;
        private System.Windows.Forms.Timer dashTimer;
    }
}

