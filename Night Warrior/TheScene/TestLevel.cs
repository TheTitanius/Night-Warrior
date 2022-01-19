using System.Drawing;
using System.Collections.Generic;
using Night_Warrior.Entitys;

namespace Night_Warrior.TheScene {
    class TestLevel: Scene {
        private static TestLevel teastLevel;
        private TestLevel() {}
        public static void CreateTestLevel() {
            teastLevel = new TestLevel();
            teastLevel.levelSize = new Size(3840, 2160);
            teastLevel.visibleArea = new Rectangle(0, 0, 1920, 1080);
            teastLevel.visibleAreaY = teastLevel.visibleArea.Y;
            teastLevel.grounds = new List<Ground> {
                new Ground(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(1920, 30), new Rectangle(0, 100, 1920, 30), new Rectangle(0, 0, 1920, 30)),
                new Ground(200, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(400, 400, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(600, 600, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(800, 800, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(2000, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                //new Ground(900, 35, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(30, 320), new Rectangle(0, 130, 30, 320), new Rectangle(0, 0, 30, 320)),
                new Ground(1000, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
            };
            teastLevel.background = new StaticEntity(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\night_city.png", new Size(1920, 1080), new Rectangle(0, 0, 1920, 1080));
            teastLevel.enemies = new List<Enemy> {
                new Enemy(1500, 29, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\enemy.png", new Size(100, 160), new Rectangle(0, 0, 100, 160), 4, 5, 1, 0),
            };
            character = new Character(0, 100, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\character.png", new Size(60, 120), new Rectangle(0, 0, 60, 120), 10, 10);
        }
        public static TestLevel GetTestLevel() {
            return teastLevel;
        }
    }

}
