using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICacheAnagram
    {
        public CacheModel GetCachedAnagram(string command);
        public void PutAnagramToCache(string command, HashSet<AnagramModel> listOfAnagrams);
    }
}
