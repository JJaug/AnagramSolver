using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public List<Anagram> GetAnagrams(string command, int minLength, string filePath);
    }
}
