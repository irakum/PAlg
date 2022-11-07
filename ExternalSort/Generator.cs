using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExternalSort
{
    public class Generator
    {
        public void GenerateFile()
        {
            using var fileStream = new BinaryWriter(File.Create(Const.FileA));
            var rand = new Random(); 
            for (long i = 0; i < Const.NumOfElem; i++) 
            {
                fileStream.Write(rand.Next(Const.MaxNum));
            }
        }
    }
}