using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayStack
{ 
    class Program
    {
        static void Main(string[] args)
        {
            var line = new ArrayStack<int>();
            line.Push(2);
            Console.WriteLine(line.Pop());
        }
    }
}
