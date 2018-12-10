using P03_ArrayBased_Stack;
using System;

namespace P03_Array_Based_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ArrayStack<int>();
            stack.Push(2);
            for (int i = 0; i < 20; i++)
            {
                stack.Push(i);
            }
            Console.WriteLine(stack.Pop());
            Console.WriteLine(string.Join(", ",stack.ToArray()));
        }
    }
}
