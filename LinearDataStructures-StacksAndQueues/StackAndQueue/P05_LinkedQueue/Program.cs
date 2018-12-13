using System;

namespace P05_LinkedQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new LinkedQueue<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.Enqueue(i);
            }

            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(string.Join(", ",queue.ToArray()));
            Console.WriteLine(string.Join(", ",queue.ToArray()));
        }
    }
}
