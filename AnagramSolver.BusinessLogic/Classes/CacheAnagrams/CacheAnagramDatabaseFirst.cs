using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.CacheAnagrams
{
    public class CacheAnagramDatabaseFirst : ICacheAnagram
    {
        public CacheModel GetCachedAnagram(string command)
        {
            var cachedModel = new CacheModel();
            using (var context = new VocabularyDBContext())
            {
                var cachedAnagram = context.CachedWords.Find(command);
                if (cachedAnagram != null)
                {
                    var anagramModel = new AnagramModel();
                    anagramModel.Word = command;
                    anagramModel.AnagramWord = cachedAnagram.Anagram;
                    cachedModel.Caches.Add(anagramModel);
                    cachedModel.IsSuccessful = true;
                }
            }
            return cachedModel;
        }

        public void PutAnagramToCache(string command, HashSet<AnagramModel> listOfAnagrams)
        {
            var anagramToDB = string.Empty;
            foreach (var anagram in listOfAnagrams)
            {
                anagramToDB += $"{anagram.AnagramWord}  ";
            }
            using (var context = new VocabularyDBContext())
            {
                var cachedWord = new CachedWord { Word = command, Anagram = anagramToDB };
                context.CachedWords.Add(cachedWord);
                context.SaveChanges();
            }
        }
    }
}
