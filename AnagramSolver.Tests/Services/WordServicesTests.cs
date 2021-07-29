using AnagramSolver.BusinessLogic.Classes.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class WordServicesTests
    {
        private IWordRepository _wordRepository;
        private IWordServices _wordServices;
        private GetTestWords _testWords;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _configuration = Substitute.For<IConfiguration>();
            _wordServices = new WordServices(_wordRepository, _configuration);
            _testWords = new GetTestWords();
        }
        [Test]
        [TestCase(1)]
        public void Should_GetSpecificWordsInAnagramModelVocabulary_When_GivenPageNumber(int pageNumber)
        {
            var wordsInPage = "5";
            var allWords = _testWords.GetListOfWord();
            _configuration["MyConfig:WordsInPage"].Returns(wordsInPage);
            _wordRepository.GetWords().Returns(allWords);

            var result = _wordServices.GetWordsAsAnagramModelVocabulary(pageNumber);

            Assert.That(result.Count, Is.EqualTo(5));
        }
        [Test]
        [TestCase("balos")]
        [TestCase("sula")]
        public void Should_GetAnagramsFromAllWords_When_GivenWord(string word)
        {
            var allWords = _testWords.GetListOfWord();
            _wordRepository.GetWords().Returns(allWords);

            var result = _wordServices.GetAnagrams(word);

            Assert.That(result.Count, Is.GreaterThanOrEqualTo(1));
        }
        [Test]
        [TestCase("s")]
        [TestCase("b")]
        public void Should_ReturnWordsWithSpecificPart_When_GivenPartOfWord(string wordPart)
        {
            var allWords = _testWords.GetListOfWord();
            var specificWords = allWords.Where(a => a.Word1.Contains(wordPart)).ToHashSet();
            _wordRepository.GetSpecificWords(wordPart).Returns(specificWords);

            var result = _wordServices.GetWordsThatHaveGivenPart(wordPart);

            Assert.That(specificWords.Count, Is.EqualTo(result.Count));
        }

    }
}
