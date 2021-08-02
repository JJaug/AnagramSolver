using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public HashSet<AnagramModel> GetAnagrams(string command);
    }
}
