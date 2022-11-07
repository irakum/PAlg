using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EightQueens
{
    public class Rbfs
    {
        private int _count;
        private int _fails;
        public void Solve(State problem)
        {
            State solution = Search(problem, int.MaxValue, 0).Item1;
            if (solution == null)
            {
                Console.WriteLine("Solution not found.");
            }
            else
            {
                solution.PrintBoard();
            }
            Console.WriteLine("Generated states: " + (Const.Size*(Const.Size-1)*_count+1));
            Console.WriteLine("Fails: " + _fails);
        }

        private (State, int) Search(State node, int limit, int iter)
        {
            State res;
            _count++;
            
            if (node.CountBeaten() == 0)
            {
                Console.WriteLine("Moves: " + iter);
                return (node, 0);
            }

            List<State> successors = node.GenerateChildren();
            List<int> cost = new List<int>();

            foreach (var child in successors)
            {
                cost.Add(child.CountBeaten()+iter); 
            }

            while (true)
            {
                int min = cost.Min();
                
                if (min > limit)
                {
                    _fails++;
                    return (null, Int32.MaxValue);
                }
                
                int indBest = cost.IndexOf(min);
                State best = successors[indBest];
                cost.RemoveAt(indBest); 
                int alt = cost.Min();
                cost.Insert(indBest, min); 
                (res, cost[indBest]) = Search(best, Math.Min(limit, alt), iter + 1); 
                if (res != null)
                {
                    return (res, cost[indBest]);
                }
            }
        }
    }
}