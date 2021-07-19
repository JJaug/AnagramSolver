using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using System.Configuration;
using System.Collections.Specialized;
using AnagramSolver.Models.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // ConfigureAppSettings(out int minLength, out int maxAnagrams, out string filePath, out string anagramApi);
            
            string filePath;
            int minLength;
            string anagramApi;

            // Read a particular key from the config file 
            filePath = ConfigurationManager.AppSettings.Get("FilePath");
            minLength = Int32.Parse(ConfigurationManager.AppSettings.Get("minLength"));
            anagramApi = ConfigurationManager.AppSettings.Get("AnagramApi");
            Console.WriteLine("The value of Key0: " + filePath);
            
            Insert(filePath, minLength);
            IWordRepository wordRepository = new WordDBRepository();
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
                var path = $"{anagramApi}{command}";
                try
                {
                    var jsonString = await client.GetStringAsync(path);
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

        private static void ConfigureAppSettings(out int minLength, out int maxAnagrams, out string filePath, out string anagramApi)
        {
            var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            minLength = Int32.Parse(config["MinWordLength"]);
            maxAnagrams = Int32.Parse(config["MaxNumberOfAnagrams"]);
            anagramApi = config["AnagramApi"];
            filePath = config["FilePath"];
            
        }

        private static void Insert(string filePath, int minLength)
        {
            int id = 1;
            HashSet<string> allLines;
            allLines = new HashSet<string>(File.ReadLines(filePath));
            var newList = new HashSet<string>();
            string connectionString =
                @"Data Source=LT-LIT-SC-0597\MSSQLSERVER01;Initial Catalog=VocabularyDB;Integrated Security=True";
            foreach (string line in allLines)
            {
                string[] wordsInLine = line.Split("\t").ToArray();
                if (wordsInLine[2].Length >= minLength)
                {
                    var word = wordsInLine[2];
                    newList.Add(word);
                }
            }

            try
            {
                var conn = new SqlConnection(connectionString);
                conn.Open();
                foreach (var word in newList)
                {
                    var cmd = new SqlCommand("INSERT INTO Words (ID, Word) VALUES (@id, @word)", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@word", word);
                    cmd.ExecuteNonQuery();
                    id++;
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
