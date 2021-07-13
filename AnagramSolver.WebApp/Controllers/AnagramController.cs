using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.WebApp.Models;
using JW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string _filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt";
        private readonly WordRepository _wordRepository;
        public AnagramController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _wordRepository = new WordRepository(_filePath, 4);
        }

        public IActionResult Index(int id)
        {
            var words = _wordRepository.GetSpecificPage(id);

            var newList = new HashSet<AnagramViewModel>();
            foreach (var word in words)
            {
                var anagram = new AnagramViewModel();
                anagram.Word = word;
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
            return View();
        }

    }
}
