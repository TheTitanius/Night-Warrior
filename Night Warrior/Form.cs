using System;
using System.Drawing;
using System.Windows.Forms;

using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.WinForms;
using OpenTK.Mathematics;

namespace Night_Warrior {
    public partial class Form : System.Windows.Forms.Form {
        private GLControl control;
        private Button button1;
        private bool loaded = false;
        public Form() {
            InitializeComponent();
        }
        
        private void GlControl1_Loaded(object sender, EventArgs e) {
            GL.ClearColor(Color4.Skyblue);
        }
        private void GlControl1_Paint(object sender, PaintEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            control.SwapBuffers();
        }

        private void GlControl1_Click(object sender, EventArgs e) {
            GL.ClearColor(Color4.Red);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            control.SwapBuffers();
        }

        private void Form_Load(object sender, EventArgs e) {

        }
    }
}
