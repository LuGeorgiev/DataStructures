using System;
using System.Linq;

namespace SumAndAverage
{
    //Write a program that reads from the console a sequence of integer numbers(on a single line, separated by a space). Calculate and print the sum and average of the elements of the sequence.Keep the sequence in List<int>.Round the average to second digit after the decimal separator

    class Program
    {
        static void Main()
        {
            var list = Console.ReadLine().Split(' ')
                .Where(x => x.Length > 0)
                .Select(x => int.Parse(x))
                .ToList();

            if (list.Count>0)
            {
                Console.WriteLine($"Sum={list.Sum()}; Average={list.Average().ToString("f2")}");
            }
            else
            {
                Console.WriteLine("Sum=0; Average=0.00");
            }
        }
    }
}
