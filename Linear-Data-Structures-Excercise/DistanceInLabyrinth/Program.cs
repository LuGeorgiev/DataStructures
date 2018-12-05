using System;
using System.Collections.Generic;

namespace DistanceInLabyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixSize = int.Parse(Console.ReadLine());
            var matrix = new string[matrixSize, matrixSize];

            matrix = PopulateMatrix(matrix);
            // index format row,col
            string startingPosition = FindStart(matrix);
            matrix = PopulateCosts(matrix, startingPosition);

            PrintMatrix(matrix);
        }

        private static string[,] PopulateCosts(string[,] matrix, string startingPosition)
        {
            int maxIndex = matrix.GetLength(0) - 1;
            var queue = new Queue<string>();
            queue.Enqueue(startingPosition);
            while (queue.Count>0)
            {
                var indexes = queue.Dequeue().Split(',');
                int row = int.Parse(indexes[0]);
                int col = int.Parse(indexes[1]);
                string currentValue = matrix[row, col];

                //Up Direction
                if (row-1>=0 &&matrix[row-1,col]=="0")
                {
                    if (currentValue=="*")
                    {
                        matrix[row - 1, col] = "1";
                    }
                    else
                    {
                        int currentLength = int.Parse(matrix[row, col]);
                        matrix[row - 1, col] = (currentLength+1).ToString();
                    }
                    queue.Enqueue($"{row - 1},{col}");
                }

                //DownDirection
                if (row + 1 <= maxIndex && matrix[row + 1, col] == "0")
                {
                    if (currentValue == "*")
                    {
                        matrix[row + 1, col] = "1";
                    }
                    else
                    {
                        int currentLength = int.Parse(matrix[row, col]);
                        matrix[row + 1, col] = (currentLength + 1).ToString();
                    }
                    queue.Enqueue($"{row + 1},{col}");
                }

                //RightDirection
                if (col + 1 <= maxIndex && matrix[row, col+1] == "0")
                {
                    if (currentValue == "*")
                    {
                        matrix[row, col + 1] = "1";
                    }
                    else
                    {
                        int currentLength = int.Parse(matrix[row, col]);
                        matrix[row , col + 1] = (currentLength + 1).ToString();
                    }
                    queue.Enqueue($"{row},{col + 1}");
                }

                //LeftDirection
                if (col - 1 >=0 && matrix[row, col - 1] == "0")
                {
                    if (currentValue == "*")
                    {
                        matrix[row, col - 1] = "1";
                    }
                    else
                    {
                        int currentLength = int.Parse(matrix[row, col]);
                        matrix[row, col - 1] = (currentLength + 1).ToString();
                    }
                    queue.Enqueue($"{row},{col - 1}");
                }
            }

            return matrix;
        }

        private static string FindStart(string[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i, j] == "*")
                    {
                        return $"{i},{j}";
                    }
                }
            }
            return null;
        }

        private static void PrintMatrix(string[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i, j]=="0")
                    {
                        matrix[i, j] = "u";
                    }
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static string[,] PopulateMatrix(string[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                var input = Console.ReadLine().ToCharArray();
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = input[j].ToString();
                }
            }
            return matrix;
        }
    }
}
