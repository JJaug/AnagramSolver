using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public HashSet<Words> GetAllWords();
    }
}
