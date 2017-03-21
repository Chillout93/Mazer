using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mazer
{
    public class MazeSolver
    {
        private Point start;
        private Point end;
        private int[][] maze;
        private int mazeHeight;
        private int mazeWidth;
        public IList<Point> ShortestPath { get; private set; }

        public MazeSolver(Point start, Point end, int[][] maze)
        {
            this.start = start;
            this.end = end;
            this.maze = maze;
            this.mazeHeight = maze.Length;
            this.mazeWidth = maze[0].Length;
            this.ShortestPath = new List<Point>();

            SolveMaze();
        }

        // Dijkstra Algorithm
        private bool SolveMaze()
        {
            IList<Vertex> vertexes = GetListOfMovableMazeNodes();

            vertexes.Single(x => x.Point.Equals(start)).Distance = 0;

            while (vertexes.Count != 0)
            {
                // Find the next node closest to start.
                var leastDistance = vertexes.Aggregate((x1, x2) => x1.Distance < x2.Distance ? x1 : x2);

                // If the next node is the end point, we have finished.
                if (leastDistance.Point.Equals(end))
                {
                    while (leastDistance.Previous != null)
                    {
                        ShortestPath.Add(leastDistance.Point);
                        leastDistance = leastDistance.Previous;
                    }
                    return true;
                }

                // Otherwise find all neighbours and continue search.
                var neighbours = GetNeighbours(leastDistance, vertexes);
                foreach (var neighbour in neighbours)
                {
                    var possibleMinimumDistance = leastDistance.Distance + 1;
                    if (possibleMinimumDistance < neighbour.Distance)
                    {
                        neighbour.Distance = possibleMinimumDistance;
                        neighbour.Previous = leastDistance;
                    }
                }

                vertexes.Remove(leastDistance);
            }

            return false;
        }

        private IList<Vertex> GetNeighbours(Vertex currentPosition, IList<Vertex> neighboursToFind)
        {
            return neighboursToFind
                .Where(neighbour => (neighbour.Point.Height + 1 == currentPosition.Point.Height || neighbour.Point.Height - 1 == currentPosition.Point.Height) && neighbour.Point.Width == currentPosition.Point.Width
                                 || (neighbour.Point.Width + 1 == currentPosition.Point.Width || neighbour.Point.Width - 1 == currentPosition.Point.Width) && neighbour.Point.Height == currentPosition.Point.Height)
                .ToList();
        }

        private IList<Vertex> GetListOfMovableMazeNodes()
        {
            var vertexes = new List<Vertex>();
            for (int i = 0; i < mazeHeight; i++)
                for (int j = 0; j < mazeWidth; j++)
                    if (maze[i][j] == 0) vertexes.Add(new Vertex(new Point(j, i), int.MaxValue, null));

            return vertexes;
        }

        public string PrintResult()
        {
            if (ShortestPath.Count == 0)
                return "There is no valid path in this maze.";

            string[,] output = new string[mazeHeight, mazeWidth];

            // Print maze in string array.
            for (int i = 0; i < mazeHeight; i++)
                for (int j = 0; j < mazeWidth; j++)
                    output[i, j] = (maze[i][j] == 1) ? "#" : " ";

            // Overlay shortest path if available.
            foreach (var point in ShortestPath)
                output[point.Height, point.Width] = "X";

            // Overlay start and end positions.
            output[start.Height, start.Width] = "S";
            output[end.Height, end.Width] = "E";

            // Output string array as full string.
            var outputString = new StringBuilder();
            for (int i = 0; i < mazeHeight; i++)
            {
                for (int j = 0; j < mazeWidth; j++)
                    outputString.Append(output[i, j]);

                outputString.Append(Environment.NewLine);
            }

            return outputString.ToString();
        }
    }
}
