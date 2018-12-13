using System;

namespace P04_LinkedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new LinkedStack<int>();
            stack.Push(2);
            for (int i = 0; i < 4; i++)
            {
                stack.Push(i);
            }
            Console.WriteLine(stack.Pop());

            Console.WriteLine(string.Join(", ", stack.ToArray()));
        }
    }
}
