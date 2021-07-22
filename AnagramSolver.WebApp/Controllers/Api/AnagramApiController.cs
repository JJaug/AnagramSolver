using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace AnagramSolver.WebApp.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramApiController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;
        private readonly ICacheAnagram _cachedAnagrams;
        private readonly ISearchLog _searchLog;
        public AnagramApiController()
        {
            _wordRepository = new WordRepositoryWithEF();
            _cachedAnagrams = new CacheAnagramWithEF();
            _searchLog = new SearchLogWithEF();

        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public string GetJsonString(string wordForAnagrams)
        {
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var anagramsById = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                anagramsById = _anagramSolver.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, anagramsById);
            }
            foreach (var word in anagramsById)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                anagram.Word = wordForAnagrams;
                vocabularyByModel.Add(anagram);
            }
            string jsonString = JsonSerializer.Serialize(vocabularyByModel);
            return jsonString;
        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public List<string> GetForJS(string wordForAnagrams)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var listOfAnagrams = new List<string>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var anagramsById = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                anagramsById = _anagramSolver.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, anagramsById);
            }
            foreach (var word in anagramsById)
            {
                listOfAnagrams.Add(word);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = ts.Milliseconds;

            _searchLog.updateSearchLog(elapsedTime, wordForAnagrams, listOfAnagrams);

            return listOfAnagrams;
        }
        [HttpGet("[action]/{word}")]
        public HashSet<string> GetSearchList(string wordPart)
        {
            var wordsContainingSpecificPart = _wordRepository.GetSpecificWords(wordPart);

            return wordsContainingSpecificPart;
        }
    }
}
