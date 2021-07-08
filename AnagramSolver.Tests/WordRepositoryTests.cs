using AnagramSolver.BusinessLogic.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class WordRepositoryTests
    {
        [SetUp]


        [Test]
        public void Should_ReturnListOfWords_When_GivenFileOfWords()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt";
            var wordRepository = new WordRepository();
            var allWords = wordRepository.GetAllWords(@filePath, 4);
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        public void Should_ReturnNull_When_GivenWrongFilePath()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas2.txt";
            var wordRepository = new WordRepository();
            var allWords = wordRepository.GetAllWords(@filePath, 4);
            Assert.IsNull(allWords);
        }
    }
}
