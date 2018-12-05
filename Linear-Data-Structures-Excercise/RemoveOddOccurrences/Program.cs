using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoveOddOccurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split(' ')
               .Where(x => x.Length > 0)
               .Select(int.Parse)
               .ToList();
            var listOccurences = new Dictionary<int, int>();

            foreach (var item in list)
            {
                if (!listOccurences.ContainsKey(item))
                {
                    listOccurences[item] = 0;
                }
                listOccurences[item] ++ ;
            }

            foreach (var item in list)
            {
                if (listOccurences[item]%2==0)
                {
                    Console.Write($"{item} ");
                }
            }
            Console.WriteLine();


            //foreach (var item in listOccurences)
            //{
            //    if (item.Value%2==0)
            //    {
            //        Console.WriteLine(string.Concat(Enumerable.Repeat(item.Key+" ", item.Value)));
            //    }
            //}
        }
    }
}
