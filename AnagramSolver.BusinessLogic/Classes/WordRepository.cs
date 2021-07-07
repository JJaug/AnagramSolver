using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public HashSet<Words> GetAllWords(string filePath, int minLength)
        {
            HashSet<Words> newList = new HashSet<Words>();
            HashSet<string> allLines = new HashSet<string>(File.ReadLines(@filePath));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                if ((itemsInLine[1] == "dkt" || itemsInLine[1] == "vksm" || itemsInLine[1] == "bdv" || itemsInLine[1] == "tikr. dkt")&&
                    (itemsInLine[2].Length >= minLength))
                {
                    Words word = new Words();
                    word.Type = itemsInLine[1];
                    word.Word = itemsInLine[2];
                    newList.Add(word);
                }
            }
            return newList;
        }
    }
}
