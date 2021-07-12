using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        public List<string> GetSpecificAmountOfWords(string filePath)
        {
            HashSet<string> allLines;
            var newList = new List<string>();
            allLines = new HashSet<string>(File.ReadLines(@filePath));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                newList.Add(itemsInLine[2]);
            }
            return newList.Take(100).ToList();
        }
        public HashSet<string> GetAllWords(string filePath, int minLength)
        {
            HashSet<string> allLines;
            var newList = new HashSet<string>();
            try
            {
                allLines = new HashSet<string>(File.ReadLines(@filePath));
            }
            catch (FileNotFoundException)
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
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("Defined index doesn't exist");
                }
                catch (Exception)
                {
                    throw new Exception("Corrupted file");
                }
            }
            return newList;
        }

    }
}
