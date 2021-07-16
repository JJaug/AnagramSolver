﻿using AnagramSolver.BusinessLogic.Classes;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AnagramSolver.Tests
{

    [TestFixture]
    public class WordRepositoryTests
    {
        private WordRepository _wordRepository;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/";


        [Test]
        public void Should_ReturnListOfWords_When_GivenFileOfWords()
        {
            _wordRepository = new WordRepository(_filePath + "zodynas.txt", 4);
            var allWords = _wordRepository.GetAllWords();
            Assert.That(allWords, !Is.Null);
        }
        [Test]
        public void Should_CatchException_When_GivenWrongFilePath()
        {
            _wordRepository = new WordRepository(_filePath + "zodynas2.txt", 4);
            var ex = Assert.Throws<FileNotFoundException>(() => _wordRepository.GetAllWords());
            Assert.That(ex.Message, Is.EqualTo("File not found"));

        }
        [Test]
        public void Should_Return4Words_When_Given5Words()
        {
            _wordRepository = new WordRepository(_filePath + "trumpasZodynas.txt", 4);

            var allWords = _wordRepository.GetAllWords();
            Assert.That(allWords.Count, Is.EqualTo(4));
        }
        [Test]
        public void Should_CatchException_When_NoDefinedIndex()
        {
            _wordRepository = new WordRepository(_filePath + "netaisyklingasZodynas.txt", 4);

            var ex = Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetAllWords());
            Assert.That(ex.Message, Is.EqualTo("Defined index doesn't exist"));
        }
        [Test]
        [Explicit]
        public void Should_CatchException_When_GivenIncorrectFile()
        {
            _wordRepository = new WordRepository(_filePath + "netaisyklingasZodynas.txt", 4);
            var ex = Assert.Throws<Exception>(() => _wordRepository.GetAllWords());
            Assert.That(ex.Message, Is.EqualTo("Corrupted file"));
        }
        [Test]
        public void Should_ReturnFirst100Words_When_PaginatorInPageOne()
        {
            _wordRepository = new WordRepository(_filePath + "zodynas.txt", 4);
            var words = _wordRepository.GetSpecificPage(1);
            var allWords = _wordRepository.GetAllWords();
            var first100 = allWords.Take(100).ToList();
            Assert.That(words, Is.EqualTo(first100));
        }
        [Test]
        public void Should_ReturnSecond100Words_When_PaginatorInPageTwo()
        {
            _wordRepository = new WordRepository(_filePath + "zodynas.txt", 4);
            var words = _wordRepository.GetSpecificPage(2);
            var allWords = _wordRepository.GetAllWords();
            var second100 = allWords.Skip(100).Take(100).ToList();
            Assert.That(words, Is.EqualTo(second100));
        }
    }
}
