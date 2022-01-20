using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Night_Warrior.Entitys {
    class Character: Unit {
        private const double MaxHorizontalSpeed = 22;
        private const int MaxJumpSpeed = -18;
        private const double DashAcceleration = 2;
        private const int MaxHp = 5;
        private const int MaxFramesInvulnerability = 50;

        private bool isJump = false;
        private bool isInvulnerability = false;
        private bool isDash = false;

        private bool canJump = true;
        private bool canDash = true;
        private bool canCanDash = false;

        private double dashSpeed = 0;
        private double verticalSpeed = 0;
        private bool dashDirection;
        private bool decreaseSpeed = false;
        private bool correctY = false;
        private bool correctXR = false;
        private bool correctXL = true;
        private int framesInvulnerability;
        private List<StaticEntity> healthContainers;
        private StaticEntity mannaContainer;
        private StaticEntity impactwEffects;
        public bool IsInvulnerability {
            get {
                return isInvulnerability;
            }
            set {
                isInvulnerability = value;
            }
        }
        public bool CanCanDash {
            set {
                canCanDash = value;
            }
        }
        public bool IsJump {
            get {
                return isJump;
            }
            set {
                isJump = value;
            }
        }
        public bool CanJump {
            get {
                return canJump;
            }
            set {
                canJump = value;
            }
        }
        public bool IsMoveLeft {
            get {
                return isMoveLeft;
            }
            set {
                isMoveLeft = value;
            }
        }
        public bool IsMoveReight {
            get {
                return isMoveReight;
            }
            set {
                isMoveReight = value;
            }
        }
        public double VerticalSpeed {
            get {
                return verticalSpeed;
            }
        }
        public bool IsDash {
            get {
                return isDash;
            }
            set {
                isDash = value;
            }
        }
        public bool CanDash {
            get {
                return canDash;
            }
            set {
                canDash = value;
            }
        }
        public double DashSpeed {
            get {
                return dashSpeed;
            }
        }
        public Character(int x, int y, string imagePath, Size size, Rectangle region, int hS, int hitboxOffset) : base(x, y, imagePath, size, region, hS, hitboxOffset) {
            hp = MaxHp;
            healthContainers = new List<StaticEntity> {
                new StaticEntity(170, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 60), new Rectangle(0, 860, 60, 60), true),
                new StaticEntity(250, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 60), new Rectangle(0, 860, 60, 60), true),
                new StaticEntity(330, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 60), new Rectangle(0, 860, 60, 60), true),
                new StaticEntity(410, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 60), new Rectangle(0, 860, 60, 60), true),
                new StaticEntity(490, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 60), new Rectangle(0, 860, 60, 60), true),
            };
            mannaContainer = new StaticEntity(20, 920, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(140, 140), new Rectangle(0, 940, 140, 140));
            impactwEffects = new StaticEntity(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(140, 120), new Rectangle(0, 240, 140, 120));
            damage = 1;
            MaxFramesAttack = 5;
        }
        protected override void IsMoveHorizontal() {
            if (isMoveLeft || isMoveReight || isDash) {
                isMoveHorizontal = true;
                return;
            }
            isMoveHorizontal = false;
        }
        public void DelayDash() {
            if (canCanDash) {
                canDash = true;
                canCanDash = false;
            }
        }
        private void IsMoveVertical() {
            if (!isStand && (isDrop || isJump)) {
                isMoveVertical = true;
                return;
            }
            isMoveVertical = false;
        }
        protected override void RenderingAnimations() {
            IsMoveHorizontal();
            IsMoveVertical();
            int state = 0;
            if (!isMoveHorizontal && !isMoveVertical) {
                state = 0;
            } else {
                if (isMoveHorizontal) {
                    if (isMoveLeft) {
                        state = 1;
                    }
                    if (isMoveReight) {
                        state = 2;
                    }
                    if (isDash) {
                        if (directionGazeHorizontal) {
                            state = 4;
                        } else {
                            state = 3;
                        }
                    }
                } else {
                    if (isMoveVertical) {
                        state = 5;
                    }
                }

            }
            if (state != 4 && state != 3) {
                region.Y = 0;
                region.Width = 60;
                region.Height = 120;
                SetHitBox(0, 0, new Size(60, 120));
                size.Width = 60;
                size.Height = 120;
                if (correctY) {
                    CorrectY(Y + 20);
                    correctY = false;
                }
            }
            if (isDash) {
                region.Y = 20;
                if (!correctY) {
                    CorrectY(Y - 20);
                }
                region.Width = 80;
                region.Height = 100;
                SetHitBox(0, 0, new Size(80, 100));
                size.Width = 80;
                size.Height = 100;
                correctY = true;
            }
            switch (state) {
                case 0:
                    if (directionGazeHorizontal) {
                        region.X = 0;
                        region.Height = 120;
                        SetHitBox(0, 0, new Size(60, 120));
                    } else {
                        region.X = 60;
                        region.Height = 120;
                        SetHitBox(0, 0, new Size(60, 120));
                    }
                    break;
                case 1:
                    region.X = 180;
                    region.Height = 120;
                    SetHitBox(0, 0, new Size(55, 120));
                    break;
                case 2:
                    region.X = 120;
                    region.Height = 120;
                    SetHitBox(0, 0, new Size(55, 120));
                    break;
                case 3:
                    region.X = 320;
                    break;
                case 4:
                    region.X = 240;
                    break;
                case 5:
                    if (directionGazeHorizontal) {
                        region.X = 400;
                    } else {
                        region.X = 460;
                    }
                    break;
            }
            if (framesAttack > 0) {
                framesAttack--;
                if (framesAttack > MaxFramesAttack / 2) {
                    if (directionGazeHorizontal) {
                        region.X = 520;
                        region.Width = 120;
                        size.Width = 120;
                        if (!correctXR) {
                            CorrectX(X - 40);
                            correctXR = true;
                        }
                        SetHitBox(55, 0, new Size(60, 120));
                    } else {
                        region.X = 640;
                        region.Width = 120;
                        size.Width = 120;
                        SetHitBox(5, 0, new Size(60, 120));
                    }
                } else {
                    impactwEffects.ChangeImageForImpactWEffects(directionGazeHorizontal, 140);
                    if (directionGazeHorizontal) {
                        region.X = 760;
                        region.Width = 120;
                        size.Width = 120;
                        if (correctXR) {
                            CorrectX(X + 40);
                            correctXR = false;
                        }
                        SetHitBox(15, 0, new Size(60, 120));
                        impactwEffects.SetXY(X + size.Width + 60 - impactwEffects.Size.Width, Y + size.Height - 10);
                    } else {
                        region.X = 880;
                        region.Width = 120;
                        size.Width = 120;
                        if (correctXL) {
                            CorrectX(X - 55);
                            correctXL = false;
                        }
                        SetHitBox(55, 0, new Size(60, 120));
                        impactwEffects.SetXY(X + size.Width - 40 - impactwEffects.Size.Width, Y + size.Height - 10);
                    }
                }
            } else {
                isAttack = false;
                if (!correctXL) {
                    CorrectX(X + 55);
                    correctXL = true;
                }
            }
            if (framesInvulnerability > 0) {
                framesInvulnerability--;
                isInvulnerability = true;
                if (framesInvulnerability % 5 == 0) {
                    region.Y += 120;
                }
            } else {
                isInvulnerability = false;
            }
            UpdatePoints();
        }
        public override void Draw() {
            RenderingAnimations();
            graphics.DrawImage(image, new Rectangle((int)x, (int)y, size.Width, size.Height), region, GraphicsUnit.Pixel);

            /*
            raphics.FillRectangle(new SolidBrush(Color.White), hitBox);
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(A, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(B, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(C, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(D, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(AB, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(BC, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(CD, new Size(2, 2)));
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(DA, new Size(2, 2)));
            */
            for(int i = 0; i < hp; i++) {
                healthContainers[i].IsActive = true;
            }
            for(int i = MaxHp-1; i >= hp; i--) {
                healthContainers[i].IsActive = false;
            }
            //Attack();
            if (isAttack) {
                if (framesAttack < MaxFramesAttack / 2) {
                    impactwEffects.Graphics = graphics;
                    impactwEffects.Draw();
                }
            }
            foreach(StaticEntity healthContainer in healthContainers) {
                healthContainer.Graphics = graphics;
                healthContainer.ChangeImageForHp();
                healthContainer.Draw();
            }
            mannaContainer.Graphics = graphics;
            mannaContainer.Draw();
            /*
            for (double y = bL; y < bU; y++) {
                double a = 0.05;
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
        public void StartJump(bool motionVertical) {
            verticalSpeed = MaxJumpSpeed;
            if (motionVertical) {
                y -= 1;
                SetHitBox(0, 0, Size);
            }
            isJump = true;
            isDrop = false;
            canJump = false;
            isStand = false;
            isMoveVertical = true;
        }
        public bool Jump(bool motionVertical) {
            isMoveVertical = true;
            if (motionVertical) {
                y += verticalSpeed;
                SetHitBox(0, 0, Size);
            }
            verticalSpeed += G;
            if(verticalSpeed >= MaxForceGravity) {
                verticalSpeed = 0;
                forceGravity = MaxForceGravity;
                return false;
            }
            return true;
        }
        public void Dash(bool motionHorizontal) {
            isMoveHorizontal = true;
            if(dashSpeed <= MaxHorizontalSpeed && !decreaseSpeed) {
                dashSpeed += DashAcceleration;
            } else {
                dashSpeed -= 2 * DashAcceleration;
                decreaseSpeed = true;
                if(dashSpeed <= 0) {
                    StopDash();
                    return;
                }
            }
            if (motionHorizontal) {
                if (dashDirection) {
                    x += dashSpeed;
                } else {
                    x -= dashSpeed;
                }
                SetHitBox(0, 0, Size);
            }
        }
        public void StartDash() {
            isStand = false;
            isDash = true;
            decreaseSpeed = false;
            canMoveLeft = false;
            canMoveReight = false;
            canJump = false;
            canDash = false;
            isDrop = false;
            dashDirection = directionGazeHorizontal;
            if (dashDirection) {
                isMoveReight = true;
            } else {
                isMoveLeft = true;
            }
            isMoveHorizontal = true;
            verticalSpeed = 0;
        }
        public void StopDash() {
            isStand = false;
            isDash = false;
            dashSpeed = 0;
            canMoveLeft = true;
            canMoveReight = true;
            isMoveReight = false;
            isMoveLeft = false;
            isMoveHorizontal = false;
        }
        public void StopDrop() {
            if (isDrop) {
                verticalSpeed = 0;
            }
            isDrop = false;
        }
        public void CorrectX(double x) {
            this.x = x;
            SetHitBox(0, 0, Size);
        }
        public override int GetWayHitboxesInteract(Rectangle groundHitbox) {
            int result = base.GetWayHitboxesInteract(groundHitbox);
            if (result != -1) {
                return result;
            }
            if (IsPointInHitbox(groundHitbox, A)) {
                if (isJump) {
                    //Debug.WriteLine("AJ");
                    return 2;
                }
                if (isMoveLeft || (isDash && !directionGazeHorizontal)) {
                    //Debug.WriteLine("AL");
                    return 1;
                }
            }
            if (IsPointInHitbox(groundHitbox, B)) {
                if (isJump) {
                    //Debug.WriteLine("BJ");
                    return 2;
                }
                if (isMoveReight || (isDash && directionGazeHorizontal)) {
                    //Debug.WriteLine("BR");
                    return 3;
                }
            }
            if (IsPointInHitbox(groundHitbox, C)) {
                if (isMoveReight || (isDash && directionGazeHorizontal)) {
                    if(groundHitbox.Y >= C.Y - 10 && groundHitbox.X + 2 <= C.X) {
                        //Debug.WriteLine("CD");
                        return 0;
                    } else {
                        //Debug.WriteLine("CR");
                        return 3;
                    }
                }
                if (!isDash && (isDrop || (isJump && verticalSpeed > 0))) {
                    if (groundHitbox.Y >= C.Y - 10 && groundHitbox.X + 2 <= C.X) {
                        //Debug.WriteLine("CD");
                        return 0;
                    }
                }
            }
            if (IsPointInHitbox(groundHitbox, D)) {
                if (isMoveLeft || (isDash && !directionGazeHorizontal)) {
                    if (groundHitbox.Y >= C.Y - 10 && groundHitbox.X + groundHitbox.Width - 2 <= C.X) {
                        //Debug.WriteLine("DD");
                        return 0;
                    }
                    //Debug.WriteLine("DL");
                    return 1;
                }
                if (isDrop || (isJump && verticalSpeed > 0)) {
                    if (groundHitbox.Y >= C.Y - 10 && groundHitbox.X + groundHitbox.Width - 2 <= C.X) {
                        //Debug.WriteLine("DD");
                        return 0;
                    }
                }
            }
            if (IsPointInHitbox(groundHitbox, AB)) {
                if (isJump) {
                    //Debug.WriteLine("ABJ");
                    return 2;
                }
            }
            if (IsPointInHitbox(groundHitbox, BC)) {
                if (isMoveReight || (isDash && directionGazeHorizontal)) {
                    //Debug.WriteLine("BCR");
                    return 3;
                }
            }
            if (IsPointInHitbox(groundHitbox, CD)) {
                if (isDrop || (isJump && verticalSpeed > 0)) {
                    //Debug.WriteLine("CDD");
                    return 0;
                }
            }
            if (IsPointInHitbox(groundHitbox, DA)) {
                if (isMoveLeft || (isDash && !directionGazeHorizontal)) {
                    //Debug.WriteLine("DAD");
                    return 1;
                }
            }
            if (!isDash && groundHitbox.X + groundHitbox.Height / 2 <= GetCenterHitbox().X) {
                //Debug.WriteLine("SR");
                return 1;
            }
            if (!isDash && groundHitbox.X + groundHitbox.Height / 2 >= GetCenterHitbox().X) {
                //Debug.WriteLine("SL");
                return 3;
            }
            return 0;
        }
        public override void TakingDamage(int damage) {
            base.TakingDamage(damage);
            framesInvulnerability = MaxFramesInvulnerability;
        }
    }
}
