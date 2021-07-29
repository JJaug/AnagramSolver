using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordServices _wordServices;
        private readonly ICacheServices _cachedAnagrams;
        public HomeController(IWordServices wordService, ICacheServices cachedanagrams)
        {
            _wordServices = wordService;
            _cachedAnagrams = cachedanagrams;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Form(string id)
        {
            var cachedModels = await _cachedAnagrams.GetCachedAnagram(id);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = await _wordServices.GetAnagrams(id);
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
