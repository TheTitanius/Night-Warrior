using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class Unit: AnimationEntity{
        protected int horizontalSpeed;
        protected double forceGravity;
        public int HorizontalSpeed {
            get {
                return horizontalSpeed;
            }
            set {
                horizontalSpeed = value;
            }
        }
        public Unit(int x, int y, string imagePath, Size size, Rectangle region, int hS, double fG) : base(x, y, imagePath, size, region) {
            horizontalSpeed = hS;
            forceGravity = fG;
        }
        public void MoveLeft() {
            x -= horizontalSpeed;
            SetHitBox(0, 0, Size);
        }
        public void MoveReight() {
            x += horizontalSpeed;
            SetHitBox(0, 0, Size);
        }
        public void Drop() {
            y += forceGravity;
            SetHitBox(0, 0, Size);
        }
    }
}
