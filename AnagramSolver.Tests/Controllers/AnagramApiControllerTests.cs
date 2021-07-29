using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using AnagramSolver.Tests.Helpers;
using AnagramSolver.WebApp.Controllers.Api;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AnagramSolver.Tests.Controllers
{
    [TestFixture]
    public class AnagramApiControllerTests
    {
        private IWordServices _wordServices;
        private ICacheServices _cachedServices;
        private ISearchLogServices _searchLogServices;
        private AnagramApiController _anagramApiController;
        private TestData _testWords;

        [SetUp]
        public void Setup()
        {
            _cachedServices = Substitute.For<ICacheServices>();
            _wordServices = Substitute.For<IWordServices>();
            _searchLogServices = Substitute.For<ISearchLogServices>();
            _anagramApiController = new AnagramApiController(_wordServices, _cachedServices, _searchLogServices);
            _testWords = new TestData();
        }
        [Test]
        public void Should_GetJsonStringFromVocabularyByModel_When_GivenWordForAnagramUsingCache()
        {
            var wordForAnagrams = "alus";
            var cachedModel = _testWords.GetCacheModelIsSuccessfulTrue();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);
            var jsonFromTestWords = JsonSerializer.Serialize(allWords);

            var result = _anagramApiController.GetJsonString(wordForAnagrams);

            Assert.AreEqual(jsonFromTestWords, result);
        }

        [Test]
        public void Should_GetJsonStringFromVocabularyByModel_When_GivenWordForAnagramWithoutCache()
        {
            var wordForAnagrams = "rimti";
            var cachedModel = _testWords.GetCacheModelIsSuccessfulFalse();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);
            var anagramModel = new AnagramModel { Word = wordForAnagrams, AnagramWord = "mirti" };
            var vocabularyByModel = new HashSet<AnagramModel> { anagramModel };
            var jsonFromTestWords = JsonSerializer.Serialize(allWords);
            var jsonFromWordForAnagrams = JsonSerializer.Serialize(vocabularyByModel);


            _wordServices.GetAnagrams(wordForAnagrams).Returns(vocabularyByModel);
            _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);

            var result = _anagramApiController.GetJsonString(wordForAnagrams);

            Assert.AreNotEqual(jsonFromTestWords, result);
            Assert.AreEqual(jsonFromWordForAnagrams, result);
        }
        [Test]
        public void Should_ReturnVocabularyByModel_When_GivenWordForAnagramUsingCache()
        {
            var wordForAnagrams = "alus";
            var cachedModel = _testWords.GetCacheModelIsSuccessfulTrue();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);

            var result = _anagramApiController.GetForJS(wordForAnagrams);

            Assert.AreEqual(allWords, result);
            Assert.That(cachedModel.IsSuccessful, Is.True);
        }
        [Test]
        public void Should_ReturnVocabularyByModel_When_GivenWordForAnagramWithoutCache()
        {
            var wordForAnagrams = "rimti";
            var cachedModel = _testWords.GetCacheModelIsSuccessfulFalse();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);
            var anagramModel = new AnagramModel { Word = wordForAnagrams, AnagramWord = "mirti" };
            var vocabularyByModel = new HashSet<AnagramModel> { anagramModel };

            _wordServices.GetAnagrams(wordForAnagrams).Returns(vocabularyByModel);
            _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);

            var result = _anagramApiController.GetForJS(wordForAnagrams);

            Assert.AreNotEqual(allWords, result);
            Assert.That(cachedModel.IsSuccessful, Is.False);
        }
        [Test]
        [TestCase("s")]
        [TestCase("b")]
        public void Should_GetWordsContainingSpecificPart_When_GivenPartOfWord(string wordPart)
        {
            var allWords = _testWords.GetListOfStrings();
            var specificWords = allWords.Where(w => w.Contains(wordPart)).ToHashSet();
            _wordServices.GetWordsThatHaveGivenPart(wordPart).Returns(specificWords);

            var result = _anagramApiController.GetSearchList(wordPart);

            Assert.That(specificWords.Count, Is.EqualTo(result.Count));
            Assert.AreEqual(specificWords, result);
        }
    }
}
