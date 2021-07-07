using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

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
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configurationRoot = configuration.Build();
            });
    }
}
