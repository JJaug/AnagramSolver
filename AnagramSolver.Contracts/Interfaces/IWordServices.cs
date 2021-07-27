using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServices
    {
        public HashSet<AnagramModel> GetWords(int pageNumber);
        public HashSet<AnagramModel> GetAnagrams(string wordForAnagrams);
        public HashSet<AnagramModel> CreateAnagramModelHashSet(HashSet<WordModel> allWords);



    }
}
