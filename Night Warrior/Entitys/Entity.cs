using System.Drawing;

namespace Night_Warrior.Entitys {
    abstract class Entity {
        protected double x;
        public double X {
            get {
                return x;
            }
        }
        protected double y;
        public double Y {
            get {
                return 1080 - y - size.Height;
            }
        }
        protected Image image;
        protected Graphics graphics;
        protected Rectangle region;
        protected Size size;
        public Size Size {
            get {
                return size;
            }
            set {
                size = value;
            }
        }
        public Image Image {
            get {
                return image;
            }
            set {
                image = value;
            }
        }
        public Graphics Graphics {
            get {
                return graphics;
            }
            set {
                graphics = value;
            }
        }
        public Rectangle Region {
            get {
                return region;
            }
            set {
                region = value;
            }
        }
        protected Entity(int x, int y, string imagePath, Size size,Graphics graphics) {
            this.x = x;
            this.y = 1080 - y - size.Height;
            image = Image.FromFile(imagePath);
            this.graphics = graphics;
            this.size = size;
        }
        protected Entity(int x, int y, string imagePath, Size size) {
            this.x = x;
            this.y = 1080 - y - size.Height;
            image = Image.FromFile(imagePath);
            this.size = size;
        }
        protected Entity(int x, int y, string imagePath, Size size, Rectangle region) {
            this.x = x;
            this.y = 1080 - y - size.Height;
            image = Image.FromFile(imagePath);
            this.region = region;
            this.size = size;
        }
        public abstract void Draw();
    }
}