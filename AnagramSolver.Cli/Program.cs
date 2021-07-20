using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            using IHost host = CreateHostBuilder(args).Build();

            var fillDatabase = new PersistentRepository();
            fillDatabase.PopulateDataBaseFromFile();
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

                await host.RunAsync();


            }

        }

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

                Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.FilePath}");
                Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.MaxNumberOfAnagrams}");
            });
           
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

