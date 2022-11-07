using System;

namespace ExternalSort
{
    public static class Const
    {
        public const string FileA = @"D:\2year\algorithms\ExternalSort\fileA.bin";
        public const string FileB = @"D:\2year\algorithms\ExternalSort\fileB.bin";
        public const string FileC = @"D:\2year\algorithms\ExternalSort\fileC.bin";
        public const string FileName = @"D:\2year\algorithms\ExternalSort\file";
        public static readonly long NumOfElem = 10*(long)Math.Pow(2, 18);
        public static readonly long MaxListLen = (long)Math.Pow(2, 27);
        public static readonly long MaxMaxListLen = (long)Math.Pow(2, 28);
        public static readonly long MaxNumOfElem = 3*(long)Math.Pow(2, 28);
        public const int OutputNums = 50;
        public const int MaxNum = Int32.MaxValue;
    }
}