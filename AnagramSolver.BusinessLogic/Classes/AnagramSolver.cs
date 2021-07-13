using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly HashSet<string> _allWords;
        public AnagramSolver(HashSet<string> allWords)
        {
            _allWords = allWords;
        }
        public List<string> GetAnagrams(string command)
        {
            var newList = new List<string>();

            char[] commandChars = command.ToCharArray();
            Array.Sort(commandChars);
            foreach (var word in _allWords)
            {
                if (word.Length == command.Length)
                {
                    char[] wordChars = word.ToCharArray();
                    Array.Sort(wordChars);
                    bool isEqual = Enumerable.SequenceEqual(wordChars, commandChars);
                    if (isEqual)
                    {
                        if (word != command)
                        {
                            newList.Add(word);
                        }
                    }

                }
            }
            return newList;
        }
    }
}

            //var listOfCommands = command.Split(" ");
            //var newList = new List<Anagram>();
            //var commandList = new List<Words>();

            //for(int i = 0; i < listOfCommands.Length; i++)
            //{
            //    Words commandItem = new Words();
            //    commandItem.Word = listOfCommands[i];
            //    var items = allWords.Where(x => x == commandItem.Word).ToList();
            //    var item = allWords.SingleOrDefault(x => x == commandItem.Word);
            //    if (item != null)
            //        allWords.Remove(item);
            //}

            //command = command.Replace(" ", string.Empty);

            //var commandChars = command.ToList();
            //var remainingChars = commandChars;
            //var containsChars = remainingChars;
            //var result = string.Empty;

            //SentenceMaker(remainingChars);
            
            //void SentenceMaker(List<char> remainingChars)
            //{
            //    var contains = false;
            //    foreach (var combination in allWords)
            //    {
            //        var word = combination;

            //        if(combination.Length >= minLength)
            //        {
            //            var wordChars = word.ToCharArray();
            //            foreach (char charInWord in word)
            //            {
            //                if (containsChars.Contains(charInWord))
            //                {
            //                    containsChars.Remove(charInWord);
            //                    contains = true;
            //                }else
            //                {
            //                    contains = false;
            //                    break;
            //                }
            //            }

            //            if (contains)
            //            {
            //                foreach (char charInWord in wordChars)
            //                {
            //                    remainingChars.Remove(charInWord);
            //                }
            //                var item = allWords.SingleOrDefault(x => x == word);
            //                if (item != null)
            //                    allWords.Remove(item);
            //                result += $"{word} ";
            //                if (!remainingChars.Any())
            //                {
            //                    Anagram anagram = new Anagram();
            //                    anagram.Word = result;
            //                    newList.Add(anagram);
            //                } else if(combination == allWords.Last())
            //                {
            //                    break;
            //                }
            //                SentenceMaker(remainingChars);
            //            }
            //        }

            //    }
            //}
            