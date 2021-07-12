﻿using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            IWordRepository wordRepository = new WordRepository();
            ConfigureAppSettings(out int minLength, out int maxAnagrams, out string filePath);
            var allWords = wordRepository.GetAllWords(filePath, minLength);
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
                var listOfAnagrams = result.GetAnagrams(command);

                Console.WriteLine("___Anogramos___");

                for (int i = 0; i < listOfAnagrams.Count; i++)
                {
                    Console.WriteLine(listOfAnagrams[i]);
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
