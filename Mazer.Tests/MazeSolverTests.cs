using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Mazer.Tests
{
    [TestClass]
    public class MazeSolverTests
    {
        private static readonly string mazeDirectory = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\ListOfMazes\\";

        [TestMethod]
        public void MazeSolver_WithInput_PrintsResultCorrectly()
        {
            var mazeInput = GetMazeInput("input.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            var expectedMaze = 
                "#####" + Environment.NewLine + 
                "#S# #" + Environment.NewLine + 
                "#X# #" + Environment.NewLine + 
                "#XXE#" + Environment.NewLine + 
                "#####" + Environment.NewLine;

            Assert.AreEqual(expectedMaze, mazeSolver.PrintResult());
        }

        [TestMethod]
        public void MazeSolver_WithSmallInput_PrintsResultCorrectly()
        {
            var mazeInput = GetMazeInput("small.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            var expectedMaze =
                "#####" + Environment.NewLine +
                "#SXX#" + Environment.NewLine +
                "# #X#" + Environment.NewLine +
                "# #X#" + Environment.NewLine +
                "# #E#" + Environment.NewLine +
                "#####" + Environment.NewLine;

            Assert.AreEqual(expectedMaze, mazeSolver.PrintResult());
        }

        [TestMethod]
        public void MazeSolver_WithMediumInput_PrintsResultCorrectly()
        {
            var mazeInput = GetMazeInput("medium_input.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            var expectedMaze =
                "#######################" + Environment.NewLine +
                "#S#   #           # # #" + Environment.NewLine +
                "#X# # # ####### # # # #" + Environment.NewLine +
                "#X# # #       # # # # #" + Environment.NewLine +
                "#X# # ######### # # # #" + Environment.NewLine +
                "#X# # #XXXXXXX# #   # #" + Environment.NewLine +
                "#X# # #X#####X# ##### #" + Environment.NewLine +
                "#X# # #XXXXX#X# #   # #" + Environment.NewLine +
                "#X### ### #X#X# # # # #" + Environment.NewLine +
                "#XXX  #   #X#X# # # # #" + Environment.NewLine +
                "###X#######X#X# ### # #" + Environment.NewLine +
                "#  XXX#XXXXX#X#   # # #" + Environment.NewLine +
                "#####X#X#####X### #XXX#" + Environment.NewLine +
                "#   #XXX  #  XXX# #X#X#" + Environment.NewLine +
                "# # ##### ### #X# #X#X#" + Environment.NewLine +
                "# #     # #   #X  #X#X#" + Environment.NewLine +
                "# ##### # # ###X###X#X#" + Environment.NewLine +
                "# #   # # #   #X#XXX#X#" + Environment.NewLine +
                "# # # # ##### #X#X# #X#" + Environment.NewLine +
                "#   # #       #XXX# #E#" + Environment.NewLine +
                "#######################" + Environment.NewLine;

            Assert.AreEqual(expectedMaze, mazeSolver.PrintResult());
        }

        [TestMethod]
        public void MazeSolver_WithSparseMedium_PrintsResultCorrectly()
        {
            var mazeInput = GetMazeInput("sparse_medium.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            var expectedMaze =
                "#####################" + Environment.NewLine +
                "#S                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#X                  #" + Environment.NewLine +
                "#XXXXXXXXXXXXXXXXXXE#" + Environment.NewLine +
                "#####################" + Environment.NewLine;

            Assert.AreEqual(expectedMaze, mazeSolver.PrintResult());
        }

        [TestMethod]
        public void MazeSolver_WithLargeInput_DoesNotReturnError()
        {
            var mazeInput = GetMazeInput("large_input.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            Assert.AreNotEqual(0, mazeSolver.ShortestPath);
        }

        [TestMethod]
        public void MazeSolver_WithIncorrectMaze_ReturnsErrorMessage()
        {
            var mazeInput = GetMazeInput("incorrect_input.txt");
            var mazeSolver = ParseMazeInput(mazeInput);

            Assert.AreEqual("There is no valid path in this maze.", mazeSolver.PrintResult());
            Assert.AreEqual(0, mazeSolver.ShortestPath.Count);
        }

        private int[][] GetMazeInput(string fileName)
        {
            return File.ReadAllLines($"{mazeDirectory}{fileName}")
                .ToArray()
                .Select(x => x.Split(' ').Select(y => int.Parse(y)).ToArray())
                .ToArray();
        }

        private MazeSolver ParseMazeInput(int[][] mazeInput)
        {
            var start = new Point(mazeInput[1][0], mazeInput[1][1]);
            var end = new Point(mazeInput[2][0], mazeInput[2][1]);
            var maze = mazeInput.Skip(3).TakeWhile(x => x != null).ToArray();

            return new MazeSolver(start, end, maze);
        }
    }
}
