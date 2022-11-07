using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ExternalSort
{
    public class MySort
    {
        private int _numOfLists;
        public void OptimizeSort()
        {
            Sort basicSort = new Sort();
            basicSort.PrintFile();
            CountNumOfLists();
            DivideFiles();
            File.Delete(Const.FileA);
            MergeAll();
            basicSort.PrintFile();
        }

        private void CountNumOfLists()
        {
            int numLists = (int) (Const.NumOfElem / Const.MaxListLen);
            int power = (int)Math.Log2(numLists);
            if ((int)Math.Pow(2, power) != numLists)
            {
                _numOfLists = (int)Math.Pow(2, power + 1);
            }
            else
            {
                _numOfLists = (int)Math.Pow(2, power);
            }
        }

        private void DivideFiles()
        {
            using BinaryReader fileA = new BinaryReader(File.OpenRead(Const.FileA));
            long fileLength = fileA.BaseStream.Length;
            long pos = 0;
            int currElem;
            List<int> numbers = new List<int>();
            string fileInpName;

            for (int i = 0; i < _numOfLists; i++)
            {
                numbers.Clear();
                
                for (int j = 0; j < Const.MaxListLen; j++)
                {
                    if (pos == fileLength)
                    {
                        break;
                    }

                    currElem = fileA.ReadInt32();
                    pos += 4;
                    numbers.Add(currElem);
                }

                numbers.Sort();
                fileInpName = string.Format(Const.FileName + "{0}.bin", i);
                using (BinaryWriter fileInput = new BinaryWriter(File.Create(fileInpName)))
                {
                    foreach (var num in numbers)
                    {
                        fileInput.Write(num);
                    }
                }
            }
        }
        
        private void MergeAll()
        {
            int rounds = (int)Math.Log2(_numOfLists);
            string fileA = null, fileB, fileC;
            int pairs;
            int numOfFiles = _numOfLists;

            for (int i = 0; i < rounds; i++)
            {
                pairs = numOfFiles / 2;
                for (int j = 0; j < pairs; j++)
                {
                    if (i % 2 == 0)
                    {
                        fileA = string.Format(Const.FileName + "{0}.bin", numOfFiles + j);
                        fileB = string.Format(Const.FileName + "{0}.bin", 2 * j);
                        fileC = string.Format(Const.FileName + "{0}.bin", 2 * j + 1);
                    }
                    else
                    {
                        fileA = string.Format(Const.FileName + "{0}.bin", j);
                        fileB = string.Format(Const.FileName + "{0}.bin", 2*(numOfFiles+j));
                        fileC = string.Format(Const.FileName + "{0}.bin", 2*(numOfFiles+j)+1);
                    }
                    MergeFiles(fileA, fileB, fileC);
                }
                numOfFiles = pairs;
            }
            
            File.Copy(fileA, Const.FileA);
        }

        private void MergeFiles(string fileAName, string fileBName, string fileCName)
        {
            using BinaryWriter fileA = new BinaryWriter(File.Create(fileAName));
            using BinaryReader fileB = new BinaryReader(File.OpenRead(fileBName));
            using BinaryReader fileC = new BinaryReader(File.OpenRead(fileCName));
            long lenB = fileB.BaseStream.Length;
            long lenC = fileC.BaseStream.Length;
            bool endB = false, endC = false;
            int? numberB = null, numberC = null;

            while (!endC || !endB)
            {
                if (fileB.BaseStream.Position != lenB)
                {
                    if (numberB == null)
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
                    if (numberC == null)
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
                    }
                    else
                    {
                        fileA.Write(numberC.Value);
                        numberC = null;
                    }
                }
                else if (numberC != null)
                {
                    fileA.Write(numberC.Value);
                    numberC = null;
                }
            }
        }

    }
}