using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.WordRepositories
{
    public class WordRepositoryDatabaseFirst : IWordRepository
    {
        private readonly VocabularyDBContext _context;
        private readonly IConfiguration config;
        private int wordsInPage;
        public WordRepositoryDatabaseFirst(VocabularyDBContext context, IConfiguration configuration)
        {
            _context = context;
            config = configuration;
        }
        public HashSet<WordModel> GetAllWords()
        {
            var wordsFromDB = new HashSet<WordModel>();

            var allWords = _context.Words.ToList();
            foreach (var word in allWords)
            {

                var wordModel = new WordModel();
                wordModel.ID = word.Id;
                wordModel.Word = word.Word1;
                wordsFromDB.Add(wordModel);

            }

            return wordsFromDB;
        }

        public HashSet<WordModel> GetSpecificPage(int pageNumber)
        {
            wordsInPage = int.Parse(config.GetSection("MyConfig").GetSection("WordsInPage").Value);
            var howManySkip = (pageNumber * wordsInPage) - wordsInPage;
            var wordsFromDB = new HashSet<WordModel>();

            var allWords = _context.Words.ToList();
            foreach (var word in allWords)
            {

                var wordModel = new WordModel();
                wordModel.Word = word.Word1;
                wordModel.ID = word.Id;
                wordsFromDB.Add(wordModel);

            }

            return wordsFromDB.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string wordPart)
        {
            var specificWords = new HashSet<string>();
            var wordModel = new WordModel();

            var wordsFromDb = _context.Words
                .Where(w => w.Word1.Contains(wordPart))
                .ToHashSet();
            foreach (var word in wordsFromDb)
            {
                specificWords.Add(word.Word1);
            }


            return specificWords;
        }
    }
}
