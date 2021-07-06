using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public HashSet<Anagram> GetAnagrams(string command);
    }
}
