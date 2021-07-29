using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramApiController : ControllerBase
    {
        private readonly IWordServices _wordServices;
        private readonly ICacheServices _cachedServices;
        private readonly ISearchLogServices _searchLogServices;
        public AnagramApiController(IWordServices wordServices, ICacheServices cachedanagrams, ISearchLogServices searchLog)
        {
            _wordServices = wordServices;
            _cachedServices = cachedanagrams;
            _searchLogServices = searchLog;

        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public async Task<string> GetJsonString(string wordForAnagrams)
        {
            var cachedModels = await _cachedServices.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = await _wordServices.GetAnagrams(wordForAnagrams);
                _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            string jsonString = JsonSerializer.Serialize(vocabularyByModel);
            return jsonString;
        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public async Task<HashSet<AnagramModel>> GetForJS(string wordForAnagrams)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var cachedModels = await _cachedServices.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = await _wordServices.GetAnagrams(wordForAnagrams);
                _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = ts.Milliseconds;

            _searchLogServices.UpdateSearchLog(elapsedTime, wordForAnagrams, vocabularyByModel);

            return vocabularyByModel;
        }
        [HttpGet("[action]/{word}")]
        public async Task<HashSet<string>> GetSearchList(string wordPart)
        {
            var wordsContainingSpecificPart = await _wordServices.GetWordsThatHaveGivenPart(wordPart);

            return wordsContainingSpecificPart;
        }
    }
}
