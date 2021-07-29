using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Task<HashSet<Word>> GetSpecificWords(string word);
        public Task<List<Word>> GetWords();

    }
}
