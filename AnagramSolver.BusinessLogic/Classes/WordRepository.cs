using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class WordRepository : IWordRepository
    {
        private readonly string _filePath;
        private readonly int _minLength;
        public WordRepository(string filePath, int minLength)
        {
            _filePath = filePath;
            _minLength = minLength;
        }
        public HashSet<string> GetSpecificPage(int id)
        {
            var howManySkip = (id * 100) - 100;
            HashSet<string> allLines;
            var newList = new HashSet<string>();
            allLines = new HashSet<string>(File.ReadLines(_filePath));
            foreach (string line in allLines)
            {
                string[] itemsInLine = line.Split("\t").ToArray();
                if (itemsInLine[2].Length >= _minLength)
                {
                    newList.Add(itemsInLine[2]);
                }
            }
            return newList.Skip(howManySkip).Take(100).ToHashSet();
        }
        public HashSet<string> GetAllWords()
        {
            HashSet<string> allLines;
            var newList = new HashSet<string>();
            try
            {
                allLines = new HashSet<string>(File.ReadLines(_filePath));
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
                    if (itemsInLine[2].Length >= _minLength)
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
