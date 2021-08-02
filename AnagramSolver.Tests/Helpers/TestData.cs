using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using AnagramSolver.WebApp.Models;
using System.Collections.Generic;

namespace AnagramSolver.Tests.Helpers
{
    public class TestData
    {
        public List<string> GetListOfStrings()
        {
            var allWords = new List<string>();
            string[] words = { "labos", "balos", "mirti", "rimti", "alus", "sula", "salu",
                "visma", "giria", "girti", "rytas", "pieva", "lekiau", "kiaule", "saule",
                "dabar", "sveiki", "puiku", "tvarka", "varna" };
            for (int i = 0; i < 20; i++)
            {
                allWords.Add(words[i]);
            }
            return allWords;
        }
        public List<Word> GetListOfWord()
        {
            var allWords = new List<Word>();
            string[] words = { "labos", "balos", "mirti", "rimti", "alus", "sula", "salu",
                "visma", "giria", "girti", "rytas", "pieva", "lekiau", "kiaule", "saule",
                "dabar", "sveiki", "puiku", "tvarka", "varna" };
            for (int i = 0; i < 20; i++)
            {
                var wordModel = new Word { Id = i + 1, Word1 = words[i] };
                allWords.Add(wordModel);
            }
            return allWords;
        }
        public HashSet<AnagramModel> GetHashSetOfAnagramModel()
        {
            var allWords = new HashSet<AnagramModel>();
            string[] words = { "labos", "balos", "mirti", "rimti", "alus", "sula", "salu",
                "visma", "giria", "girti", "rytas", "pieva", "lekiau", "kiaule", "saule",
                "dabar", "sveiki", "puiku", "tvarka", "varna" };
            foreach (var word in words)
            {
                var anagramModel = new AnagramModel();
                anagramModel.Word = word;
                allWords.Add(anagramModel);
            }

            return allWords;
        }
        public CacheModel GetCacheModelIsSuccessfulTrue()
        {
            var anagram = new AnagramModel { Word = "alus", AnagramWord = "sula" };
            var cachedAnagram = new HashSet<AnagramModel> { anagram };
            var cachedModel = new CacheModel { Caches = cachedAnagram, IsSuccessful = true };
            return cachedModel;
        }
        public CacheModel GetCacheModelIsSuccessfulFalse()
        {
            var anagram = new AnagramModel { Word = "alus", AnagramWord = "sula" };
            var cachedAnagram = new HashSet<AnagramModel> { anagram };
            var cachedModel = new CacheModel { Caches = cachedAnagram, IsSuccessful = false };
            return cachedModel;
        }
        public CachedWord GetCachedWord()
        {
            var cachedWord = new CachedWord { Word = "alus", Anagram = "sula" };
            return cachedWord;
        }
        public UserDto GetUserDto()
        {
            var userDto = new UserDto
            {
                Id = 1,
                FirstName = "Jonas",
                LastName = "Jaugelis",
                Email = "jonas@jonas.lt",
                Pass = "labas",
                FavouriteWords = "pieva"
            };
            return userDto;
        }
        public User CreateTestUser()
        {
            var userId = 1;
            var userName = "Jonas";
            var userLastName = "Jaugelis";
            var userEmail = "abc@cdv.lt";
            var userPass = "labas";
            var user = new User { Id = userId, FirstName = userName, LastName = userLastName, Email = userEmail, Pass = userPass };
            return user;
        }

    }
}
