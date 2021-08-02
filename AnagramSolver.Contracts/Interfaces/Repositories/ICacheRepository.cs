using AnagramSolver.EF.DatabaseFirst.Models;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICacheRepository
    {
        public Task<CachedWord> FindCachedWord(string command);
        public void SaveWordToCache(CachedWord wordToCache);
    }
}
