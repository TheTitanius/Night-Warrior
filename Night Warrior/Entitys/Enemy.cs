using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Night_Warrior.Entitys {
    class Enemy: Unit {
        private const int MaxFramesInvulnerability = 5;
        private const int MannaFromHit = 34;
        private const int viewRadius = 500;
        private const int attackRadius = 200;

        private bool isDied = false;
        private bool correctYT = false;
        private bool isNoticedCharacter = false;

        public int GetMannaFromHit {
            get {
                return MannaFromHit;
            }
        }
        private StaticEntity impactwEffects;
        public Enemy(int x, int y, string imagePath, Size size, Rectangle region, int hS, int hp, int damage, int hitboxOffset) : base(x, y, imagePath, size, region, hS, hitboxOffset) {
            this.hp = hp;
            this.damage = damage;
            SetHitBox(0, 0, Size);
            MaxFramesAttack = 14;
            impactwEffects = new StaticEntity(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\enemy.png", new Size(160, 120), new Rectangle(0, 180, 160, 120));
        }
        public override void RenderingAnimations() {
            IsMoveHorizontal();

            region.Y = 0;
            region.Height = 160;
            size.Height = 160;
            size.Width = 100;
            region.Width = 100;
            if (directionGazeHorizontal) {
                region.X = 0;
                SetHitBox(0, 0, new Size(80, 160));
            } else {
                region.X = 100;
                SetHitBox(20, 0, new Size(80, 160));
            }
            if (isAttack) {
                if (framesAttack > 0) {
                    if (framesAttack > 2) {
                        size.Height = 180;
                        region.Height = 180;
                        if (!correctYT) {
                            correctYT = true;
                            y -= 20;
                        }
                        if (directionGazeHorizontal) {
                            region.X = 200;
                            SetHitBox(40, 0, new Size(60, 160));
                        } else {
                            region.X = 300;
                            SetHitBox(0, 0, new Size(60, 160));
                        }
                    } else {
                        if (correctYT) {
                            correctYT = false;
                            y += 20;
                        }
                        size.Width = 120;
                        region.Width = 120;
                        if (directionGazeHorizontal) {
                            region.X = 400;
                            SetHitBox(40, 0, new Size(40, 160));
                            impactwEffects.SetXY(X + size.Width, Y + size.Height - 10);
                        } else {
                            region.X = 520;
                            SetHitBox(0, 0, new Size(60, 160));
                            impactwEffects.SetXY(X - impactwEffects.Size.Width, Y + size.Height - 10);
                        }
                        impactwEffects.ChangeImageForImpactWEffects(directionGazeHorizontal, 160);
                    }
                    framesAttack--;
                }
                else {
                    isAttack = false;
                    framesAttack = 0;
                }
            }
            if (framesInvulnerability > 0) {
                framesInvulnerability--;
                isInvulnerability = true;
            } else {
                isInvulnerability = false;
            }
            UpdatePoints();
        }
        public override void Draw() {
            if (!isDied) {
                RenderingAnimations();
                graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);
                //graphics.FillRectangle(new SolidBrush(Color.White), hitBox);
                /*
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(A, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(B, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(C, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(D, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(AB, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(BC, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(CD, new Size(2, 2)));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(DA, new Size(2, 2))); 
                */
                if (isAttack) {
                    if (framesAttack < 2) {
                        impactwEffects.Graphics = graphics;
                        impactwEffects.Draw();
                    }
                }

                /*
                for (double y = bL; y < bU; y++) {
                    double a = 0.045;
                    if (directionGazeHorizontal) {
                        a *= -1;
                    } else {
                        a *= 1;
                    }
                    double b = -2 * a * (vY);
                    double c = vX - (a * Math.Pow(vY, 2)) - b * vY;
                    double x = a * Math.Pow(y, 2) + b * y + c;
                    graphics.FillRectangle(new SolidBrush(Color.Red), (int)x, 1080 - (int)y, 2, 2);
                }*/
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
                        Attack(character, 0.045, 160);
                    }
                } else {
                    if (isNoticedCharacter) {
                        if (character.GetCenterHitbox().X > GetCenterHitbox().X) {
                            MoveReight();
                            isStand = false;
                        }
                        if (character.GetCenterHitbox().X < GetCenterHitbox().X) {
                            MoveLeft();
                            isStand = false;
                        }
                        if (character.GetCenterHitbox().X < GetCenterHitbox().X + attackRadius && character.GetCenterHitbox().X > GetCenterHitbox().X - attackRadius && character.GetCenterHitbox().Y < GetCenterHitbox().Y + attackRadius / 4 && character.GetCenterHitbox().Y > GetCenterHitbox().Y - attackRadius / 4) {
                            StartAttack();
                            isStand = true;
                        }
                    } else {
                        if (GetCenterHitbox().X + viewRadius > character.GetCenterHitbox().X && GetCenterHitbox().X - viewRadius < character.GetCenterHitbox().X && character.GetCenterHitbox().Y < GetCenterHitbox().Y + viewRadius / 4 && character.GetCenterHitbox().Y > GetCenterHitbox().Y - viewRadius / 4) {
                            isNoticedCharacter = true;
                            Debug.WriteLine("Ujdyj");
                        }
                    }
                }
            }
        }
        public override void TakingDamage(int damage) {
            base.TakingDamage(damage);
            framesInvulnerability = MaxFramesInvulnerability;
        }
        protected override void Repulsion(bool enemyDirectionGazeHorizontal) {
            base.Repulsion(enemyDirectionGazeHorizontal); 
            int bais;
            if (enemyDirectionGazeHorizontal) {
                bais = 40;
            } else {
                bais = -40;
            }
            impactwEffects.SetXY(impactwEffects.X + bais, impactwEffects.Y);
        }
    }
}
