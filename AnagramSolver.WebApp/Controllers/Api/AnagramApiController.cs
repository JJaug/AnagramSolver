using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace AnagramSolver.WebApp.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramApiController : ControllerBase
    {
        private readonly IWordServices _wordServices;
        private readonly ICacheAnagram _cachedAnagrams;
        private readonly ISearchLog _searchLog;
        public AnagramApiController(IWordServices wordServices, ICacheAnagram cachedanagrams, ISearchLog searchLog)
        {
            _wordServices = wordServices;
            _cachedAnagrams = cachedanagrams;
            _searchLog = searchLog;

        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public string GetJsonString(string wordForAnagrams)
        {
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var vocabularyByModel = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                vocabularyByModel = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, vocabularyByModel);
            }
            string jsonString = JsonSerializer.Serialize(vocabularyByModel);
            return jsonString;
        }
        [HttpGet("[action]/{wordForAnagrams}")]
        public HashSet<AnagramModel> GetForJS(string wordForAnagrams)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var cachedModels = _cachedAnagrams.GetCachedAnagram(wordForAnagrams);
            var listOfAnagrams = cachedModels.Caches;
            if (!cachedModels.IsSuccessful)
            {
                listOfAnagrams = _wordServices.GetAnagrams(wordForAnagrams);
                _cachedAnagrams.PutAnagramToCache(wordForAnagrams, listOfAnagrams);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = ts.Milliseconds;

            _searchLog.UpdateSearchLog(elapsedTime, wordForAnagrams, listOfAnagrams);

            return listOfAnagrams;
        }
        [HttpGet("[action]/{word}")]
        public HashSet<string> GetSearchList(string wordPart)
        {
            var wordsContainingSpecificPart = _wordServices.GetWordsThatHaveGivenPart(wordPart);

            return wordsContainingSpecificPart;
        }
    }
}
