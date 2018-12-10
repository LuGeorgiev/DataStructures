using System;
using System.Collections.Generic;

namespace P02_CalculateSequenceWithQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            var queue = new Queue<int>();
            queue.Enqueue(number);
            int index = 1;

            while (queue.Count!=0)
            {
                int output = queue.Dequeue();
                if (index==50)
                {
                    Console.WriteLine(output);
                    break;
                }
                Console.Write(output+", ");
                queue.Enqueue(output + 1);
                queue.Enqueue(2*output + 1);
                queue.Enqueue(output + 2);
                index++;
            }
        }
    }
}
