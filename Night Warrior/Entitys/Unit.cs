using System.Drawing;
using System.Diagnostics;

namespace Night_Warrior.Entitys {
    abstract class Unit: AnimationEntity{
        protected const int MaxForceGravity = 15;
        protected int MaxFramesAttack;
        protected const double G = 0.7;

        protected int horizontalSpeed;
        protected double forceGravity = 0;

        protected bool isDrop = true;
        protected bool isStand = false;
        protected bool isMoveHorizontal = false;
        protected bool isMoveVertical = false;
        protected bool isMoveLeft = false;
        protected bool isMoveReight = false;
        protected bool isAttack = false;

        protected bool canMoveLeft = true;
        protected bool canMoveReight = true;

        protected int framesAttack;

        protected bool directionGazeHorizontal = true;
        protected int directionGazeVertical = 0;
        protected int damage;
        protected int hp;
        public int HP {
            get {
                return hp;
            }
            set {
                hp = value;
            }
        }
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
        public double ForceGravity {
            get {
                return forceGravity;
            }
            set {
                forceGravity = value;
            }
        }
        public bool DirectionGazeHorizontal {
            get {
                return directionGazeHorizontal;
            }
            set {
                directionGazeHorizontal = value;
            }
        }
        public Unit(int x, int y, string imagePath, Size size, Rectangle region, int hS, int hitboxOffset) : base(x, y, imagePath, size, region, hitboxOffset) {
            horizontalSpeed = hS;
        }
        protected virtual void IsMoveHorizontal() {
            if (isMoveLeft || isMoveReight) {
                isMoveHorizontal = true;
                return;
            }
            isMoveHorizontal = false;
        }
        public void MoveLeft() {
            canMoveReight = true;
            x -= horizontalSpeed;
            directionGazeHorizontal = false;
            SetHitBox(0, 0, Size);
            isMoveHorizontal = true;
        }
        public void MoveReight() {
            canMoveLeft = true;
            x += horizontalSpeed;
            directionGazeHorizontal = true;
            isMoveHorizontal = true;
            SetHitBox(0, 0, Size);
        }
        public void Drop(bool motionVertical) {
            isMoveVertical = true;
            if (forceGravity <= MaxForceGravity) {
                forceGravity += G;
            }
            if (motionVertical) {
                y += forceGravity;
                SetHitBox(0, 0, Size);
            }
        }
        public void CorrectY(double y) {
            this.y = 1080 - y - size.Height;
            SetHitBox(0, 0, Size);
        }
        public virtual void TakingDamage(int damage) {
            hp -= damage;
        }
        public virtual void StartAttack() {
            if (!isAttack) {
                isAttack = true;
                framesAttack = MaxFramesAttack;
            }
        }
        protected Point A;
        protected Point B;
        protected Point C;
        protected Point D;
        protected Point AB;
        protected Point BC;
        protected Point CD;
        protected Point DA;
        public void UpdatePoints() {
            A = new Point(hitBox.X, hitBox.Y);
            B = new Point(hitBox.X + hitBox.Width, hitBox.Y);
            C = new Point(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height);
            D = new Point(hitBox.X, hitBox.Y + hitBox.Height);
            AB = new Point(hitBox.X + hitBox.Width / 2, hitBox.Y);
            BC = new Point(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height / 2);
            CD = new Point(hitBox.X + hitBox.Width / 2, hitBox.Y + hitBox.Height);
            DA = new Point(hitBox.X, hitBox.Y + hitBox.Height / 2);
        }
        public virtual int GetWayHitboxesInteract(Rectangle groundHitbox) {
            if (IsPointInHitbox(groundHitbox, D) && IsPointInHitbox(groundHitbox, C)) {
                return 0;
            }
            if (IsPointInHitbox(groundHitbox, A) && IsPointInHitbox(groundHitbox, D)) {
                return 1;
            }
            if (IsPointInHitbox(groundHitbox, A) && IsPointInHitbox(groundHitbox, B)) {
                return 2;
            }
            if (IsPointInHitbox(groundHitbox, B) && IsPointInHitbox(groundHitbox, C)) {
                return 3;
            }
            return -1;
        }
        protected bool IsPointInHitbox(Rectangle groundHitbox, Point point) {
            if ((point.X <= groundHitbox.X + groundHitbox.Width && point.X >= groundHitbox.X) && (point.Y <= groundHitbox.Y + groundHitbox.Height && point.Y >= groundHitbox.Y)) {
                return true;
            }
            return false;
        }
    }
}
