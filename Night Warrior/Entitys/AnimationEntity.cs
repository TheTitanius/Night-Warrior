using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class AnimationEntity : Entity {
        protected Rectangle hitBox;
        public Rectangle HitBox {
            get {
                return hitBox;
            }
            set {
                hitBox = value;
            }
        }
        protected int maxFrame;
        public int MaxFrame {
            get {
                return maxFrame;
            }
            set {
                maxFrame = value;
            }
        }

        protected int currentFrame = 0;
        public AnimationEntity(int x, int y, string imagePath, Size size, Rectangle region): base (x, y, imagePath, size, region) { }
        public AnimationEntity(int x, int y, string imagePath, Size size, Rectangle region, int maxFrame) : base(x, y, imagePath, size, region) {
            this.maxFrame = maxFrame;
        }
        public void SetHitBox(int x, int y, Size size) {
            hitBox = new Rectangle(new Point(x + (int)this.x, (int)this.y + y + this.size.Height - size.Height), size);
        }
        public Point GetCenterHitbox() {
            return new Point(HitBox.X + HitBox.Width / 2, HitBox.Y + HitBox.Height / 2);
        }
    }
}
