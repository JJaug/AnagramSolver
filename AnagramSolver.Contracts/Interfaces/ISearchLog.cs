using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLog
    {
        public void updateSearchLog(int elapsedTime, string wordForAnagrams, List<string> listOfAnagrams);
    }
}
