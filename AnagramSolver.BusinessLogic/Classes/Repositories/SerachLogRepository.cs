using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.BusinessLogic.Classes.Repositories
{
    public class SerachLogRepository : ISearchLogRepository
    {
        private readonly VocabularyDBContext _context;
        public SerachLogRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public async void PutInfoToSearchLog(SearchLog logToAdd)
        {
            await _context.SearchLogs.AddAsync(logToAdd);
            _context.SaveChanges();
        }
    }
}
