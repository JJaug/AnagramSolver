using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly ICacheServices _cachedAnagrams;
        private readonly IWordServices _wordServices;
        public AnagramController(IWordServices wordServices, ICacheServices cachedanagrams)
        {
            _cachedAnagrams = cachedanagrams;
            _wordServices = wordServices;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var vocabularyByModel = _wordServices.GetWordsAsAnagramModelVocabulary(pageNumber);
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(model: pageNumber);
        }
        public IActionResult Details(string wordForAnagrams)
        {
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            ViewBag.vocabularyByModel = vocabularyByModel;
            return View(vocabularyByModel);
        }
    }
}
