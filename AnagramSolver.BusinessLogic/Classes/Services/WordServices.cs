using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Services
{
    public class WordServices : IWordServices
    {
        private readonly IConfiguration config;
        private readonly IWordRepository _wordRepository;
        public WordServices(IWordRepository wordRepository, IConfiguration configuration)
        {
            _wordRepository = wordRepository;
            config = configuration;
        }
        public async Task<HashSet<AnagramModel>> GetWordsAsAnagramModelVocabulary(int pageNumber)
        {
            var wordsInPage = int.Parse(config["MyConfig:WordsInPage"]);
            var howManySkip = (pageNumber * wordsInPage) - wordsInPage;
            var allWords = _wordRepository.GetAll();
            var wordsFromDB = new HashSet<WordModel>();
            foreach (var word in allWords)
            {
                var wordModel = new WordModel();
                wordModel.ID = word.Id;
                wordModel.Word = word.Word1;
                wordsFromDB.Add(wordModel);
            }
            var vocabularyByModel = new HashSet<AnagramModel>();
            foreach (var word in wordsFromDB)
            {
                var wordInPage = new AnagramModel();
                wordInPage.Word = word.Word;
                vocabularyByModel.Add(wordInPage);
            }
            return vocabularyByModel.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }
        public async Task<HashSet<AnagramModel>> GetAnagrams(string wordForAnagrams)
        {
            var allWords = _wordRepository.GetAll();
            var wordsFromDB = new HashSet<WordModel>();
            foreach (var word in allWords)
            {
                var wordModel = new WordModel();
                wordModel.ID = word.Id;
                wordModel.Word = word.Word1;
                wordsFromDB.Add(wordModel);
            }
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(wordsFromDB);
            var anagramsById = _anagramSolver.GetAnagrams(wordForAnagrams);
            return anagramsById;
        }
        public async Task<HashSet<string>> GetWordsThatHaveGivenPart(string wordPart)
        {
            var wordsFromDb = _wordRepository.GetSpecificWords(wordPart);
            var specificWords = new HashSet<string>();
            foreach (var word in wordsFromDb)
            {
                specificWords.Add(word.Word1);
            }
            return specificWords;
        }
    }
}
