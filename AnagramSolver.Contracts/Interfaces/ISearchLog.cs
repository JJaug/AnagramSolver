using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLog
    {
        public void UpdateSearchLog(int elapsedTime, string wordForAnagrams, HashSet<AnagramModel> listOfAnagrams);
    }
}
