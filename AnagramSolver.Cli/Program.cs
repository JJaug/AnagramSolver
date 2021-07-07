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
            int minLength, maxAnagrams;
            string filePath;
            ConfigureAppSettings(out minLength, out maxAnagrams, out filePath);
            var result = new BusinessLogic.Classes.AnagramSolver();
            var command = string.Empty;


            while (true)
            {
                Console.WriteLine("Iveskite zodi kurio anogramos norite arba iveskite exit jei norite iseiti is konsoles");
                command = Console.ReadLine();
                if (command.Length < minLength)
                {
                    Console.WriteLine($"Zodis turi buti bent is {minLength} raidziu");
                    command = Console.ReadLine();
                }
                if (command == "exit")
                {
                    break;
                }
                var listOfAnagrams = result.GetAnagrams(command, minLength, filePath);

                Console.WriteLine("___Anogramos___");

                for (int i = 0; i < listOfAnagrams.Count; i++)
                {
                    Console.WriteLine(listOfAnagrams[i].ToString());
                }
            }

        }

        private static void ConfigureAppSettings(out int minLength, out int maxAnagrams, out string filePath)
        {
            var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            minLength = Int32.Parse(config["MinWordLength"]);
            maxAnagrams = Int32.Parse(config["MaxNumberOfAnagrams"]);
            filePath = config["FilePath"];
        }
    }
}
