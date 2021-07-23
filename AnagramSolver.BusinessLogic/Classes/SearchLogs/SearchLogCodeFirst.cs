using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.SearchLogs
{
    public class SearchLogCodeFirst : ISearchLog
    {
        public void UpdateSearchLog(int elapsedTime, string wordForAnagrams, List<string> listOfAnagrams)
        {
            using (var context = new VocabularyCodeFirstContext())
            {
                var logToAdd = new SearchLog { UserIp = "2.2.2.2", Word = wordForAnagrams, Anagrams = listOfAnagrams.Count, SearchTime = elapsedTime, CreatedAt = DateTime.Now };
                context.SearchLogs.Add(logToAdd);
                context.SaveChanges();

            }
        }
    }
}
