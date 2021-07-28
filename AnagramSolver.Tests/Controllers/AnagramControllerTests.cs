using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using AnagramSolver.Tests.Helpers;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Tests.Controllers
{
    [TestFixture]
    public class AnagramControllerTests
    {
        private ICacheServices _cachedServices;
        private IWordServices _wordServices;
        private AnagramController _anagramController;
        private GetTestWords _testWords;
        [SetUp]
        public void Setup()
        {
            _cachedServices = Substitute.For<ICacheServices>();
            _wordServices = Substitute.For<IWordServices>();
            _anagramController = new AnagramController(_wordServices, _cachedServices);
            _testWords = new GetTestWords();
        }
        [Test]
        public void Should_GetWordsInPageOne_When_GivenPageNumberOne()
        {
            var allWords = _testWords.GetHashSetOfAnagramModel();
            _wordServices.GetWordsAsAnagramModelVocabulary(Arg.Any<int>()).Returns(allWords);
            var pageNumber = 1;

            var result = (ViewResult)_anagramController.Index(pageNumber);
            var model = result.Model;

            Assert.AreEqual(allWords, model);
        }
        [Test]
        [TestCase("alus")]
        public void Should_ReturnWordWithAnagramFromCache_When_WordDoesExistsInCache(string wordForAnagrams)
        {
            var cachedModel = _testWords.GetCacheModelIsSuccessfulTrue();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);

            var result = (ViewResult)_anagramController.Details(wordForAnagrams);
            var model = result.Model;

            Assert.AreEqual(allWords, model);
            Assert.That(cachedModel.IsSuccessful, Is.True);
        }

        [Test]
        public void Should_ReturnNewlyCreatedVocabulary_When_WordDoesNotExistInCache()
        {
            var wordForAnagrams = "rimti";
            var cachedModel = _testWords.GetCacheModelIsSuccessfulFalse();
            var allWords = cachedModel.Caches;
            _cachedServices.GetCachedAnagram(wordForAnagrams).Returns(cachedModel);
            var anagramModel = new AnagramModel { Word = wordForAnagrams, AnagramWord = "mirti" };
            var vocabularyByModel = new HashSet<AnagramModel> { anagramModel };

            _wordServices.GetAnagrams(wordForAnagrams).Returns(vocabularyByModel);
            _cachedServices.PutAnagramToCache(wordForAnagrams, vocabularyByModel);

            var result = (ViewResult)_anagramController.Details(wordForAnagrams);
            var model = result.Model;

            Assert.AreNotEqual(allWords, model);
            Assert.That(cachedModel.IsSuccessful, Is.False);
        }

    }
}
