using System.Drawing;

namespace Night_Warrior.Entitys {
    class Background: StaticEntity {
        public Background(int x, int y, string imagePath, Size size, Graphics graphics) : base(x, y, imagePath, size, graphics) { }
        public Background(int x, int y, string imagePath, Size size) : base(x, y, imagePath, size) { }
        public Background(int x, int y, string imagePath, Size size, Rectangle region) : base(x, y, imagePath, size, region) { }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
        }
    }
}