using System.Drawing;

namespace Night_Warrior.Entitys {
    class Character: Unit {
        private double verticalSpeed = 0;
        private bool isJump = false;
        private bool canJump = true;
        protected bool isMoveLeft = false;
        protected bool isMoveReight = false;
        public bool IsJump {
            get {
                return isJump;
            }
            set {
                isJump = value;
            }
        }
        public bool CanJump {
            get {
                return canJump;
            }
            set {
                canJump = value;
            }
        }
        public bool IsMoveLeft {
            get {
                return isMoveLeft;
            }
            set {
                isMoveLeft = value;
            }
        }
        public bool IsMoveReight {
            get {
                return isMoveReight;
            }
            set {
                isMoveReight = value;
            }
        }
        public double VerticalSpeed {
            get {
                return verticalSpeed;
            }
        }
        private readonly double G;
        public Character(int x, int y, string imagePath, Size size, Rectangle region, int hS, int fG, double G) : base(x, y, imagePath, size, region, hS, fG) {
            this.G = G;
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
            graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
        }
        public void StartJump() {
            verticalSpeed = -32;
            y -= 1;
            SetHitBox(0, 0, Size);
        }
        public bool Jump(bool motionVertical) {
            if (motionVertical) {
                y += verticalSpeed;
                SetHitBox(0, 0, Size);
            }
            verticalSpeed += G;
            if(verticalSpeed >= forceGravity) {
                verticalSpeed = 0;
                return false;
            }
            return true;
        }
    }
}
