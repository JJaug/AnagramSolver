using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class SearchLogWithEF : ISearchLog
    {
        public void updateSearchLog(int elapsedTime, string wordForAnagrams, List<string> listOfAnagrams)
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
