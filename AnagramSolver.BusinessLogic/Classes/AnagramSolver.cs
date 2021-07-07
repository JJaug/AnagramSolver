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

            List<Anagram> newList = new List<Anagram>();

            char[] commandChars = command.ToCharArray();
            Array.Sort(commandChars);

            foreach (var word in allWords)
            {
                if(word.Length == command.Length)
                {

                    char[] wordChars = word.ToCharArray();
                    Array.Sort(wordChars);

                    bool isEqual = Enumerable.SequenceEqual(wordChars, commandChars);
                    if (isEqual)
                    {
                        if (word != command)
                        {
                        Anagram anagram = new Anagram();
                        anagram.Text = word;
                        newList.Add(anagram);
                        }

                    }
                
                }
            }
            return newList;
        }
    }
}
