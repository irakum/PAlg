using System;
using System.Collections.Generic;

namespace EightQueens
{
    public class State
    {
        private byte[] Board { get; }
        
        public State()
        {
            Board = new byte[Const.Size];
            GenerateRandom();
        }

        public State(byte[] board)
        {
            Board = board;
        }
        
        public List<State> GenerateChildren()
        {
            List<State> children = new List<State>();
            
            for (int i = 0; i < Const.Size; i++)
            {
                for (byte j = 0; j < Const.Size; j++)
                {
                    if (Board[i] != j)
                    {
                        byte[] child = new byte[Const.Size];
                        for (int k = 0; k < Const.Size; k++)
                        {
                            child[k] = Board[k];
                        }
                        child[i] = j;
                        children.Add(new State(child));
                    }
                }
            }

            return children;
        }
        private void GenerateRandom()
        {
            Random random = new Random();

            for (int i = 0; i < Const.Size; i++)
            {
                byte col = (byte)random.Next(Const.Size);
                Board[i] = col;
            }
        }
        public int CountBeaten()
        {
            return CheckColumns() + CheckLeftDiag() + CheckRightDiag();
        }
        private int CheckColumns()
        {
            int pairs = 0;
            int count;
            
            for (byte col = 0; col < Const.Size; col++)
            {
                count = 0;
                for (int j = 0; j < Const.Size; j++)
                {
                    if (Board[j] == col)
                    {
                        count++;
                    }
                }

                if (count>1)
                {
                    pairs += count - 1;
                }
            }

            return pairs;
        }
        private int CheckRightDiag()
        {
            int pairs = 0;
            int count;
            
            for (int dif = 0; dif < Const.Size-1; dif++)
            {
                count = 0;
                for (int row = 0; row+dif < Const.Size; row++)
                {
                    if (Board[row] == row+dif)
                    {
                        count++;
                    }
                }

                if (count>1)
                {
                    pairs += count - 1;
                }
            }
            
            for (int dif = 1; dif < Const.Size-1; dif++)
            {
                count = 0;
                for (int col = 0; col+dif < Const.Size; col++)
                {
                    if (Board[col+dif] == col)
                    {
                        count++;
                    }
                }

                if (count>1)
                {
                    pairs += count - 1;
                }
            }

            return pairs;
        }
        private int CheckLeftDiag()
        {
            int pairs = 0;
            int count;
            
            for (int sum = 1; sum < Const.Size; sum++)
            {
                count = 0;
                for (int row = 0; row < sum+1; row++)
                {
                    if (Board[row] == sum-row)
                    {
                        count++;
                    }
                }

                if (count>1)
                {
                    pairs += count - 1;
                }
            }
            
            for (int sum = Const.Size; sum < 2*Const.Size-2; sum++)
            {
                count = 0;
                for (int row = sum-Const.Size+1; row < Const.Size; row++)
                {
                    if (Board[row] == sum-row)
                    {
                        count++;
                    }
                }

                if (count>1)
                {
                    pairs += count - 1;
                }
            }

            return pairs;
        }
        public void PrintBoard()
        {
            for (int i = 0; i < Const.Size; i++)
            {
                for (byte j = 0; j < Const.Size; j++)
                {
                    if (Board[i] == j)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("_ ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool Same(State other)
        {
            bool res = true;
            for (int i = 0; i < Const.Size; i++)
            {
                if (Board[i] != other.Board[i])
                {
                    res = false;
                }
            }

            return res;
        }
    }
}