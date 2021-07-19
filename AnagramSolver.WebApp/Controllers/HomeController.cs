using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _wordRepository = new WordDBRepository();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form(string txtWord)
        {
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramById = _anagramSolver.GetAnagrams(txtWord);
            foreach (var word in anagramById)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                vocabularyByModel.Add(anagram);
            }
            return View(vocabularyByModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
