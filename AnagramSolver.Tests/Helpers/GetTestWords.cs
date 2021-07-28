using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.Tests.Helpers
{
    public class GetTestWords
    {
        public HashSet<Word> GetTestAllWords()
        {
            var allWords = new HashSet<Word>();
            string[] words = { "labos", "balos", "mirti", "rimti", "alus", "sula", "salu", "visma", "giria", "girti", "rytas", "pieva", "lekiau", "kiaule", "saule", "dabar", "sveiki", "puiku", "tvarka", "varna" };
            for (int i = 0; i < 20; i++)
            {
                var wordModel = new Word { Id = i + 1, Word1 = words[i] };
                allWords.Add(wordModel);
            }
            return allWords;
        }
    }
}
