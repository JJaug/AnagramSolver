using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IFileToDatabaseRepository
    {
        public Task AddWordsToDatabase(HashSet<string> vocabulary);

    }
}
