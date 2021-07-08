using AnagramSolver.BusinessLogic.Classes;
using NUnit.Framework;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class WordRepositoryTests
    {
        private WordRepository _wordRepository;

        [SetUp] public void Setup()
        {
            _wordRepository = new WordRepository();
        }


        [Test]
        [TestCase("C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt")]
        public void Should_ReturnListOfWords_When_GivenFileOfWords(string filePath)
        {
            var allWords = _wordRepository.GetAllWords(@filePath, 4);
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        [TestCase("C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas2.txt")]
        public void Should_ReturnNull_When_GivenWrongFilePath(string filePath)
        {
            var allWords = _wordRepository.GetAllWords(@filePath, 4);
            Assert.IsNull(allWords);
        }
    }
}
