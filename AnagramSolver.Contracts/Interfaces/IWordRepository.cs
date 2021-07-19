using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public HashSet<WordModel> GetAllWords();
        public HashSet<WordModel> GetSpecificPage(int id);
        public HashSet<string> GetSpecificWords(string word);
    }
}
