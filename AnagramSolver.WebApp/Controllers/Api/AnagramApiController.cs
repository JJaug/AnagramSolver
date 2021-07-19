using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AnagramSolver.WebApp.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramApiController : ControllerBase
    {
        private readonly ILogger<AnagramApiController> _logger;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        public AnagramApiController(ILogger<AnagramApiController> logger)
        {
            _logger = logger;
            _wordRepository = new WordDBRepository();

        }
        [HttpGet("[action]/{id}")]
        public string GetJsonString(string id)
        {
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var allAnagramById = _anagramSolver.GetAnagrams(id);
            foreach (var word in allAnagramById)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                anagram.Word = id;
                vocabularyByModel.Add(anagram);
            }
            string jsonString = JsonSerializer.Serialize(vocabularyByModel);
            return jsonString;
        }
        [HttpGet("[action]/{id}")]
        public List<string> GetForJS(string id)
        {
            var listOfAnagrams = new List<string>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramsById = _anagramSolver.GetAnagrams(id);
            foreach (var word in anagramsById)
            {
                listOfAnagrams.Add(word);
            }
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
