using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public HashSet<Words> GetAllWords()
        {
            HashSet<Words> newList = new HashSet<Words>();
            HashSet<string> allLines = new HashSet<string>(File.ReadLines(@"C:\Users\jonas.jaugelis\source\repos\AnagramSolver\zodynas.txt"));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                Words word = new Words();
                word.Type = itemsInLine[1];
                word.Word = itemsInLine[2];
                newList.Add(word);
            }
            return newList;
        }
    }
}
