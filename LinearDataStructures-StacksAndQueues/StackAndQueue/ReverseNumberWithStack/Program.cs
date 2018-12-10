using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_ReverseNumberWithStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<int>();

            Console.ReadLine()
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(int.Parse)
                .ToList()
                .ForEach(x => stack.Push(x));

            while (stack.Count!=0)
            {
                Console.Write(stack.Pop()+" ");
            }
        }
    }
}
