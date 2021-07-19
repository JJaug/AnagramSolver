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
            var newList = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var result = _anagramSolver.GetAnagrams(id);
            foreach (var word in result)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                anagram.Word = id;
                newList.Add(anagram);
            }
            string jsonString = JsonSerializer.Serialize(newList);
            return jsonString;
        }
        [HttpGet("[action]/{id}")]
        public List<string> GetForJS(string id)
        {
            var newList = new List<string>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var result = _anagramSolver.GetAnagrams(id);
            foreach (var word in result)
            {
                newList.Add(word);
            }
            return newList;
        }
    }
}
