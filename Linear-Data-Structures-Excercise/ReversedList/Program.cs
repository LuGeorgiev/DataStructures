using System;

namespace ReversedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ReversedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            Console.WriteLine(list[1]);
            Console.WriteLine(list[4]);
            Console.WriteLine(list.RemoveAt(2));
            Console.WriteLine(list[2]);
            Console.WriteLine("-------------");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("-------------");
            Console.WriteLine(list.Capacity);
        }
    }
}
