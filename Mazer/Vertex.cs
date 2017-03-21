namespace Mazer
{
    public class Vertex
    {
        public int Distance { get; set; }
        public Vertex Previous { get; set; }
        public Point Point { get; set; }

        public Vertex(Point point, int distance, Vertex previous)
        {
            this.Distance = distance;
            this.Previous = previous;
            this.Point = point;
        }
    }
}
