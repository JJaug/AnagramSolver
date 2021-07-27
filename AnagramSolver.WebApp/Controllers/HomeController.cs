using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        private readonly ICacheAnagram _cachedAnagrams;
        public HomeController(IWordRepository wordRepository, ICacheAnagram cachedanagrams)
        {
            _wordRepository = wordRepository;
            _cachedAnagrams = cachedanagrams;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form(string id)
        {
            var vocabularyByModel = new HashSet<AnagramModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var cachedModels = _cachedAnagrams.GetCachedAnagram(id);
            var anagramsById = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                anagramsById = _anagramSolver.GetAnagrams(id);
                _cachedAnagrams.PutAnagramToCache(id, anagramsById);
            }
            foreach (var word in anagramsById)
            {
                vocabularyByModel.Add(word);
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
