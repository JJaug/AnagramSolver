using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.CacheAnagrams
{
    public class CacheAnagramDatabaseFirst : ICacheAnagram
    {
        private readonly VocabularyDBContext _context;
        public CacheAnagramDatabaseFirst(VocabularyDBContext context)
        {
            _context = context;
        }
        public CacheModel GetCachedAnagram(string command)
        {
            var cachedModel = new CacheModel();
            var cachedAnagram = _context.CachedWords.Find(command);
            if (cachedAnagram != null)
            {
                var anagramModel = new AnagramModel();
                anagramModel.Word = command;
                anagramModel.AnagramWord = cachedAnagram.Anagram;
                cachedModel.Caches.Add(anagramModel);
                cachedModel.IsSuccessful = true;
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

            var cachedWord = new CachedWord { Word = command, Anagram = anagramToDB };
            _context.CachedWords.Add(cachedWord);
            _context.SaveChanges();

        }
    }
}
