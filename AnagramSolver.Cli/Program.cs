using System;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new BusinessLogic.Classes.AnagramSolver();
            var command = "";
            while(true)
            {
                Console.WriteLine("Iveskite zodi kurio anogramos norite arba iveskite exit jei norite iseiti is konsoles");
                command = Console.ReadLine();
                if(command == "exit")
                {
                    break;
                }
                var listOfAnagrams = result.GetAnagrams(command);
                Console.WriteLine("___Anogramos___");
                foreach(var anagram in listOfAnagrams)
                {
                    Console.WriteLine(anagram.Text);
                }
            }
        }
    }
}
