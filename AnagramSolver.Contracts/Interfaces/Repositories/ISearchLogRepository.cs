using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogRepository
    {
        public void PutInfoToSearchLog(SearchLog logToAdd);

    }
}
