using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public HashSet<string> GetAllWords(string filePath, int minLength)
        {
            var newList = new HashSet<string>();
            var allLines = new HashSet<string>(File.ReadLines(@filePath));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                if (itemsInLine[2].Length >= minLength)   
                {
                    newList.Add(itemsInLine[2]);
                }
            }
            return newList;
        }
    }
}
