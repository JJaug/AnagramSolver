using System;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = "";
            do
            {
                Console.WriteLine("Iveskite zodi kurio anogramos norite");
                command = Console.ReadLine();


            } while (command == "exit");
        }
    }
}
