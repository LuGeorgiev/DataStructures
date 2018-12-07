using System;

namespace LinkedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new LinkedStack<int>();
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            stack.Push(8);

            Console.WriteLine(string.Join(", ",stack.ToAray()));
        }
    }
}
