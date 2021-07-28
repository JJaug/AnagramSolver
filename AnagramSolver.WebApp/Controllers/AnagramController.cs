using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramController : Controller
    {
        private readonly ICacheServices _cachedServices;
        private readonly IWordServices _wordServices;
        public AnagramController(IWordServices wordServices, ICacheServices cachedanagrams)
        {
            _cachedServices = cachedanagrams;
            _wordServices = wordServices;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var vocabularyByModel = _wordServices.GetWordsAsAnagramModelVocabulary(pageNumber);
            ViewBag.pageNumber = pageNumber;
            return View(vocabularyByModel);
        }
        public IActionResult Details(string wordForAnagrams)
        {
            var cachedModels = _cachedServices.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            return View(vocabularyByModel);
        }
    }
}
