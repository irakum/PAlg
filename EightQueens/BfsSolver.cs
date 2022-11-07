using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EightQueens
{
    public class BfsSolver
    {
        private Queue<State> _boards = new();

        public void Solve(State cur)
        {
            if (cur.CountBeaten() == 0)
            {
                return;
            }

            _boards.Enqueue(cur);
            bool solved = false;
            List<State> children;
            int iterations = 0;
            
            while (!solved)
            {
                cur = _boards.Dequeue();
                children = cur.GenerateChildren();
                foreach (var child in children)
                {
                    _boards.Enqueue(child);
                    if (child.CountBeaten() == 0)
                    {
                        solved = true;
                        cur = child;
                        break;
                    }
                }
                iterations ++;
            }
            Console.WriteLine("Moves: " + iterations);
            Console.WriteLine("Generated states: " + (iterations + _boards.Count));
            Console.WriteLine("Not used: " + _boards.Count);
            cur.PrintBoard();
        }
    }
}