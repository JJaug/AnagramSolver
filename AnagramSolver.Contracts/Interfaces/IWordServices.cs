﻿using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServices
    {
        public HashSet<AnagramModel> GetWords(int pageNumber);
        public HashSet<AnagramModel> GetAnagrams(string wordForAnagrams);
        public HashSet<AnagramModel> CreateAnagramModelHashSet(HashSet<AnagramModel> allWords);
        public HashSet<string> GetWordsThatHaveGivenPart(string wordPart);


    }
}
