using AnagramSolver.BusinessLogic.Classes.CacheAnagrams;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class CacheServicesTests
    {
        private TestData _testWords;
        private ICacheRepository _cacheRepository;
        private ICacheServices _cacheServices;
        [SetUp]
        public void Setup()
        {
            _testWords = new TestData();
            _cacheRepository = Substitute.For<ICacheRepository>();
            _cacheServices = new CacheServices(_cacheRepository);

        }
        [Test]
        public void Should_ReturnCachedModelWithAnagramsAndIsSuccessfulTrue_When_GivenWordThatIsInCache()
        {
            var command = "alus";
            var cachedAnagram = _testWords.GetCachedWord();
            _cacheRepository.FindCachedWord(Arg.Any<string>()).Returns(cachedAnagram);

            var cachedModel = _cacheServices.GetCachedAnagram(command);

            Assert.IsTrue(cachedModel.IsSuccessful);

        }
        [Test]
        [TestCase("sula", "alus")]
        public void Should_PutAnagramToCache_When_GivenCommandAndAnagrams(string command, string anagram)
        {
            var cachedWord = new CachedWord { Word = command, Anagram = anagram };
            _cacheRepository.SaveWordToCache(cachedWord);
            Assert.Pass();
        }

    }
}
