using System.Drawing;
using System.Collections.Generic;
using Night_Warrior.Entitys;
using System.Diagnostics;

namespace Night_Warrior.TheScene {
    class TestLevel: Scene {
        private static TestLevel teastLevel;
        private static StaticEntity sword;
        private TestLevel() {}
        public static void CreateTestLevel() {
            teastLevel = new TestLevel();
            teastLevel.LevelRestart();
        }
        public static TestLevel GetTestLevel() {
            return teastLevel;
        }
        public override void LevelRestart() {
            teastLevel.levelSize = new Size(2880, 2160);
            teastLevel.visibleArea = new Rectangle(0, 0, 1920, 1080);
            teastLevel.visibleAreaY = teastLevel.visibleArea.Y;
            teastLevel.grounds = new List<Ground> {
                new Ground(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(2880, 40), new Rectangle(0, 100, 2880, 40), new Rectangle(0, 0, 2880, 40)),
                new Ground(200, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
                new Ground(400, 400, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
                new Ground(600, 600, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
                new Ground(800, 800, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
                new Ground(2000, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
                new Ground(1000, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\ground.png", new Size(180, 100), new Rectangle(0, 0, 180, 80), new Rectangle(0, 0, 180, 20)),
            };
            teastLevel.background = new StaticEntity(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\night_city.png", new Size(1920, 1080), new Rectangle(0, 0, 1920, 1080));
            teastLevel.enemies = new List<Enemy> {
                new Enemy(1000, 29, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\enemy.png", new Size(100, 160), new Rectangle(0, 0, 100, 160), 4, 4, 1, 0),
            };
            character = new Character(0, 100, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 120), new Rectangle(0, 0, 60, 120), 10, 10);
            sword = new StaticEntity(600, 600, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\dead_enemy.png", new Size(160, 80), new Rectangle(0, 0, 160, 80), true);
        }
        public override void MovingInScene(double xSpeed, double ySpeed) {
            base.MovingInScene(xSpeed, ySpeed);
            sword.MovingInScene(xSpeed, ySpeed);
        }
        public override void PaintInsides() {
            sword.Draw();
            base.PaintInsides();
        }
        public override void SetGrafics(Graphics g) {
            base.SetGrafics(g);
            sword.Graphics = g;
        }
        public static void InteractionWithDeadEnemy() {
            if (character.HitBox.IntersectsWith(sword.Size) && sword.IsActive) {
                character.HaveSword = true;
                sword.IsActive = false;
                sword.Region = new Rectangle(160, sword.Region.Y, sword.Region.Width, sword.Region.Height);
            }
        }
    }
}
