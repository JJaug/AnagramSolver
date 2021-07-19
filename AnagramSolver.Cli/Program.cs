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


            var filePath = ReadSetting("FilePath");
            int minLength = Int32.Parse(ReadSetting("MinWordLength"));
            var anagramApi = ReadSetting("AnagramApi");
            var connString = ReadSetting("ConnectionString");

            Insert(filePath, minLength, connString);
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

        private static void Insert(string filePath, int minLength, string connectionString)
        {
            int id = 1;
            HashSet<string> allLines;
            allLines = new HashSet<string>(File.ReadLines(filePath));
            var newList = new HashSet<string>();

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
        static string ReadSetting(string key)
        {

            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;

        }
    }
}
