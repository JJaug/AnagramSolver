using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.BusinessLogic.Classes.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly VocabularyDBContext _context;
        public CacheRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public CachedWord FindCachedWord(string command)
        {
            var cachedAnagram = _context.CachedWords.Find(command);
            return cachedAnagram;
        }

        public void SaveWordToCache(CachedWord wordToCache)
        {
            _context.CachedWords.Add(wordToCache);
            _context.SaveChanges();
        }
    }
}
