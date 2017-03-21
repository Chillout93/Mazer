using System;
using System.IO;
using System.Linq;

namespace Mazer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Maze: ");
            var fileName = Console.ReadLine();

            var mazeInput = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\..\\..\\..\\ListOfMazes\\{fileName}")
                .ToArray()
                .Select(x => x.Split(' ').Select(y => int.Parse(y)).ToArray())
                .ToArray();

            var start = new Point(mazeInput[1][0], mazeInput[1][1]);
            var end = new Point(mazeInput[2][0], mazeInput[2][1]);
            var maze = mazeInput.Skip(3).TakeWhile(x => x != null).ToArray();
            
            var mazeSolver = new MazeSolver(start, end, maze);
            
            Console.Write(mazeSolver.PrintResult());
            Console.Read();
        }
    }
}
