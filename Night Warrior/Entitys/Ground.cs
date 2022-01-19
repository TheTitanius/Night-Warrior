using System.Drawing;

namespace Night_Warrior.Entitys {
    class Ground: StaticEntity {
        private Rectangle hitBox;
        public Rectangle HitBox {
            get {
                return hitBox;
            }
            set {
                hitBox = value;
            }
        }
        public Ground(int x, int y, string imagePath, Size size) : base(x, y, imagePath, size) { }
        public Ground(int x, int y, string imagePath, Size size, Rectangle region): base(x, y, imagePath, size, region) { }
        public Ground(int x, int y, string imagePath, Size size, Rectangle region, Rectangle hitBox) : base(x, y, imagePath, size, region) {
            SetHitBox(hitBox.X, HitBox.Y, new Size(hitBox.Width, hitBox.Height));
        }
        public void SetHitBox(int x, int y, Size size) {
            hitBox = new Rectangle(new Point(x + (int)this.x, (int)this.y + y + this.size.Height - size.Height), size);
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
            graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
        }
        public Point GetCenterHitbox() {
            return new Point(HitBox.X+HitBox.Width/2, HitBox.Y + HitBox.Height / 2);
        }
        public void MovingInScene(double hS, double vS) {
            x += hS;
            y += vS;
            SetHitBox(0, 0, new Size(hitBox.Width, hitBox.Height));
        }
    }
}
