﻿using AnagramSolver.Contracts.Interfaces;
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
        public HashSet<AnagramModel> GetWordsAsAnagramModelVocabulary(int pageNumber)
        {
            var allWords = _wordRepository.GetSpecificPage(pageNumber);
            var vocabularyByModel = new HashSet<AnagramModel>();
            foreach (var word in allWords)
            {
                var wordInPage = new AnagramModel();
                wordInPage.Word = word.Word;
                vocabularyByModel.Add(wordInPage);
            }
            return vocabularyByModel;
        }
        public HashSet<AnagramModel> GetAnagrams(string wordForAnagrams)
        {
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramsById = _anagramSolver.GetAnagrams(wordForAnagrams);
            return anagramsById;
        }
        public HashSet<string> GetWordsThatHaveGivenPart(string wordPart)
        {
            return _wordRepository.GetSpecificWords(wordPart);
        }
    }
}
