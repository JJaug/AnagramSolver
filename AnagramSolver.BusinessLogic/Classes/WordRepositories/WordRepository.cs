using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.WordRepositories
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
            HashSet<string> fileLines;
            var vocabularyByModel = new HashSet<WordModel>();
            fileLines = new HashSet<string>(File.ReadLines(_filePath));
            foreach (string line in fileLines)
            {
                string[] wordsInLine = line.Split("\t").ToArray();
                if (wordsInLine[2].Length >= _minLength)
                {
                    var id = 1;
                    var word = new WordModel();
                    word.Word = wordsInLine[2];
                    word.ID = id;
                    vocabularyByModel.Add(word);
                    id++;
                }
            }
            return vocabularyByModel.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string wordPart)
        {
            var allWords = GetAllWords();
            var specificVocabulary = new HashSet<string>();
            foreach (var word in allWords)
            {
                if (word.Word.Contains(wordPart))
                {
                    specificVocabulary.Add(word.Word);
                }
            }
            return specificVocabulary;
        }

        public HashSet<WordModel> GetAllWords()
        {
            HashSet<string> fileLines;
            var vocabularyByModel = new HashSet<WordModel>();
            try
            {
                fileLines = new HashSet<string>(File.ReadLines(_filePath));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File not found");

            }
            foreach (string line in fileLines)
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
                        vocabularyByModel.Add(word);
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
            return vocabularyByModel;
        }

    }
}
