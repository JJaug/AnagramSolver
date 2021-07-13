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
        public AnagramController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var _wordRepository = new WordRepository();
            var words = _wordRepository.GetSpecificAmountOfWords(_filePath);

            var newList = new HashSet<AnagramViewModel>();
            foreach (var word in words)
            {
                var anagram = new AnagramViewModel();
                anagram.Word = word;
                newList.Add(anagram);
            }
            var pager = new Pager(totalItems: words.Count, pageSize: 30);
            ViewBag.newList = newList;
            return View(newList);
        }
        public IActionResult Details(string id)
        {
            var newList = new HashSet<AnagramViewModel>();
            var _wordRepository = new WordRepository();
            var allWords = _wordRepository.GetAllWords(_filePath, 4);
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
