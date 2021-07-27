using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.SearchLogs
{
    public class SearchLogDatabaseFirst : ISearchLog
    {
        public void UpdateSearchLog(int elapsedTime, string wordForAnagrams, HashSet<AnagramModel> listOfAnagrams)
        {
            using (var context = new VocabularyDBContext())
            {
                var logToAdd = new SearchLog { UserIp = "2.2.2.2", Word = wordForAnagrams, Anagrams = listOfAnagrams.Count, SearchTime = elapsedTime, CreatedAt = DateTime.Now };
                context.SearchLogs.Add(logToAdd);
                context.SaveChanges();

            }
        }
    }
}
