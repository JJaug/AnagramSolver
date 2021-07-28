using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICacheRepository
    {
        public CachedWord FindCachedWord(string command);
        public void SaveWordToCache(CachedWord wordToCache);
    }
}
