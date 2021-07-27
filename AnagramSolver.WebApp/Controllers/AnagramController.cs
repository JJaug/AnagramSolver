using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly ICacheAnagram _cachedAnagrams;
        private readonly IWordServices _wordServices;
        public AnagramController(IWordServices wordServices, ICacheAnagram cachedanagrams)
        {
            _cachedAnagrams = cachedanagrams;
            _wordServices = wordServices;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var vocabularyByModel = _wordServices.GetWords(pageNumber);
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(model: pageNumber);
        }
        public IActionResult Details(string wordForAnagrams)
        {
            var vocabularyByModel = new HashSet<AnagramModel>();

            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var anagramsById = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                anagramsById = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, anagramsById);
            }
            foreach (var word in anagramsById)
            {
                vocabularyByModel.Add(word);
            }
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(vocabularyByModel);
        }
    }
}
