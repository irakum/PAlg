using System;
using System.Diagnostics;
using System.IO;

namespace ExternalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            generator.GenerateFile();
            Sort sorter = new Sort();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            sorter.ExterSort();
            sw.Stop();
            Console.WriteLine("Sorting+output took {0}", sw.Elapsed);
        }
    }
}