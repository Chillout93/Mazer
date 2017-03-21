namespace Mazer
{
    public class Point
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Point(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override bool Equals(object obj)
        {
            var point = obj as Point;

            return (point != null) ? (point.Height == this.Height && point.Width == this.Width) : false;
        }

        public override int GetHashCode()
        {
            return this.Width.GetHashCode() + this.Height.GetHashCode();
        }
    }
}
