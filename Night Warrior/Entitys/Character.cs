using System.Drawing;

namespace Night_Warrior.Entitys {
    class Character: Unit {
        private double verticalSpeed = 0;
        private readonly double G;
        public Character(int x, int y, string imagePath, Size size, Rectangle region, int hS, int fG, double G) : base(x, y, imagePath, size, region, hS, fG) {
            this.G = G;
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
            graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
        }
        public void StartJump() {
            verticalSpeed = -30;
            y -= 1;
            SetHitBox(0, 0, Size);
        }
        public bool Jump() {
            y += verticalSpeed;
            SetHitBox(0, 0, Size);
            verticalSpeed += G;
            if(verticalSpeed >= forceGravity) {
                verticalSpeed = 0;
                return false;
            }
            return true;
        }
        public void CorrectY(int y){
            this.y = 1080 - y - size.Height;
            SetHitBox(0, 0, Size);
        }
    }
}
