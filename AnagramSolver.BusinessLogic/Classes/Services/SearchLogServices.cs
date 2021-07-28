using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.SearchLogs
{
    public class SearchLogServices : ISearchLogServices
    {
        private ISearchLogRepository _searchLogRepository;
        public SearchLogServices(ISearchLogRepository searchLogRepository)
        {
            _searchLogRepository = searchLogRepository;
        }
        public void UpdateSearchLog(int elapsedTime, string wordForAnagrams, HashSet<AnagramModel> listOfAnagrams)
        {

            var logToAdd = new SearchLog { UserIp = "2.2.2.2", Word = wordForAnagrams, Anagrams = listOfAnagrams.Count, SearchTime = elapsedTime, CreatedAt = DateTime.Now };
            _searchLogRepository.PutInfoToSearchLog(logToAdd);


        }
    }
}
