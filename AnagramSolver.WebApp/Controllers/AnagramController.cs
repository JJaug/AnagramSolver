using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        private readonly ICacheAnagram _cachedAnagrams;
        public AnagramController()
        {
            _wordRepository = new WordRepositoryWithEF();
            _cachedAnagrams = new CacheAnagramWithEF();
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var allWords = _wordRepository.GetSpecificPage(pageNumber);
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            foreach (var word in allWords)
            {
                var anagram = new AnagramViewModel();
                anagram.Word = word.Word;
                vocabularyByModel.Add(item: anagram);
            }
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(model: pageNumber);
        }
        public IActionResult Details(string wordForAnagrams)
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
                vocabularyByModel.Add(anagram);
            }
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(vocabularyByModel);
        }
    }
}
