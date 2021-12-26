using System.Drawing;
using System.Collections.Generic;
using Night_Warrior.Entitys;

namespace Night_Warrior.TheScene {
    class TestLevel: Scene {
        private static TestLevel teastLevel;
        private TestLevel() {}
        public static void CreateTestLevel() {
            teastLevel = new TestLevel();
            teastLevel.levelSize = new Size(3840, 1080);
            teastLevel.visibleArea = new Rectangle(0, 0, 1920, 1080);
            teastLevel.grounds = new List<Ground> {
                new Ground(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(1920, 30), new Rectangle(0, 100, 1920, 30), new Rectangle(0, 0, 1920, 30)),
                new Ground(200, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(400, 400, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
                new Ground(2000, 200, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_ground.png", new Size(190, 100), new Rectangle(0, 0, 190, 100), new Rectangle(0, 0, 190, 19)),
            };
            teastLevel.background = new Background(0, 0, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\night_city.png", new Size(1920, 1080), new Rectangle(0, 0, 1920, 1080));
            character = new Character(0, 1000, @"D:\Projects\C#\CourseWork\Night Warrior\Night Warrior\res\test_character.png", new Size(60, 100), new Rectangle(0, 0, 60, 100),  8, ForceGravity, G);
        }
        public static TestLevel GetTestLevel() {
            return teastLevel;
        }
    }

}
