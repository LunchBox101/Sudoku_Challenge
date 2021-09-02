using System;
using System.Collections.Generic;

namespace Sudoku_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //stored Puzzle paths
            Dictionary<string, string> puzzles = new Dictionary<string, string>();
            puzzles.Add("1", @"Unsolved Puzzles/puzzle1.txt");
            puzzles.Add("2", @"Unsolved Puzzles/puzzle2.txt");
            puzzles.Add("3", @"Unsolved Puzzles/puzzle3.txt");
            puzzles.Add("4", @"Unsolved Puzzles/puzzle4.txt");
            puzzles.Add("5", @"Unsolved Puzzles/puzzle5.txt");
            
            string userEnteredData = "";
            //loop until user enters q to quit
            while(!userEnteredData.Equals("q"))
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Sudoku Challenge Enter 1 2 3 4 5");
                Console.WriteLine("Process will then solve givin puzzle and store it in the Solved Puzzle Folder");
                Console.WriteLine("enter q to quit");
                Console.Write("->");
                userEnteredData = Console.ReadLine();
                //if user enters correct input we can move forward
                //else display reminder for user to enter correct data
                if(puzzles.ContainsKey(userEnteredData))
                {
                    
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    Sudoku_Solver sudokuClass = new Sudoku_Solver(System.IO.Path.Combine(path, puzzles[userEnteredData]), System.IO.Path.Combine(path, "Solved Puzzles"));
                    if(sudokuClass.runSolver())
                    {
                        Console.WriteLine("The Process has finished Successfully");
                    }
                    else
                    {
                        Console.WriteLine("The Process was unable to solve the puzzle");
                    }
                }
                else if(!userEnteredData.Equals("q") && !puzzles.ContainsKey(userEnteredData))
                {
                    Console.WriteLine("The data you have entered was not correct/n please try again");
                    userEnteredData = "";
                }
            }
        }
    }
}
