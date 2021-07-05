using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public List<string> GetAnagrams(string command);
    }
}
