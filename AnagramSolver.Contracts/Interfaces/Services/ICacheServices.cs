using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICacheServices
    {
        public Task<CacheModel> GetCachedAnagram(string command);
        public void PutAnagramToCache(string command, HashSet<AnagramModel> listOfAnagrams);
    }
}
