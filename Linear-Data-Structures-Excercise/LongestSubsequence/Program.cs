using System;
using System.Linq;

namespace LongestSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split(' ')
                .Where(x => x.Length > 0)
                .Select(int.Parse)
                .ToList();

            int number=list[0];
            int maxOccurencies = 1;
            int currentNumber = list[0];
            int currentOccurencies = 1;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i]==list[i-1])
                {
                    currentOccurencies++;
                    if (currentOccurencies>maxOccurencies)
                    {
                        number = list[i];
                        maxOccurencies = currentOccurencies;
                    }                    
                }
                else
                {
                    currentOccurencies = 1;
                }
            }
            for (int i = 0; i < maxOccurencies; i++)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();

        }
    }
}
