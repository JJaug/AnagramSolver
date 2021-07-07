using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class AnagramSolver : IAnagramSolver
    {
        WordRepository wordRepository = new WordRepository();
        public List<Anagram> GetAnagrams(string command)
        {
            var allWords = wordRepository.GetAllWords();

            // var exists = allWords.FirstOrDefault(p => p.Equals("sula"));

            var newList = new List<Anagram>();

            command = command.Replace(" ", string.Empty);

            var commandChars = command.ToList();
            var remainingChars = commandChars;
            var result = string.Empty;

            sentenceMaker();
            
            void sentenceMaker()
            {
                foreach (var combination in allWords)
                {
                    var wordChars = combination.Word.ToList();
                    var contains = wordChars.Intersect(remainingChars).Count() == wordChars.Count();
                    if (contains)
                    {
                        remainingChars = remainingChars.Except(wordChars).ToList();
                        result = $"{combination.Word} ";
                        if (!remainingChars.Any())
                        {
                            Anagram anagram = new Anagram();
                            anagram.Word = result;
                            newList.Add(anagram);

                        }
                        sentenceMaker();
                    }

                }
            }
            return newList;
        }
    }
}
