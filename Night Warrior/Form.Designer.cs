
namespace Night_Warrior {
    partial class Form {
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
            this.control = new OpenTK.WinForms.GLControl();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // control
            // 
            this.control.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            this.control.APIVersion = new System.Version(4, 0, 0, 0);
            this.control.BackColor = System.Drawing.Color.Red;
            this.control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            this.control.IsEventDriven = true;
            this.control.Location = new System.Drawing.Point(0, 0);
            this.control.Name = "control";
            this.control.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            this.control.Size = new System.Drawing.Size(1920, 1055);
            this.control.TabIndex = 1;
            this.control.Load += new System.EventHandler(this.GlControl1_Loaded);
            this.control.Click += new System.EventHandler(this.GlControl1_Click);
            this.control.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl1_Paint);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Location = new System.Drawing.Point(762, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(376, 228);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ты пидор";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1055);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.control);
            this.Name = "Form";
            this.Text = "Night Warrior";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

