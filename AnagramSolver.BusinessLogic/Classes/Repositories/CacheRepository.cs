using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly VocabularyDBContext _context;
        public CacheRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public async Task<CachedWord> FindCachedWord(string command)
        {
            var cachedAnagram = _context.CachedWords.FindAsync(command);
            return await cachedAnagram;
        }

        public async void SaveWordToCache(CachedWord wordToCache)
        {
            await _context.CachedWords.AddAsync(wordToCache);
            _context.SaveChanges();
        }
    }
}
