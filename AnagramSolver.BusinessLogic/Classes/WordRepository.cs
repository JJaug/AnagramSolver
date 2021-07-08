using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public HashSet<string> GetAllWords(string filePath, int minLength)
        {
            var allLines = new HashSet<string>();
            var newList = new HashSet<string>();
            try
            {
                allLines = new HashSet<string>(File.ReadLines(@filePath));
            }
            catch
            {
                return null;
            }
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
