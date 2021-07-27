using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.SearchLogs
{
    public class SearchLogDatabaseFirst : ISearchLog
    {
        private readonly VocabularyDBContext _context;
        public SearchLogDatabaseFirst(VocabularyDBContext context)
        {
            _context = context;
        }
        public void UpdateSearchLog(int elapsedTime, string wordForAnagrams, HashSet<AnagramModel> listOfAnagrams)
        {

            var logToAdd = new SearchLog { UserIp = "2.2.2.2", Word = wordForAnagrams, Anagrams = listOfAnagrams.Count, SearchTime = elapsedTime, CreatedAt = DateTime.Now };
            _context.SearchLogs.Add(logToAdd);
            _context.SaveChanges();


        }
    }
}
