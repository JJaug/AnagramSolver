using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
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

            Dictionary<int, char> commandDict = new Dictionary<int, char>();

            var commandChars = command.ToCharArray();

            for(int i=0; i < commandChars.Length; i++)
            {
                commandDict.Add(i, commandChars[i]);
            }
            var remainingChars = commandChars;
            var result = string.Empty;

            sentenceMaker(remainingChars);
            
            void sentenceMaker(char[] remainingChars)
            {
                foreach (var combination in allWords)
                {
                    if(combination.Word.Length >= minLength)
                    {
                        var word = combination.Word;
                        var wordChars = word.ToCharArray();
                        var contains = !wordChars.Except(remainingChars).Any();
                        if (contains)
                        {
                            //remainingChars = remainingChars.;
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
