﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class AnagramSolver : IAnagramSolver
    {
        WordRepository wordRepository = new WordRepository();
        public List<Anagram> GetAnagrams(string command, int minLength, string filePath)
        {
            var allWords = wordRepository.GetAllWords(filePath, minLength);

            // var exists = allWords.FirstOrDefault(p => p.Equals("sula"));

            var newList = new List<Anagram>();

            command = command.Replace(" ", string.Empty);

            var commandChars = command.ToList();
            var remainingChars = commandChars;
            var result = string.Empty;

            sentenceMaker(remainingChars);
            
            void sentenceMaker(List<char> remainingChars)
            {
                if(remainingChars.Count >= minLength)
                {
                    foreach (var combination in allWords)
                    {
                        var word = combination.Word;
                        var wordChars = word.ToList();
                        var contains = wordChars.Intersect(remainingChars).Count() == wordChars.Count();
                        if (contains)
                        {
                            remainingChars = remainingChars.Except(wordChars).ToList();
                            result += $"{combination.Word} ";
                            if (!remainingChars.Any())
                            {
                                Anagram anagram = new Anagram();
                                anagram.Word = result;
                                newList.Add(anagram);
                            }
                            sentenceMaker(remainingChars);
                        }

                    }
                }
            }
            return newList;
        }
    }
}
