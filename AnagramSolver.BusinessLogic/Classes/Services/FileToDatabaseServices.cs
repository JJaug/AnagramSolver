using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.PersistentRepositories
{
    public class FileToDatabaseServices : IFileToDatabaseService
    {
        private IFileToDatabaseRepository _fileToDatabaseRepository;
        private readonly IConfiguration config;
        public FileToDatabaseServices(IFileToDatabaseRepository fileToDatabaseRepository, IConfiguration configuration)
        {
            config = configuration;
            _fileToDatabaseRepository = fileToDatabaseRepository;
        }
        public void PopulateDataBaseFromFile()
        {
            var filePath = config["MyConfig:FilePath"];
            var minLength = int.Parse(config["MyConfig:MinWordLength"]);

            HashSet<string> fileLines;
            fileLines = new HashSet<string>(File.ReadLines(filePath));
            var vocabulary = new HashSet<string>();

            foreach (string line in fileLines)
            {
                string[] wordsInLine = line.Split("\t").ToArray();
                if (wordsInLine[2].Length >= minLength)
                {
                    var word = wordsInLine[2];
                    vocabulary.Add(word);
                }
            }
            _fileToDatabaseRepository.AddWordsToDatabase(vocabulary).ConfigureAwait(false);
        }
    }
}
