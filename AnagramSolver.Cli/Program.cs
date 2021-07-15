using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            ConfigureAppSettings(out int minLength, out int maxAnagrams, out string filePath);
            IWordRepository wordRepository = new WordRepository(filePath, minLength);
            var allWords = wordRepository.GetAllWords();
            var result = new BusinessLogic.Classes.AnagramSolver(allWords);
            while (true)
            {
                Console.WriteLine("Iveskite zodi kurio anogramos norite arba iveskite exit jei norite iseiti is konsoles");
                string command = Console.ReadLine();
                if (command.Length < minLength)
                {
                    Console.WriteLine($"Zodis turi buti bent is {minLength} raidziu");
                    command = Console.ReadLine();
                }
                if (command == "exit")
                {
                    break;
                }
                try
                {
                    var jsonString = await client.GetStringAsync("https://localhost:44385/api/get/" + $"{command}");
                    HashSet<AnagramViewModel> listOfAnagrams = JsonSerializer.Deserialize<HashSet<AnagramViewModel>>(jsonString);

                    Console.WriteLine("___Anogramos___");
                    foreach (var anagram in listOfAnagrams)
                    {
                        Console.WriteLine(anagram.AnagramWord);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
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
