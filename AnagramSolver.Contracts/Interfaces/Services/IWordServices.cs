using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServices
    {
        public Task<HashSet<AnagramModel>> GetWordsAsAnagramModelVocabulary(int pageNumber);
        public Task<HashSet<AnagramModel>> GetAnagrams(string wordForAnagrams);
        public Task<HashSet<string>> GetWordsThatHaveGivenPart(string wordPart);


    }
}
