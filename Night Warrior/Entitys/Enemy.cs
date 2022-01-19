using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Night_Warrior.Entitys {
    class Enemy: Unit {

        private const int viewRadius = 200;
        private const int attackRadius = 80;
        private bool isDied = false;
        public Enemy(int x, int y, string imagePath, Size size, Rectangle region, int hS, int hp, int damage, int hitboxOffset) : base(x, y, imagePath, size, region, hS, hitboxOffset) {
            this.hp = hp;
            this.damage = damage;
            SetHitBox(0, 0, Size);
            MaxFramesAttack = 8;
        }
        protected override void RenderingAnimations() {
            IsMoveHorizontal();

            size.Height = 160;
            if (directionGazeHorizontal) {
                region.X = 0;
                SetHitBox(0, 0, new Size(80, 160));
            } else {
                region.X = 100;
                SetHitBox(20, 0, new Size(80, 160));
            }
            if (isAttack) {
                if (framesAttack > 0) {
                    framesAttack--;/*
                    if (framesAttack > 2) {
                        size.Height = 180;
                        if (directionGazeHorizontal) {
                            region.X = 200;
                        } else {
                            region.X = 300;
                        }
                    } else {
                        if (directionGazeHorizontal) {
                            region.X = 400;
                        } else {
                            region.X = 520;
                        }
                    }*/
                }
            } else {
                Debug.WriteLine("Lol");
                isAttack = false;
            }
            UpdatePoints();
        }
        public override void Draw() {
            if (!isDied) {
                RenderingAnimations();
                graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
                //graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
                
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(A, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(B, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(C, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(D, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(AB, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(BC, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(CD, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(DA, new Size(2, 2)));
            }
        }
        public void MovingInScene(double hS, double vS) {
            x += hS;
            y += vS;
            SetHitBox(0, 0, new Size(hitBox.Width, hitBox.Height));
        }
        public void IIUpdate(Character character) {
            if(hp == 0) {
                isDied = true;
            }
            if (!isDied) {
                if (isAttack) {
                    if (framesAttack < 2 && character.GetCenterHitbox().X < GetCenterHitbox().X + attackRadius + size.Width / 2 && character.GetCenterHitbox().X > GetCenterHitbox().X - attackRadius - size.Width / 2 && character.GetCenterHitbox().Y < GetCenterHitbox().Y + attackRadius + size.Height / 2 && character.GetCenterHitbox().Y > GetCenterHitbox().Y - attackRadius - size.Height / 2) {
                        if (!character.IsInvulnerability) {
                            character.TakingDamage(damage);
                        }
                    }
                } else {
                    if (character.GetCenterHitbox().X > GetCenterHitbox().X) {
                        MoveReight();
                        isStand = false;
                    }
                    if (character.GetCenterHitbox().X < GetCenterHitbox().X) {
                        MoveLeft();
                        isStand = false;
                    }
                    if (character.GetCenterHitbox().X < GetCenterHitbox().X + viewRadius && character.GetCenterHitbox().X > GetCenterHitbox().X - viewRadius && character.GetCenterHitbox().Y < GetCenterHitbox().Y + viewRadius / 2 && character.GetCenterHitbox().Y > GetCenterHitbox().Y - viewRadius / 2) {
                        StartAttack();
                    }
                }
            }
        }
        public List<Point> GetPoints() {
            return new List<Point> { A, B, C, D, AB, BC, CD, DA};
        }
    }
}
