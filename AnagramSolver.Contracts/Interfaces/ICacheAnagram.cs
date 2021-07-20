using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICacheAnagram
    {
        public List<string> GetCachedAnagram(string command);
        public void PutAnagramToCache(string command, List<string> listOfAnagrams);
    }
}
