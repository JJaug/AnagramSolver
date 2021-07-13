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
        public string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/";
        [SetUp] public void Setup()
        {
            _wordRepository = new WordRepository();
        }


        [Test]
        public void Should_ReturnListOfWords_When_GivenFileOfWords()
        {
            var allWords = _wordRepository.GetAllWords(_filePath + "zodynas.txt", 4);
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        public void Should_CatchException_When_GivenWrongFilePath()
        {
            var ex = Assert.Throws<FileNotFoundException>(() => _wordRepository.GetAllWords(_filePath + "zodynas2.txt", 4));
            Assert.That(ex.Message, Is.EqualTo("File not found"));

        }
        [Test]
        public void Should_Return4Words_When_Given5Words()
        {
            var allWords = _wordRepository.GetAllWords(_filePath + "trumpasZodynas.txt", 4);
            Assert.That(allWords.Count, Is.EqualTo(4));
        }
        [Test]
        public void Should_CatchException_When_NoDefinedIndex()
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetAllWords(_filePath + "netaisyklingasZodynas.txt", 4));
            Assert.That(ex.Message, Is.EqualTo("Defined index doesn't exist"));
        }
        [Test]
        public void Should_CatchException_When_GivenIncorrectFile()
        {
            var ex = Assert.Throws<Exception>(() => _wordRepository.GetAllWords(_filePath + "netaisyklingasZodynas.txt", 4));
            Assert.That(ex.Message, Is.EqualTo("Corrupted file"));
        }
    }
}
