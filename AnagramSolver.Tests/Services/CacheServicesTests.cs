using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class CacheServicesTests
    {
        private GetTestWords _helper;
        private ICacheRepository _cacheRepository;
        [SetUp]
        public void Setup()
        {
            _helper = new GetTestWords();
            _cacheRepository = Substitute.For<ICacheRepository>();

        }
        [Test]
        [TestCase("sula", "alus")]
        public void Should_PutAnagramToCache_When_GivenCommandAndAnagrams(string command, string anagram)
        {
            var cachedWord = new CachedWord { Word = command, Anagram = anagram };
        }
    }
}
