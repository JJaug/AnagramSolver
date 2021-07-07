using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class AnagramSolver : IAnagramSolver
    {
        WordRepository word = new WordRepository();
        public List<Anagram> GetAnagrams(string command)
        {
            var allWords = word.GetAllWords();

            // var exists = allWords.FirstOrDefault(p => p.Equals("sula"));

            var newList = new List<Anagram>();

            var commandChars = command.ToCharArray();
            Array.Sort(commandChars);

            foreach (var word in allWords)
            {
                if(word.Length == command.Length)
                {
                    var wordChars = word.ToCharArray();
                    Array.Sort(wordChars);

                    var isEqual = Enumerable.SequenceEqual(wordChars, commandChars);
                    if (isEqual)
                    {
                        if (word != command)
                        {
                            var anagram = new Anagram();
                            anagram.Word = word;
                            newList.Add(anagram);
                        }

                    }
                
                }
            }
            return newList;
        }
    }
}
