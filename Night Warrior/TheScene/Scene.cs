using Night_Warrior.Entitys;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace Night_Warrior.TheScene {
    abstract class Scene {
        protected List<Ground> grounds;
        protected List<Enemy> enemies;
        protected StaticEntity background;
        public static Character character;
        protected Graphics graphics;
        protected bool motionHorizontal = true;
        protected bool motionVertical = true;
        protected Size levelSize;
        protected Rectangle visibleArea;
        protected double visibleAreaY;
        //protected double visibleAreaX = 0;
        protected Graphics Graphics {
            set {
                graphics = value;
            }
        }
        public void CameraMovement() {
            motionVertical = true;
            if(character.X >= visibleArea.Width / 2 + 1){
                motionHorizontal = false;
                character.CorrectX(visibleArea.Width / 2);
            }
            if (!motionHorizontal && character.IsDash) {
                int hS;
                if (character.DirectionGazeHorizontal) {
                    hS = -1 * (int)character.DashSpeed;
                } else {
                    hS = 1 * (int)character.DashSpeed;

                }
                MovingInScene(hS, 0);
                visibleArea.X += hS;
            }
            if (!character.IsStand) {
                if (character.Y >= visibleArea.Height / 2) {
                    if (character.IsJump) {
                        MovingInScene(0, -1 * character.VerticalSpeed);
                        visibleAreaY -= character.VerticalSpeed;
                        visibleArea.Y -= (int)visibleAreaY;
                        motionVertical = false;
                    }
                    if (character.IsDrop) {
                        character.Drop(false);
                        MovingInScene(0, -1 * character.ForceGravity);
                        visibleAreaY -= character.ForceGravity;
                        visibleArea.Y -= (int)visibleAreaY;
                        motionVertical = false;
                    }
                }

            }
            if (visibleAreaY <= 0) {
                MovingInScene(0, -visibleAreaY);
                character.CorrectY(character.Y + visibleAreaY);
                visibleAreaY = 0;
                visibleArea.Y = (int)visibleAreaY;
                motionVertical = true;
            } else {
                motionVertical = false;
            }
            motionHorizontal = true;
            foreach (Enemy enemy in enemies) {
                if (!motionVertical) {
                    enemy.IsStand = false;
                }
            }
            if(visibleArea.X > 0) {
                bool wasDash = false;
                if (character.IsDash) {
                    wasDash = true;
                }
                MovingInScene(-1 * visibleArea.X, 0);
                character.CorrectX(character.X + visibleArea.X - 9);
                visibleArea.X = 0;
                character.IsDash = wasDash;
            }
        }
        public void Examination() {
            //Перед вами алгоритм просчитывания взаимодействия хитбоксов 3000
            //Я уже не понимаю как он работает
            //Но он работает
            //Надеюсь, ничего не сломается в процессе работы, ибо починить его я не смогу
            DifferentMovement();
            CameraMovement();
            character.UpdatePoints();
            if (character.IsStand) {
                character.CanMoveLeft = true;
                character.CanMoveReight = true;
            }
            if (character.HitBox.X < 2) {
                character.CanMoveLeft = false;
                if (!character.DirectionGazeHorizontal) {
                    character.CanDash = false;
                }
            }
            if(character.X < 0) {
                if (!character.DirectionGazeHorizontal && character.IsDash) {
                    character.StopDash();
                }
                character.CorrectX(2);
            }
            foreach (Ground ground in grounds) {
                if (character.HitBox.IntersectsWith(ground.HitBox)) {
                    int wayHitboxesInteract = character.GetWayHitboxesInteract(ground.HitBox);
                    //Debug.WriteLine(wayHitboxesInteract);
                    switch (wayHitboxesInteract) {
                        case 0:
                            character.IsDrop = false;
                            character.CanJump = true;
                            character.IsJump = false;
                            character.IsStand = true;
                            character.CanCanDash = true;
                            character.CorrectY(ground.Y + ground.HitBox.Height - 1);
                            character.ForceGravity = 0;
                            if (!motionVertical && character.Y > visibleArea.Height / 2) {
                                double heightDifference = character.Y - visibleArea.Height / 2 - 1;
                                visibleAreaY += heightDifference;
                                visibleArea.Y = (int)visibleAreaY;
                                MovingInScene(0, heightDifference);
                                character.CorrectY(character.Y - heightDifference);
                            }
                            break;
                        case 1:
                            StopMoveLeft();
                            character.CanMoveLeft = false;
                            if (character.X >= 5) {
                                character.CorrectX(ground.X + ground.HitBox.Width);
                            }
                            if (character.IsDash) {
                                character.StopDash();
                            }
                            break;
                        case 2:
                            character.IsJump = false;
                            if (!character.IsDash) {
                                character.IsDrop = true;
                            }
                            character.ForceGravity = 0;
                            if (!character.IsDash) {
                                if (motionVertical) {
                                    character.CorrectY(ground.Y - character.HitBox.Height - 1);
                                }
                            }
                            break;
                        case 3:
                            StopMoveReight();
                            character.CanMoveReight = false;
                            character.CorrectX(ground.X - character.HitBox.Width);
                            if (character.IsDash) {
                                character.StopDash();
                            }
                            break;
                    }
                } else {
                    if (!character.IsJump && !character.IsDash) {
                        character.IsDrop = true;
                    } else {
                        character.StopDrop();
                    }
                }
                foreach(Enemy enemy in enemies) {
                    if (enemy.HitBox.IntersectsWith(ground.HitBox)) {
                        int wayHitboxesInteract = character.GetWayHitboxesInteract(ground.HitBox);
                        switch (wayHitboxesInteract) {
                            case 0:
                                enemy.IsDrop = false;
                                enemy.IsStand = true;
                                enemy.CorrectY(ground.Y + ground.HitBox.Height - 1);
                                break;
                        }
                    } else {
                        enemy.IsDrop = true;
                    }
                }
            }
        }
        public void Update() {
            foreach (Enemy enemy in enemies) {
                enemy.IIUpdate(character);
                if (character.IsAttack) {
                    character.Attack(enemy);
                }
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
            if (visibleArea.X != 0 && character.X >= visibleArea.Width / 2) {
                motionHorizontal = false;
            }
            if (character.CanMoveLeft) {
                if (motionHorizontal) {
                    character.MoveLeft();
                } else {
                    character.DirectionGazeHorizontal = false;
                    int hS = character.HorizontalSpeed;
                    MovingInScene(hS, 0);
                    visibleArea.X += hS;
                }
                character.IsMoveLeft = true;
                character.IsStand = false;
            }
        }
        public void MoveReight() {
            if (character.X >= visibleArea.Width / 2) {
                motionHorizontal = false;
            }
            if (character.CanMoveReight) {
                if (motionHorizontal) {
                    character.MoveReight();
                } else {
                    character.DirectionGazeHorizontal = true;
                    int hS = -1 * character.HorizontalSpeed;
                    MovingInScene(hS, 0);
                    visibleArea.X += hS;
                }
                character.IsMoveReight = true;
                character.IsStand = false;
            }
        }
        public void DifferentMovement() {
            if (character.Y >= visibleArea.Height / 2) {
                motionVertical = false;
            } else{
                motionVertical = true;
            }
            if (character.X >= visibleArea.Width / 2) {
                motionHorizontal = false;
            } else {
                motionHorizontal = true;
            }
            if (!character.IsStand && character.IsDrop && !character.IsJump && motionVertical && !character.IsDash) {
                character.Drop(motionVertical);
            }
            foreach (Enemy enemy in enemies) {
                if (!enemy.IsStand && enemy.IsDrop) {
                    enemy.Drop(true);
                }
            }
            if (!character.IsStand && character.IsJump) {
                if (!character.Jump(motionVertical)) {
                    character.IsJump = false;
                    character.IsDrop = true;
                }
            }
            if (character.IsDash) {
                character.Dash(motionHorizontal);
            }
        }
        public void Jump() {
            if (!character.IsJump && character.CanJump) {
                character.StartJump(motionVertical);
            }
        }
        public void Dash() {
            if (!character.IsDash && character.CanDash) {
                character.StartDash();
            }  
        }
        public static void StopMoveLeft() {
            character.IsMoveLeft = false;   
        }
        public static void StopMoveReight() {
            character.IsMoveReight = false;
        }
        private void MovingInScene(double xSpeed, double ySpeed) {
            foreach (Ground ground in grounds) {
                ground.MovingInScene(xSpeed, ySpeed);
            }
            foreach (Enemy enemy in enemies) {
                enemy.MovingInScene(xSpeed, ySpeed);
            }
        }
    }
}
