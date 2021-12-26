using Night_Warrior.Entitys;
using System.Collections.Generic;
using System.Drawing;

namespace Night_Warrior.TheScene {
    abstract class Scene {
        protected List<Ground> grounds;
        protected Background background;
        protected static Character character;
        protected const int ForceGravity = 25;
        protected const double G = 1.5;
        protected Graphics graphics;
        private bool paintFlag = true;
        protected bool isDrop = true;
        protected bool isJump = false;
        protected bool canJump = true;
        protected bool canMoveLeft = true;
        protected bool canMoveReight = true;
        protected bool isMoveLeft = false;
        protected bool isMoveReight = false;
        protected bool isStand = false;
        protected bool motion = true;
        protected Size levelSize;
        protected Rectangle visibleArea;
        protected Graphics Graphics {
            set {
                graphics = value;
            }
        }
        public void CameraMovement() {
            motion = true;
            if(character.X > visibleArea.Width / 2) {
                if (isMoveReight) {
                    foreach(Ground ground in grounds) {
                        ground.MovingInScene((-character.HorizontalSpeed), 0);
                    }
                    visibleArea.X -= character.HorizontalSpeed;
                    motion = false;
                }
            }
            if (visibleArea.X != 0 && character.X < visibleArea.Width / 2) {
                if (isMoveLeft) {
                    foreach (Ground ground in grounds) {
                        ground.MovingInScene((character.HorizontalSpeed), 0);
                    }
                    visibleArea.X += character.HorizontalSpeed;
                    motion = false;
                }
            }
        }
        public void Examination() {
            //Перед вами алгоритм просчитывания взаимодействия хитбоксов 3000
            //Я уже не понимаю как он работает
            //Но он работает
            //Надеюсь, ничего не сломается в процессе работы, ибо починить его я не смогу
            CameraMovement();
            canMoveLeft = true;
            if (character.HitBox.X < 5) {
                canMoveLeft = false;
            }
            foreach (Ground ground in grounds) {
                if (character.HitBox.IntersectsWith(ground.HitBox)) {
                    if (!isStand) {
                        paintFlag = false;
                    } else {
                        paintFlag = true;
                    }
                    if (ground.GetCenterHitbox().Y > character.GetCenterHitbox().Y) {
                        if (!isStand && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < character.GetCenterHitbox().X + character.HitBox.Width / 2 && ground.GetCenterHitbox().Y - ground.HitBox.Height / 2 < character.GetCenterHitbox().Y + character.HitBox.Height / 2) {
                            isDrop = false;
                            canJump = true;
                            isJump = false;
                            isStand = true;
                            character.CorrectY((int)ground.Y + ground.HitBox.Height - 1);
                        }
                    }
                    if (ground.GetCenterHitbox().Y < character.GetCenterHitbox().Y) {
                        if (!isStand && ground.GetCenterHitbox().X + ground.HitBox.Width / 2 > character.GetCenterHitbox().X - character.HitBox.Width / 2 && ground.GetCenterHitbox().X - ground.HitBox.Width / 2 < character.GetCenterHitbox().X + character.HitBox.Width / 2 && ground.GetCenterHitbox().Y + ground.HitBox.Height / 2 > character.GetCenterHitbox().Y - character.HitBox.Height / 2) {
                            isJump = false;
                            isDrop = true;
                            character.CorrectY((int)ground.Y -character.HitBox.Height - 1);
                        }
                    }
                } else {
                    paintFlag = true;
                    if (!isJump) {
                        isDrop = true;
                    } else {
                        isDrop = false;
                    }
                }
            }
        }
        public void SetGrafics(Graphics g) {
            graphics = g;
            foreach (Ground ground in grounds) {
                ground.Graphics = graphics;
            }
            character.Graphics = graphics;
            background.Graphics = graphics;
        }
        public void PaintBackground() {
            background.Draw();
        }
        public void PaintInsides() {
            if (paintFlag) {
                foreach (Ground ground in grounds) {
                    ground.Draw();
                }
                character.Draw();
            }
        }
        public void MoveLeft() {
            if (canMoveLeft && motion) {
                character.MoveLeft();
                isStand = false;
                isMoveLeft = true;
            }
        }
        public void MoveReight() {
            if (canMoveReight && motion) {
                character.MoveReight();
                isStand = false;
                isMoveReight = true;
            }
        }
        public void Drop() {
            if (!isStand && isDrop && !isJump) {
                character.Drop();
            }
            if (!isStand && isJump) {
                if (!character.Jump()) {
                    isJump = false;
                    isDrop = true;
                }
            }
        }
        public void Jump() {
            if (!isJump && canJump) {
                character.StartJump();
                isJump = true;
                isDrop = false;
                canJump = false;
                isStand = false;
            }
        }
        public void StopMoveLeft() {
            isMoveLeft = false;
        }
        public void StopMoveReight() {
            isMoveReight = false;
        }
    }
}
