using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository : IRepository<Word>
    {
        public HashSet<Word> GetSpecificWords(string word);
    }
}
