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


            var commandChars = command.ToList();
            var remainingChars = commandChars;
            var result = string.Empty;

            SentenceMaker(remainingChars);
            
            void SentenceMaker(List<char> remainingChars)
            {
                var contains = false;
                foreach (var combination in allWords)
                {
                    if(combination.Word.Length >= minLength)
                    {
                        var word = combination.Word;
                        var wordChars = word.ToCharArray();

                        foreach (char charInWord in word)
                        {
                            if (remainingChars.Contains(charInWord))
                            {
                                contains = true;
                            }else
                            {
                                contains = false;
                                break;
                            }
                        }

                        if (contains)
                        {
                            foreach (char charInWord in word)
                            {
                                remainingChars.Remove(charInWord);
                            }
                            result += $"{word} ";
                            if (!remainingChars.Any())
                            {
                                Anagram anagram = new Anagram();
                                anagram.Word = result;
                                newList.Add(anagram);
                            }
                            SentenceMaker(remainingChars);
                        }
                    }

                }
            }
            return newList;
        }
    }
}
