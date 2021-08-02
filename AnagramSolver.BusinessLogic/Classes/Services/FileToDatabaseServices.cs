using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic.Classes.PersistentRepositories
{
    public class FileToDatabaseServices : IFileToDatabaseService
    {
        private readonly IFileToDatabaseRepository _fileToDatabaseRepository;
        private readonly IConfiguration config;
        public FileToDatabaseServices(IFileToDatabaseRepository fileToDatabaseRepository, IConfiguration configuration)
        {
            config = configuration;
            _fileToDatabaseRepository = fileToDatabaseRepository;
        }
        public void PopulateDataBaseFromFile()
        {
            var filePath = config["MyConfig:FilePath"];

            HashSet<string> fileLines;
            fileLines = new HashSet<string>(File.ReadLines(filePath));
            var vocabulary = new HashSet<Word>();

            foreach (string line in fileLines)
            {
                Regex regex = new Regex(@"/^[a-zA-Z]{4,}$/");
                string[] wordsInLine = line.Split("\t").ToArray();
                if (regex.IsMatch(wordsInLine[2]))
                {
                    var wordToVocabulary = new Word { Word1 = wordsInLine[2] };
                    vocabulary.Add(wordToVocabulary);
                }
            }
            _fileToDatabaseRepository.AddWordsToDatabase(vocabulary).ConfigureAwait(false);
        }
    }
}
