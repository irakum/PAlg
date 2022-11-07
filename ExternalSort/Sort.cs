using System;
using System.IO;

namespace ExternalSort
{
    public class Sort
    {
        private long _step = 1;
        public void ExterSort()
        {
            PrintFile();
            while (_step < Const.NumOfElem)
            {
                DivideFiles();
                MergeFiles();
            }
            Console.WriteLine();
            PrintFile();
        }
        private void DivideFiles()
        {
            using BinaryReader fileA = new BinaryReader(File.OpenRead(Const.FileA));
            using BinaryWriter fileB = new BinaryWriter(File.Create(Const.FileB));
            using BinaryWriter fileC = new BinaryWriter(File.Create(Const.FileC));
            bool isOdd = true;
            long length = fileA.BaseStream.Length;
            long pos = 0;
            long counter = 0;
            int element;

            while (pos != length)
            {
                if (counter == _step)
                {
                    counter = 0;
                    isOdd = !isOdd;
                }

                element = fileA.ReadInt32();
                pos += 4;
                if (isOdd)
                {
                    fileB.Write(element);
                }
                else
                {
                    fileC.Write(element);
                }

                counter++;
            }
        }
        private void MergeFiles()
        {
            using BinaryWriter fileA = new BinaryWriter(File.Create(Const.FileA));
            using BinaryReader fileB = new BinaryReader(File.OpenRead(Const.FileB));
            using BinaryReader fileC = new BinaryReader(File.OpenRead(Const.FileC));
            long lenB = fileB.BaseStream.Length;
            long lenC = fileC.BaseStream.Length;
            bool endB = false, endC = false;
            int? numberB = null, numberC = null;
            long iterB = 0, iterC = 0;

            while (!endC || !endB)
            {
                if (iterB==_step && iterC==_step)
                {
                    iterB = 0;
                    iterC = 0;
                }

                if (fileB.BaseStream.Position != lenB)
                {
                    if (iterB < _step && numberB == null)
                    {
                        numberB = fileB.ReadInt32();
                    }
                }
                else
                {
                    endB = true;
                }

                if (fileC.BaseStream.Position != lenC)
                {
                    if (iterC < _step && numberC == null)
                    {
                        numberC = fileC.ReadInt32();
                    }
                }
                else
                {
                    endC = true;
                }

                if (numberB != null)
                {
                    if (numberC == null || numberB < numberC)
                    {
                        fileA.Write(numberB.Value);
                        numberB = null;
                        iterB++;
                    }
                    else
                    {
                        fileA.Write(numberC.Value);
                        numberC = null;
                        iterC++;
                    }
                }
                else if (numberC != null)
                {
                    fileA.Write(numberC.Value);
                    numberC = null;
                    iterC++;
                }
            }

            _step *= 2;
        }

        public void PrintFile()
        {
            using BinaryReader fileA = new BinaryReader(File.OpenRead(Const.FileA));
            long lenA = fileA.BaseStream.Length;
            for (long i = 0; i < Const.OutputNums; i++)
            {
                Console.Write(fileA.ReadInt32().ToString() + ' ');
            }
            
            Console.WriteLine();
            fileA.BaseStream.Position = lenA - 4*Const.OutputNums;

            for (long i = 0; i < Const.OutputNums; i++)
            {
                Console.Write(fileA.ReadInt32().ToString() + ' ');
            }
                
            Console.WriteLine();
        }
    }
}