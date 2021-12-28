using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class Unit: AnimationEntity{
        protected int horizontalSpeed;
        protected double forceGravity;
        protected bool isDrop = true;
        protected bool isStand = false;
        protected bool canMoveLeft = true;
        protected bool canMoveReight = true;
        public bool IsDrop {
            get {
                return isDrop;
            }
            set {
                isDrop = value;
            }
        }
        public bool IsStand {
            get {
                return isStand;
            }
            set {
                isStand = value;
            }
        }
        public bool CanMoveLeft {
            get {
                return canMoveLeft;
            }
            set {
                canMoveLeft = value;
            }
        }
        public bool CanMoveReight {
            get {
                return canMoveReight;
            }
            set {
                canMoveReight = value;
            }
        }
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
        public void CorrectY(double y) {
            this.y = 1080 - y - size.Height;
            SetHitBox(0, 0, Size);
        }
    }
}
