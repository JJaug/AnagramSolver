using AnagramSolver.BusinessLogic.Classes.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class WordServicesTests
    {
        private IWordRepository _wordRepository;
        private IWordServices _wordServices;
        private GetTestWords _testWords;
        private IConfiguration configuration;
        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _wordServices = new WordServices(_wordRepository, configuration);
            _testWords = new GetTestWords();
        }
        [Test]
        [TestCase(1)]
        public void Should_GetSpecificWordsInAnagramModelVocabulary_When_GivenPageNumber(int pageNumber)
        {
            var allWords = _testWords.GetTestAllWords();
            var wordsInPage = 5;
            _wordRepository.GetSpecificPage(1, wordsInPage).Returns(allWords);

            var result = _wordServices.GetWordsAsAnagramModelVocabulary(1);

            Assert.That(result.Count, Is.EqualTo(5));
        }
        [Test]
        [TestCase("balos")]
        [TestCase("sula")]
        public void Should_GetAnagramsFromAllWords_When_GivenWord(string word)
        {
            var allWords = _testWords.GetTestAllWords();
            _wordRepository.GetWords().Returns(allWords);

            var result = _wordServices.GetAnagrams(word);

            Assert.That(result.Count, Is.GreaterThanOrEqualTo(1));
        }
        [Test]
        [TestCase("s")]
        public void Should_ReturnWordsWithSpecificPart_When_GivenPartOfWord(string wordPart)
        {
            var allWords = _testWords.GetTestAllWords();
            _wordRepository.GetSpecificWords(wordPart);

        }

    }
}
