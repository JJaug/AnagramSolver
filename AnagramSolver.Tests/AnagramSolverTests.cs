using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private BusinessLogic.Classes.AnagramSolver _anagramSolver;

        [SetUp]
        public void Setup()
        {
            HashSet<string> allWords = new HashSet<string>(7) { "rimti", "mirti", "sula", "alus", "balos", "labos", "balso" };
            _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
        }

        [Test]
        [TestCase("sula", "alus")]
        [TestCase("alus", "sula")]
        [TestCase("mirti", "rimti")]
        [TestCase("rimti", "mirti")]
        public void Should_ReturnOneAnagram_When_GivenOneWord(string command, string anagram)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command);

            Assert.That(allAnagrams[0], Is.EqualTo(anagram));
            Assert.AreEqual(anagram, allAnagrams[0]);
        }

        [Test]
        public void Should_ReturnAllAnagrams_When_GivenOneWord()
        {
            var word = "balos";
            var allAnagrams = _anagramSolver.GetAnagrams(word);

            Assert.That(allAnagrams.Count, Is.GreaterThan(1));
        }

        [Test]
        public void Should_ReturnEmptyList_When_GivenWordWithNoAnagrams()
        {
            var word = "abcd";
            var allAnagrams = _anagramSolver.GetAnagrams(word);

            Assert.That(allAnagrams, Is.Empty);
        }

        [Test]
        [TestCase("rimti sula", "mirti alus")]
        public void Should_ReturnOneAnagram_When_GivenMoreThanOneWord(string command, string anagram)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command);

            Assert.That(allAnagrams[0], Is.EqualTo(anagram));
        }

        [Test]
        [TestCase("rimti sula")]
        [TestCase("visma praktika")]
        [TestCase("labas rytas")]
        public void Should_ReturnAllAnagrams_When_GivenMoreThanOneWord(string command)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command);

            Assert.That(allAnagrams.Count, Is.GreaterThanOrEqualTo(1));
        }
    }
}
