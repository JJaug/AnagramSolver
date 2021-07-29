using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var vocabularyByModel = await _wordServices.GetWordsAsAnagramModelVocabulary(pageNumber);
            ViewBag.pageNumber = pageNumber;
            return View(vocabularyByModel);
        }
        public async Task<IActionResult> Details(string wordForAnagrams)
        {
            var cachedModels = await _cachedServices.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                var anagramTask = await _wordServices.GetAnagrams(wordForAnagrams);
                vocabularyByModel = anagramTask;
                _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            return View(vocabularyByModel);
        }
    }
}


