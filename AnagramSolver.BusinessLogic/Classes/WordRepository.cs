using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
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
        private const int wordsInPage = 100;

        public WordRepository(string filePath, int minLength)
        {
            _filePath = filePath;
            _minLength = minLength;
        }
        public HashSet<WordModel> GetSpecificPage(int pageNumber)
        {
            var howManySkip = (pageNumber * wordsInPage) - wordsInPage;
            HashSet<string> allLines;
            var newList = new HashSet<WordModel>();
            allLines = new HashSet<string>(File.ReadLines(_filePath));
            foreach (string line in allLines)
            {
                string[] wordsInLine = line.Split("\t").ToArray();
                if (wordsInLine[2].Length >= _minLength)
                {
                    var id = 1;
                    var word = new WordModel();
                    word.Word = wordsInLine[2];
                    word.ID = id;
                    newList.Add(word);
                    id++;
                }
            }
            return newList.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string word)
        {
            var allWords = GetAllWords();
            var newList = new HashSet<string>();
            foreach(var aWord in allWords)
            {
                if (aWord.Word.Contains(word))
                {
                    newList.Add(aWord.Word);
                }
            }
            return newList;
        }

        public HashSet<WordModel> GetAllWords()
        {
            HashSet<string> allLines;
            var newList = new HashSet<WordModel>();
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
                    string[] wordsInLine = line.Split("\t").ToArray();
                    if (wordsInLine[2].Length >= _minLength)
                    {
                        var id = 1;
                        var word = new WordModel();
                        word.Word = wordsInLine[2];
                        word.ID = id;
                        newList.Add(word);
                        id++;
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
