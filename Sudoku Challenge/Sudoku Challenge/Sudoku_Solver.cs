using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sudoku_Challenge
{
    class Sudoku_Solver
    {
        private char[,] sudokuPuzzle = new char[9,9];
        private string unSolvedPath;
        private string solvedPath;
        private string fileName;
        private char[] integerCharArr = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        
        //Sudoku construtor argument are path to unsolved sudoku problems
        //and path to where to store solved sudoku problems
        //fileName is stored for creation of solved sudoku file name
        //in {0}.sln.txt format being that {0} is the fileName
        public Sudoku_Solver(string unSolvedPath, string solvedPath)
        {
            this.unSolvedPath = unSolvedPath;
            this.solvedPath = solvedPath;
            fileName = Path.GetFileNameWithoutExtension(unSolvedPath);

            if(!Directory.Exists(this.solvedPath))
            {
                Directory.CreateDirectory(this.solvedPath);
            }

            //read in the Sudoku file and store values into multidimensional Array
            using (StreamReader sr = File.OpenText(this.unSolvedPath))
            {
                string line;
                int row = 0;
                while((line = sr.ReadLine()) != null)
                {
                    char[] charArr = line.ToCharArray();
                    for(int col = 0; col < charArr.Length; col++)
                    {
                        sudokuPuzzle[row, col] = charArr[col];
                    }
                    row++;
                }
            }
        }

        //public method to start the process
        public bool runSolver()
        {
            bool flag = solver(sudokuPuzzle);
            writeSolvedPuzzleToFile();
            return flag;
        }

        //recursive method to check if the character we are adding to the Sudoku
        //problem is correct. interate though the multidimensional Array
        //once a 'X' is found we go though are integerCharArry of numbers from 1 to 9
        //SudokuRuleChecker is called that checks if this number in that given row,
        //col or 3X3 already exists if so move on to another number. 
        //if not found the number is added to that cell in the array.
        //if by the end if all numbers are check and a number can not be found the process will 
        //mark that cell as a X and move back to try a different set of number.
        private bool solver(char[,] sudokuPuzzle)
        {
            for(int row = 0; row < sudokuPuzzle.GetLength(0); row++)
            {
                for(int col = 0; col < sudokuPuzzle.GetLength(1); col++)
                {
                    if(sudokuPuzzle[row,col].Equals('X'))
                    {
                        foreach(char integerChar in integerCharArr)
                        {
                            if(SudokuRuleChecker(sudokuPuzzle, row, col, integerChar))
                            {
                                sudokuPuzzle[row, col] = integerChar;

                                if(solver(sudokuPuzzle))
                                {
                                    return true;
                                }
                                else
                                {
                                    sudokuPuzzle[row, col] = 'X';
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        //checks the row, col and 3X3 if existing integer exists if so 
        //then we return false because the integer can not be used.
        //and most look to use a different one.
        private bool SudokuRuleChecker(char[,] sudokuPuzzle, int row, int col, char integerChar)
        {
            for (int index = 0; index < 9; index++)
            {
                if (sudokuPuzzle[row, index].Equals(integerChar) ||
                    sudokuPuzzle[index, col].Equals(integerChar) ||
                    (sudokuPuzzle[3 * (row / 3) + index / 3, 3 * (col / 3) + index % 3] != 'X'
                    && sudokuPuzzle[3 * (row / 3) + index / 3, 3 * (col / 3) + index % 3] == integerChar))
                {
                    return false;
                }
            }
            return true;
        }

        //method to write solved sudoku to text file
        private void writeSolvedPuzzleToFile()
        {
            using (StreamWriter wr = new StreamWriter(Path.Combine(solvedPath, fileName + ".sln.txt")))
            {
                for (int row = 0; row < sudokuPuzzle.GetLength(0); row++)
                {
                    for (int col = 0; col < sudokuPuzzle.GetLength(1); col++)
                    {
                        wr.Write(sudokuPuzzle[row, col]);
                    }
                    wr.WriteLine();
                }
            }
        }
    }
}
