﻿using AnagramSolver.BusinessLogic.Classes;
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
        public AnagramController()
        {
            _wordRepository = new WordDBRepository();
        }

        public IActionResult Index(int id = 1)
        {
            var allWords = _wordRepository.GetSpecificPage(id);
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            foreach (var word in allWords)
            {
                var anagram = new AnagramViewModel();
                anagram.Word = word.Word;
                vocabularyByModel.Add(item: anagram);
            }
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(model: id);
        }
        public IActionResult Details(string id)
        {
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramsById = _anagramSolver.GetAnagrams(id);
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
