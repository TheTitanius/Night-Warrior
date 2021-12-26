using System.Drawing;

namespace Night_Warrior.Entitys {
    class Enemy: Unit {
        private const int viewRadius = 100;
        private bool isAttacks;
        public Enemy(int x, int y, string imagePath, Size size, Rectangle region, int hS, double fG) : base(x, y, imagePath, size, region, hS, fG) {
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
            graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
        }
        public void IIUpdate() {

        }
    }
}
