using AnagramSolver.Contracts.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public HashSet<string> GetAllWords()
        {
            HashSet<string> newList = new HashSet<string>();
            HashSet<string> allLines = new HashSet<string>(File.ReadLines(@"C:\Users\jonas.jaugelis\source\repos\AnagramSolver\zodynas.txt"));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                for (var i = 2; i == 2; i++)
                {
                    newList.Add(itemsInLine[i]);
                }
            }
            return newList;
        }
    }
}
