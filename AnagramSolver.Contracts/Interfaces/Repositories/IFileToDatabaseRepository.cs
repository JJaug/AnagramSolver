using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IFileToDatabaseRepository
    {
        public void AddWordsToDatabase(HashSet<string> vocabulary);

    }
}
