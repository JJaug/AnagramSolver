using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var anagramsById = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                anagramsById = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, anagramsById);
            }
            var vocabularyByModel = _wordServices.CreateAnagramModelHashSet(anagramsById);
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(vocabularyByModel);
        }
    }
}
