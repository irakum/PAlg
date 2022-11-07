using System.Collections.Generic;

namespace ExternalSort
{
    public class SortList
    {
        private long _step = 1;
        private List<int> A;
        private List<int> B = new List<int>();
        private List<int> C = new List<int>();

        public SortList(List<int> start)
        {
            A = start;
        }
        
        public List<int> BasicSort()
        {
            while (_step < A.Count)
            {
                DivideList();
                MergeLists();
            }

            List<int> res = A;
            return res;
        }

        private void DivideList()
        {
            B.Clear();
            C.Clear();
            bool writeToB = true;
            int counter = 0;

            foreach (var num in A)
            {
                if (counter == _step)
                {
                    counter = 0;
                    writeToB = !writeToB;
                }

                if (writeToB)
                {
                    B.Add(num);
                }
                else
                {
                    C.Add(num);
                }

                counter++;
            }
        }

        private void MergeLists()
        {
            A.Clear();
            int lenB = B.Count, lenC = C.Count;
            int? numberB = null, numberC = null;
            int iterB = 0, iterC = 0;
            int indexB = 0, indexC = 0;
            bool endB = false, endC = false;

            while (!endB || !endC)
            {
                if (iterB==_step && iterC==_step)
                {
                    iterB = 0;
                    iterC = 0;
                }

                if (indexB<lenB)
                {
                    if (iterB < _step && numberB == null)
                    {
                        numberB = B[indexB];
                        indexB++;
                    }
                }
                else
                {
                    endB = true;
                }

                if (indexC<lenC)
                {
                    if (iterC<_step && numberC==null)
                    {
                        numberC = C[indexC];
                        indexC++;
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
                        A.Add((int)numberB);
                        numberB = null;
                        iterB++;
                    }
                    else
                    {
                        A.Add((int)numberC);
                        numberC = null;
                        iterC++;
                    }
                }
                else if (numberC != null)
                {
                    A.Add((int)numberC);
                    numberC = null;
                    iterC++;
                }
            }

            _step *= 2;
        }
    }
}