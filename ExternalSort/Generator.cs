using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExternalSort
{
    public class Generator
    {
        private List<int> _nums = new List<int>();

        public void GenerateArray(int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                _nums.Add(rand.Next(100));
            }

            foreach (int number in _nums)
            {
                Console.WriteLine(number);
            }
        }

        public void GenerateFile()
        {
            using var fileStream = new BinaryWriter(File.Create(Const.FileA));
            var rand = new Random(); 
            for (long i = 0; i < Const.NumOfElem; i++) 
            {
                fileStream.Write(rand.Next());
            }
        }
    }
}