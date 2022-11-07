using System;
using System.Collections.Generic;

namespace EightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            BfsSolver Bsolver = new BfsSolver();
            Rbfs Rsolver = new Rbfs();
            State problem = new State();
            Console.WriteLine("First state:");
            problem.PrintBoard();
            Console.WriteLine("RBFS");
            Rsolver.Solve(problem);
            Console.WriteLine("BFS");
            Bsolver.Solve(problem);
        }
    }
}