using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class StaticEntity: Entity {
        protected Point point;
        public Point Point {
            get {
                return point;
            }
            set {
                point = value;
            }
        }
        public StaticEntity(int x, int y, string imagePath, Size size, Graphics graphics) : base(x, y, imagePath, size, graphics) {}
        public StaticEntity(int x, int y, string imagePath, Size size) : base(x, y,  imagePath, size) {
            this.size = size;
        }
        public StaticEntity(int x, int y, string imagePath, Size size, Graphics graphics, Point point): base(x, y, imagePath, size, graphics) {
            this.point = point;
            this.size = size;
        }
        public StaticEntity(int x, int y, string imagePath, Size size, Rectangle region) : base(x, y, imagePath, size, region) {
            this.size = size;
        }

    }
}