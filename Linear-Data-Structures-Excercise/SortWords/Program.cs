using System;
using System.Linq;

namespace SortWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine()
                .Split()
                .Where(x => x.Length > 0)
                .ToList();

            list
                .OrderBy(x => x)
                .ToList()
                .ForEach(x => Console.Write($"{x} "));

            Console.WriteLine();
        }
    }
}
