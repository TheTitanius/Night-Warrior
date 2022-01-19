using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class AnimationEntity : Entity {
        protected Rectangle hitBox;
        protected int hitboxOffset = 0;
        public Rectangle HitBox {
            get {
                return hitBox;
            }
            set {
                hitBox = value;
            }
        }

        protected int currentFrame = 0;
        public AnimationEntity(int x, int y, string imagePath, Size size, Rectangle region, int hitboxOffset) : base (x, y, imagePath, size, region) {
            this.hitboxOffset = hitboxOffset;
        }
        public void SetHitBox(int x, int y, Size size) {
            hitBox = new Rectangle(new Point(x + (int)this.x + hitboxOffset, (int)this.y + y + this.size.Height - size.Height), new Size(size.Width - 2*hitboxOffset, size.Height));
        }
        public Point GetCenterHitbox() {
            return new Point(HitBox.X + HitBox.Width / 2, HitBox.Y + HitBox.Height / 2);
        }
        protected abstract void RenderingAnimations();
    }
}
