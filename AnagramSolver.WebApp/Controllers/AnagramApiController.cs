﻿using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly WordRepository _wordRepository;
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
            _wordRepository = new WordRepository(_filePath, 4);

        }
        [HttpGet("[action]/{id}")]
        public HashSet<AnagramViewModel> Get(string id)
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
            return newList;
        }
    }
}
