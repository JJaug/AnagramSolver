using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public HashSet<Word> GetSpecificWords(string word);
        public List<Word> GetWords();

    }
}
