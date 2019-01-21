using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Wintellect.PowerCollections;

namespace RopeTests
{
    class Program
    {
        static void Main(string[] args)
        {   
            int iterrations = 100000;
            Console.WriteLine("Irerration:" + iterrations);
            Stopwatch timer = new Stopwatch();
            BigList<string> rope = new BigList<string>();



            //PrependTest(iterrations, timer, rope);

            //AppendTest(iterrations, timer, rope);

            //InsertTest(iterrations, timer, rope);


        }

        private static void InsertTest(int iterrations, Stopwatch timer, BigList<string> rope)
        {
            timer.Start();
            for (int i = 0; i < iterrations; i++)
            {
                rope.Insert(rope.Count/2,"str");
            }
            Console.WriteLine($"Rope insert time elapsed:{timer.Elapsed}");

            StringBuilder builder = new StringBuilder();
            timer.Reset();
            timer.Start();

            for (int i = 0; i < iterrations; i++)
            {
                builder.Insert(builder.Length/2,"str");
            }
            Console.WriteLine($"StringBuilder insert time elapsed:{timer.Elapsed}");
        }

        private static void AppendTest(int iterrations, Stopwatch timer, BigList<string> rope)
        {
            timer.Start();
            for (int i = 0; i < iterrations; i++)
            {
                rope.Add("str");
            }
            Console.WriteLine($"Rope append time elapsed:{timer.Elapsed}");

            StringBuilder builder = new StringBuilder();
            timer.Reset();
            timer.Start();
            for (int i = 0; i < iterrations; i++)
            {
                builder.Append("str");
            }
            Console.WriteLine($"StringBuilder append time elapsed:{timer.Elapsed}");
        }

        private static void PrependTest(int iterrations, Stopwatch timer, BigList<string> rope)
        {
            timer.Start();
            for (int i = 0; i < iterrations; i++)
            {
                rope.Insert(0, "str");
            }
            Console.WriteLine($"Rope prepend time elapsed:{timer.Elapsed}");

            StringBuilder builder = new StringBuilder();
            timer.Reset();
            timer.Start();
            for (int i = 0; i < iterrations; i++)
            {
                builder.Insert(0, "str");
            }
            Console.WriteLine($"StringBuilder prepend time elapsed:{timer.Elapsed}");
        }
    }
}
