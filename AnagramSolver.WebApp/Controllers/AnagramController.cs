using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        public AnagramController()
        {
            _wordRepository = new WordDBRepository();
        }

        public IActionResult Index(int id = 1)
        {
            var words = _wordRepository.GetSpecificPage(id);

            var newList = new HashSet<AnagramViewModel>();
            foreach (var word in words)
            {
                var anagram = new AnagramViewModel();
                anagram.Word = word.Word;
                newList.Add(anagram);
            }
            ViewBag.newList = newList;
            return View(id);
        }
        public IActionResult Details(string id)
        {
            var newList = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var result = _anagramSolver.GetAnagrams(id);
            foreach (var word in result)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                newList.Add(anagram);
            }
            ViewBag.newList = newList;
            return View(newList);
        }
    }
}
