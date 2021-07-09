using AnagramSolver.BusinessLogic.Classes;
using NUnit.Framework;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class WordRepositoryTests
    {
        private WordRepository _wordRepository;
        public const string _filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt";
        [SetUp] public void Setup()
        {
            _wordRepository = new WordRepository();
        }


        [Test]
        public void Should_ReturnListOfWords_When_GivenFileOfWords()
        {
            var allWords = _wordRepository.GetAllWords(_filePath, 4);
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        [TestCase("C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas2.txt")]
        public void Should_ReturnNull_When_GivenWrongFilePath(string filePath)
        {
            var allWords = _wordRepository.GetAllWords(@filePath, 4);
            Assert.IsNull(allWords);
        }
        [Test]
        [TestCase("C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\Data\\trumpasZodynas.txt")]
        public void Should_Return4Words_When_Given5Words(string filePath)
        {
            var allWords = _wordRepository.GetAllWords(@filePath, 4);
            Assert.That(allWords.Count, Is.EqualTo(4));
        }
    }
}
