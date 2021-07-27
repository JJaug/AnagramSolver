using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.PersistentRepositories
{
    public class PersistentRepositoryDatabaseFirst : IPersistentRepository
    {
        private readonly IConfiguration config;
        private readonly VocabularyDBContext _context;
        public PersistentRepositoryDatabaseFirst(VocabularyDBContext context, IConfiguration configuration)
        {
            _context = context;
            config = configuration;
        }
        public void PopulateDataBaseFromFile()
        {
            var filePath = config.GetSection("MyConfig").GetSection("FilePath").Value;
            var minLength = int.Parse(config.GetSection("MyConfig").GetSection("MinWoedLength").Value);

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
            try
            {

                foreach (var word in vocabulary)
                {

                    var wordToAdd = new Word { Word1 = word };
                    _context.Words.Add(wordToAdd);
                    if (_context.Words.Count() % 30000 == 0)
                        _context.SaveChanges();

                }
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
