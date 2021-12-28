using System.Drawing;

namespace Night_Warrior.Entitys {
    class Enemy: Unit {
        private const int viewRadius = 100;
        private const int attackRadius = 20;
        private bool isAttacks;
        public Enemy(int x, int y, string imagePath, Size size, Rectangle region, int hS, double fG) : base(x, y, imagePath, size, region, hS, fG) {
            SetHitBox(0, 0, Size);
        }
        public override void Draw() {
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
            graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
        }
        public void MovingInScene(double hS, double vS) {
            x += hS;
            y += vS;
            SetHitBox(0, 0, new Size(hitBox.Width, hitBox.Height));
        }
        public void IIUpdate(Point CharacterCenter) {
            if(CharacterCenter.X < GetCenterHitbox().X + viewRadius && CharacterCenter.X > GetCenterHitbox().X - viewRadius) {
                isAttacks = true;
            }
            if (isAttacks) {
                if(CharacterCenter.X < GetCenterHitbox().X + attackRadius + size.Width/2 && CharacterCenter.X > GetCenterHitbox().X - attackRadius - size.Width / 2) {
                    Attack();
                } else {
                    if(CharacterCenter.X > GetCenterHitbox().X) {
                        MoveReight();
                    }
                    if (CharacterCenter.X < GetCenterHitbox().X) {
                        MoveLeft();
                    }
                }
            }
        }
        public void Attack() {

        }
    }
}
