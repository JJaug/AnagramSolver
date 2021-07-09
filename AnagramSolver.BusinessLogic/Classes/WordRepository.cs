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
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("File not found");

            }
            foreach (string line in allLines)
            {
                try
                {
                    string[] itemsInLine = line.Split("\t").ToArray();
                    if (itemsInLine[2].Length >= minLength)
                    {
                        newList.Add(itemsInLine[2]);
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("Defined index doesn't exist");
                }
                catch (Exception e)
                {
                    throw new Exception("Corrupted file");
                }
            }
            return newList;
        }

    }
}
