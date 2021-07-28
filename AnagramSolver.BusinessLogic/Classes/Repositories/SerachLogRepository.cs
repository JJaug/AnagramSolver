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
        public void PutInfoToSearchLog(SearchLog logToAdd)
        {
            _context.SearchLogs.Add(logToAdd);
            _context.SaveChanges();
        }
    }
}
