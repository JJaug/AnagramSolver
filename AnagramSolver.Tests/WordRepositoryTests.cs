using AnagramSolver.BusinessLogic.Classes;
using NUnit.Framework;
using System;
using System.IO;

namespace AnagramSolver.Tests
{

    [TestFixture]
    public class WordRepositoryTests
    {
        private WordRepository _wordRepository;
        public const string _filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\Data\\zodynas.txt";
        public string result;
        public const string dirName = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\";
        public static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return Uri.UnescapeDataString(referenceUri.MakeRelativeUri(fileUri).ToString());
        }


        [SetUp] public void Setup()
        {
            _wordRepository = new WordRepository();
        }


        [Test]
        public void Should_ReturnListOfWords_When_GivenFileOfWords()
        {
            var result = MakeRelative(_filePath, dirName);
            var allWords = _wordRepository.GetAllWords(result, 4);
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        public void Should_CatchException_When_GivenWrongFilePath()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas2.txt";
            var ex = Assert.Throws<FileNotFoundException>(() => _wordRepository.GetAllWords(filePath, 4));
            Assert.That(ex.Message, Is.EqualTo("File not found"));

        }
        [Test]
        public void Should_Return4Words_When_Given5Words()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\Data\\trumpasZodynas.txt";
            var allWords = _wordRepository.GetAllWords(filePath, 4);
            Assert.That(allWords.Count, Is.EqualTo(4));
        }
        [Test]
        public void Should_CatchException_When_NoDefinedIndex()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\Data\\netaisyklingasZodynas.txt";
            var ex = Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetAllWords(filePath, 4));
            Assert.That(ex.Message, Is.EqualTo("Defined index doesn't exist"));
        }
        [Test]
        public void Should_CatchException_When_GivenIncorrectFile()
        {
            var filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\AnagramSolver.Tests\\Data\\netaisyklingasZodynas.txt";
            var ex = Assert.Throws<Exception>(() => _wordRepository.GetAllWords(filePath, 4));
            Assert.That(ex.Message, Is.EqualTo("Corrupted file"));
        }
    }
}
