using Night_Warrior.Entitys;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace Night_Warrior.TheScene {
    abstract class Scene {
        protected List<Ground> grounds;
        protected List<Enemy> enemies;
        protected Background background;
        protected static Character character;
        protected const int ForceGravity = 25;
        protected const double G = 2;
        protected Graphics graphics;
        protected bool motionHorizontal = true;
        protected bool motionVertical = true;
        protected Size levelSize;
        protected Rectangle visibleArea;
        protected Graphics Graphics {
            set {
                graphics = value;
            }
        }
        public void CameraMovement() {
            motionHorizontal = true;
            motionVertical = true;
            if (character.X > visibleArea.Width / 2) {
                if (character.IsMoveReight) {
                    foreach(Ground ground in grounds) {
                        ground.MovingInScene((-character.HorizontalSpeed), 0);
                    }
                    foreach(Enemy enemy in enemies) {
                        enemy.MovingInScene((-character.HorizontalSpeed), 0);
                    }
                    visibleArea.X -= character.HorizontalSpeed;
                    motionHorizontal = false;
                }
            }
            if (visibleArea.X != 0 && character.X < visibleArea.Width / 2) {
                if (character.IsMoveLeft) {
                    foreach (Ground ground in grounds) {
                        ground.MovingInScene(character.HorizontalSpeed, 0);
                    }
                    foreach (Enemy enemy in enemies) {
                        enemy.MovingInScene(character.HorizontalSpeed, 0);
                    }
                    visibleArea.X += character.HorizontalSpeed;
                    motionHorizontal = false;
                }
            }
            if (!character.IsStand && (character.Y > visibleArea.Height / 2)) {
                if (character.IsJump) {
                    foreach (Ground ground in grounds) {
                        ground.MovingInScene(0, -1 * character.VerticalSpeed);
                    }
                    foreach (Enemy enemy in enemies) {
                        enemy.MovingInScene(0, -1 * character.VerticalSpeed);
                    }
                    visibleArea.Y -= (int)character.VerticalSpeed;
                    motionVertical = false;
                }
                if (character.IsDrop) {
                    foreach (Ground ground in grounds) {
                        ground.MovingInScene(0, -1 * ForceGravity);
                    }
                    foreach (Enemy enemy in enemies) {
                        enemy.MovingInScene(0, -1 * ForceGravity);
                    }
                    visibleArea.Y -= ForceGravity;
                    motionVertical = false;
                }
            }
            if (visibleArea.Y < 0) {
                foreach (Ground ground in grounds) {
                    ground.MovingInScene(0, -1 * visibleArea.Y);
                }
                character.CorrectY(character.Y + visibleArea.Y);
                visibleArea.Y = 0;
            }
        }
        public void Examination() {
            //Перед вами алгоритм просчитывания взаимодействия хитбоксов 3000
            //Я уже не понимаю как он работает
            //Но он работает
            //Надеюсь, ничего не сломается в процессе работы, ибо починить его я не смогу
            character.CanMoveLeft = true;
            if (character.HitBox.X < 5) {
                character.CanMoveLeft = false;
            }
            foreach (Ground ground in grounds) {
                if (character.HitBox.IntersectsWith(ground.HitBox)) {
                    if (ground.GetCenterHitbox().Y > character.GetCenterHitbox().Y) {
                        if (!character.IsStand && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < character.GetCenterHitbox().X + character.HitBox.Width / 2 && ground.GetCenterHitbox().Y - ground.HitBox.Height / 2 < character.GetCenterHitbox().Y + character.HitBox.Height / 2) {
                            character.IsDrop = false;
                            character.CanJump = true;
                            character.IsJump = false;
                            character.IsStand = true;
                            character.CorrectY(ground.Y + ground.HitBox.Height - 1);
                        }
                    }
                    if (ground.GetCenterHitbox().Y < character.GetCenterHitbox().Y) {
                        if (!character.IsStand && ground.GetCenterHitbox().X + ground.HitBox.Width / 2 > character.GetCenterHitbox().X - character.HitBox.Width / 2 && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < character.GetCenterHitbox().X + character.HitBox.Width / 2 && ground.GetCenterHitbox().Y + ground.HitBox.Height / 2 > character.GetCenterHitbox().Y - character.HitBox.Height / 2) {
                            character.IsJump = false;
                            character.IsDrop = true;
                            character.CorrectY(ground.Y -character.HitBox.Height - 1);
                        }
                    }
                } else {
                    if (!character.IsJump) {
                        character.IsDrop = true;
                    } else {
                        character.IsDrop = false;
                    }
                }
                foreach(Enemy enemy in enemies) {
                    if (enemy.HitBox.IntersectsWith(ground.HitBox)) {
                        if (ground.GetCenterHitbox().Y > enemy.GetCenterHitbox().Y) {
                            if (!enemy.IsStand && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < enemy.GetCenterHitbox().X + enemy.HitBox.Width / 2 && ground.GetCenterHitbox().Y - ground.HitBox.Height / 2 < enemy.GetCenterHitbox().Y + enemy.HitBox.Height / 2) {
                                enemy.IsDrop = false;
                                enemy.IsStand = true;
                                enemy.CorrectY(ground.Y + ground.HitBox.Height - 1);
                            }
                        }
                        if (ground.GetCenterHitbox().Y < enemy.GetCenterHitbox().Y) {
                            if (!enemy.IsStand && ground.GetCenterHitbox().X + ground.HitBox.Width / 2 > enemy.GetCenterHitbox().X - enemy.HitBox.Width / 2 && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < enemy.GetCenterHitbox().X + enemy.HitBox.Width / 2 && ground.GetCenterHitbox().Y + ground.HitBox.Height / 2 > enemy.GetCenterHitbox().Y - enemy.HitBox.Height / 2) {
                                enemy.IsDrop = true;
                                enemy.CorrectY(ground.Y - enemy.HitBox.Height - 1);
                            }
                        }
                    } else {
                        enemy.IsDrop = true;
                    }
                    Debug.WriteLine(enemy.IsDrop);
                }
            }
        }
        public void Update() {
            CameraMovement();
            Drop();
            foreach (Enemy enemy in enemies) {
                enemy.IIUpdate(character.GetCenterHitbox());
            }
        }
        public void SetGrafics(Graphics g) {
            graphics = g;
            foreach (Ground ground in grounds) {
                ground.Graphics = graphics;
            }
            foreach (Enemy enemy in enemies) {
                enemy.Graphics = graphics;
            }
            character.Graphics = graphics;
            background.Graphics = graphics;
        }
        public void PaintBackground() {
            background.Draw();
        }
        public void PaintInsides() {
            foreach (Ground ground in grounds) {
                ground.Draw();
            }
            foreach (Enemy enemy in enemies) {
                enemy.Draw();
            }
            character.Draw();
        }
        public void MoveLeft() {
            if (character.CanMoveLeft && motionHorizontal) {
                character.MoveLeft();
            }
            character.IsMoveLeft = true;
            character.IsStand = false;
        }
        public void MoveReight() {
            if (character.CanMoveReight && motionHorizontal) {
                character.MoveReight();
            }
            character.IsMoveReight = true;
            character.IsStand = false;
        }
        public void Drop() {
            if (!character.IsStand && character.IsDrop && !character.IsJump && motionVertical) {
                character.Drop();
            }
            foreach (Enemy enemy in enemies) {
                if (!enemy.IsStand && enemy.IsDrop) {
                    enemy.Drop();
                }
            }
            if (!character.IsStand && character.IsJump) {
                if (!character.Jump(motionVertical)) {
                    character.IsJump = false;
                    character.IsDrop = true;
                }
            }
        }
        public static void Jump() {
            if (!character.IsJump && character.CanJump) {
                character.StartJump();
                character.IsJump = true;
                character.IsDrop = false;
                character.CanJump = false;
                character.IsStand = false;
            }
        }
        public static void StopMoveLeft() {
            character.IsMoveLeft = false;
        }
        public static void StopMoveReight() {
            character.IsMoveReight = false;
        }
    }
}
