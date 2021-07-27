using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.Services
{
    public class WordServices : IWordServices
    {
        private readonly IWordRepository _wordRepository;
        public WordServices(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public HashSet<AnagramModel> GetWords(int pageNumber)
        {
            var allWords = _wordRepository.GetSpecificPage(pageNumber);
            return CreateAnagramModelHashSet(allWords);
        }
        public HashSet<AnagramModel> GetAnagrams(string wordForAnagrams)
        {
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramsById = _anagramSolver.GetAnagrams(wordForAnagrams);
            return anagramsById;
        }
        public HashSet<AnagramModel> CreateAnagramModelHashSet(HashSet<WordModel> allWords)
        {
            var vocabularyByModel = new HashSet<AnagramModel>();
            foreach (var word in allWords)
            {
                var anagram = new AnagramModel();
                anagram.Word = word.Word;
                vocabularyByModel.Add(anagram);
            }
            return vocabularyByModel;
        }
    }
}
