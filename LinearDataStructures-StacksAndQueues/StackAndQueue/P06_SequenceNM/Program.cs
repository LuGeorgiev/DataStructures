using System;
using System.Collections.Generic;
using System.Linq;

namespace P06_SequenceNM
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int start = nums[0];
            int toReach = nums[1];


            if (start > toReach)
            {
                return;
            }

            var queue = new Queue<Item>();
            queue.Enqueue(new Item(start, null));

            while (queue.Count > 0)
            {
                var element = queue.Dequeue();

                if (element.Value < toReach)
                {
                    queue.Enqueue(new Item(element.Value + 1, element));
                    queue.Enqueue(new Item(element.Value + 2, element));
                    queue.Enqueue(new Item(element.Value * 2, element));

                }
                else if (element.Value == toReach)
                {
                    var stack = new Stack<int>();
                    while (element != null)
                    {
                        stack.Push(element.Value);
                        element = element.PrevItem;
                    }
                    Console.WriteLine(string.Join(" -> ", stack));
                    return;
                }
            }
        }
              

        private class Item
        {
            public Item(int value, Item prevItem)
            {
                this.Value = value;
                this.PrevItem = prevItem;
            }
            public int Value { get; set; }

            public Item PrevItem { get; set; }
        }
    }
}
