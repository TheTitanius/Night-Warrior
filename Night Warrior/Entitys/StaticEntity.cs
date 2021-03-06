using System.Drawing;

namespace Night_Warrior.Entitys {
    class StaticEntity: Entity {
        private Point point;
        private bool isActive;
        public Point Point {
            get {
                return point;
            }
            set {
                point = value;
            }
        }
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                isActive = value;
            }
        }
        public Rectangle Size {
            get {
                return new Rectangle(new Point((int)x, (int)y), size);
            }
        }
        public StaticEntity(int x, int y, string imagePath, Size size) : base(x, y,  imagePath, size) {
            this.size = size;
        }
        public StaticEntity(int x, int y, string imagePath, Size size, Rectangle region) : base(x, y, imagePath, size, region) {
            this.size = size;
        }
        public StaticEntity(int x, int y, string imagePath, Size size, Rectangle region, bool isActive) : base(x, y, imagePath, size, region) {
            this.size = size;
            this.isActive = isActive;
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
        }
        public void ChangeImageForHp() {
            if (isActive) {
                region.X = 0;
            } else {
                region.X = 60;
            }
        }
        public void ChangeImageForImpactWEffects(bool directionGazeHorizontal, int x) {
            if (directionGazeHorizontal) {
                region.X = 0;
            } else {
                region.X = x;
            }
        }
        public void ChangeImageForManna(int amountManna) {
            region.Y = 940 + (136 - amountManna);
            region.Height = amountManna;
            size.Height = amountManna;
            y = 1080 - (922) - size.Height;
        }
        public void SetXY(double x, double y) {
            this.x = x;
            this.y = 1080 - y;
        }
        public void MovingInScene(double hS, double vS) {
            x += hS;
            y += vS;
        }
    }
}