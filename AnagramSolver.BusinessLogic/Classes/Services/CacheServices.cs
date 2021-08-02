using AnagramSolver.BusinessLogic.Exceptions;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.CacheAnagrams
{
    public class CacheServices : ICacheServices
    {
        private readonly ICacheRepository _cacheRepository;
        public CacheServices(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<CacheModel> GetCachedAnagram(string command)
        {
            try
            {
                ValidateCommand(command);
                var cachedModel = new CacheModel();
                var cachedAnagram = await _cacheRepository.FindCachedWord(command);
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
            catch (WrongCommandException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        public void PutAnagramToCache(string command, HashSet<AnagramModel> listOfAnagrams)
        {
            var anagramToDB = string.Empty;

            foreach (var anagram in listOfAnagrams)
            {
                anagramToDB += $"{anagram.AnagramWord}  ";
            }

            var cachedWord = new CachedWord { Word = command, Anagram = anagramToDB };
            _cacheRepository.SaveWordToCache(cachedWord);

        }
        public void ValidateCommand(string command)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");

            if (!regex.IsMatch(command))
                throw new WrongCommandException(command);
        }
    }
}
