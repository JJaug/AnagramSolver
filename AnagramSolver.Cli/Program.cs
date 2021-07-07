using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            var minLength = config["MinWordLength"];
            var minLengthAsNumber = Int32.Parse(minLength);
            var maxAnagrams = config["MaxNumberOfAnagrams"];
            var maxAnagramsAsNumber = Int32.Parse(maxAnagrams);

            var result = new BusinessLogic.Classes.AnagramSolver();
            var command = "";
            

            while(true)
            {
                Console.WriteLine("Iveskite zodi kurio anogramos norite arba iveskite exit jei norite iseiti is konsoles");
                command = Console.ReadLine();
                if(command.Length < minLengthAsNumber)
                {
                    Console.WriteLine($"Zodis turi buti bent is {minLength} raidziu");
                    command = Console.ReadLine();
                }
                if(command == "exit")
                {
                    break;
                }
                var listOfAnagrams = result.GetAnagrams(command);

                Console.WriteLine("___Anogramos___");

                for(int i = 0; i < maxAnagramsAsNumber; i++)
                {
                    Console.WriteLine(listOfAnagrams[i].Word);
                }
            }

        }

    }
}
