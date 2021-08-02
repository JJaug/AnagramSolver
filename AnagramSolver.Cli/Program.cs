﻿using AnagramSolver.Models.Models;
using GenericsTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static GenericsTask.Enums.GenderEnum;

namespace AnagramSolver.Cli
{
    class Program
    {

        static void ToConsole(string input)
        {
            Console.WriteLine(input);
        }
        static void ToFile(string input)
        {
            File.WriteAllTextAsync("File.txt", input);
        }
        static string FirstLetterUp(string input)
        {
            var result = char.ToUpper(input[0]) + input.Substring(1);
            return result;
        }
        static void ActionA()
        {
            Action<string> print = ToFile;
            var displayWithEvents = new DisplayWithEvents(print);
            displayWithEvents.ClickEvent += (s, args) =>
            {
                Console.WriteLine("Something clicked...");
            };
            Func<string, string> letterUp = FirstLetterUp;
            displayWithEvents.FormattedPrint(letterUp, "ooo sveiki");


        }
        static void ActionB()
        {
            Action<string> print = ToFile;
            var displayWithEvents = new DisplayWithEvents(print);
            displayWithEvents.ClickEvent += (s, args) =>
            {
                Console.WriteLine("Something clicked...");
            };
            Func<string, string> letterUp = FirstLetterUp;
            displayWithEvents.FormattedPrint(letterUp, "viso gero");


        }
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var x = new GenericsTask<Gender>();
            x.MapValueToEnum("labas");
            Action<string> print = ToFile;
            print("labas");
            Func<string, string> style = FirstLetterUp;
            var _display = new Display(print);
            _display.FormattedPrint(style, "labas rytas");
            Console.WriteLine("Type in A or B for event");
            var key = Console.ReadLine();
            if (key == "a")
            {
                ActionA();
            }
            else
            {
                ActionB();
            }

            //using IHost host = CreateHostBuilder(args).Build();

            //var fillDatabase = new PersistentRepositoryCodeFirst();
            //fillDatabase.PopulateDataBaseFromFile();
            //RemoveCachedWordTableInDB();
            var anagramApi = ReadSetting("AnagramApi");
            var minLength = int.Parse(ReadSetting("MinWordLength"));
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
                    HashSet<AnagramModel> listOfAnagrams = JsonSerializer.Deserialize<HashSet<AnagramModel>>(jsonString);

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

                //await host.RunAsync();


            }

        }

        //static void RemoveCachedWordTableInDB()
        //{
        //    using (var context = new VocabularyDBContext())
        //    {
        //        context.CachedWords.RemoveRange(context.CachedWords);
        //        context.SaveChanges();
        //    }
        //}

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                IConfigurationRoot configurationRoot = configuration.Build();

                AppSettings options = new();
                configurationRoot.GetSection(nameof(AppSettings))
                                 .Bind(options);

            }
        );


        static string ReadSetting(string key)
        {

            var appSettings = ConfigurationManager.AppSettings;
            string specificSetting = appSettings[key] ?? "Not Found";
            return specificSetting;

        }

        public class AppSettings
        {
            public string FilePath { get; set; }
            public int MaxNumberOfAnagrams { get; set; }
            public int MinWordLength { get; set; }
            public string AnagramApi { get; set; }
        }

    }
}

