using AnagramSolver.BusinessLogic.Classes.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class WordServicesTests
    {
        private IWordRepository _wordRepository;
        private IWordServices _wordServices;
        private int wordsInPage;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _wordServices = new WordServices(_wordRepository);
            wordsInPage = 100;
        }
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void Should_GetSpecificWordsInAnagramModelVocabulary_When_GivenPageNumber(int pageNumber)
        {
            var allWords = _wordRepository.GetAllWords();
            _wordRepository.GetSpecificPage(pageNumber).Returns(allWords);

            var result = _wordServices.GetWordsAsAnagramModelVocabulary(pageNumber);

            foreach(var resultWord in result)
            {
                foreach(var repositoryWord in allWords)
                {
                    Assert.That(resultWord.Word, Is.EqualTo(repositoryWord.Word));
                }
            }
        }

    }
}
