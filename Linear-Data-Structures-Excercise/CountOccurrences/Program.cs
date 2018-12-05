using System;
using System.Collections.Generic;
using System.Linq;

namespace CountOccurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split(' ')
               .Where(x => x.Length > 0)
               .Select(int.Parse)
               .ToList();
            var listOccurences = new SortedDictionary<int, int>();

            foreach (var item in list)
            {
                if (!listOccurences.ContainsKey(item))
                {
                    listOccurences[item] = 0;
                }
                listOccurences[item]++;
            }

            foreach (var kvp in listOccurences)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value} times");
            }
        }
    }
}
