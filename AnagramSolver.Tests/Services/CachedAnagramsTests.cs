using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class CachedAnagramsTests
    {
        private GetTestWords _helper;
        [SetUp]
        public void Setup()
        {
            _helper = new GetTestWords();

        }
        [Test]
        [TestCase("sula", "alus")]
        public void Should_PutAnagramToCache_When_GivenCommandAndAnagrams(string command, string anagram)
        {
            var mockSet = Substitute.For<DbSet<CachedWord>>();
            var mockContext = Substitute.For<VocabularyDBContext>();
            mockContext.CachedWords.Returns(mockSet);
            var cachedWord = new CachedWord { Word = command, Anagram = anagram };
            mockContext.CachedWords.Add(cachedWord);
            mockSet.Received(1).Add(Arg.Any<CachedWord>());
            mockContext.Received(1).SaveChangesAsync();
        }
    }
}
