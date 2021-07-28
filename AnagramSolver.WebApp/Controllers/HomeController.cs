using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordServices _wordServices;
        private readonly ICacheAnagram _cachedAnagrams;
        public HomeController(IWordServices wordService, ICacheAnagram cachedanagrams)
        {
            _wordServices = wordService;
            _cachedAnagrams = cachedanagrams;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form(string id)
        {
            var cachedModels = _cachedAnagrams.GetCachedAnagram(id);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = _wordServices.GetAnagrams(id);
                _cachedAnagrams.PutAnagramToCache(id, vocabularyByModel);
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
